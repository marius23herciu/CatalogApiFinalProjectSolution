using CatalogApiFinalProject.DTOs;
using Data.Models;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogApiFinalProject.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace CatalogApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly DataLayer dataLayer;
        private readonly CatalogueDbContext ctx;

        public StudentsController(CatalogueDbContext ctx, DataLayer dataLayer)
        {
            this.ctx = ctx;
            this.dataLayer = dataLayer;
        }

        /*• Obtinerea tuturor studentilor*/

        /// <summary>
        /// Returns all students with full name, age and address.
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StudentWithAddressToGet>))]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await ctx.Students.Include(s => s.Address).Select(s => s.ToDto()).ToListAsync());
        }

        /*• Obtinerea unui student dupa ID*/

        /// <summary>
        /// Returns data of a selected student by his Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentWithAddressToGet))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            var student = await ctx.Students.Include(a => a.Address).Where(s => s.Id == id).FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound("Student Id does not exist.");
            }
            return Ok(student.ToDto());
        }

        /*• Creeare student*/

        /// <summary>
        /// Creates and adds a student in database.
        /// </summary>
        /// <param name="studentToCreate"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StudentWithAddressToGet))]
        public IActionResult CreateStudent([FromBody] StudentToCreate studentToCreate)
        {
            var newStudent = dataLayer.CreateStudent(studentToCreate.FirstName, studentToCreate.LastName, studentToCreate.Age).Result;
            return Created("New Student Created.", newStudent.ToDto());
        }

        /*• Stergere student*/

        /// <summary>
        /// Deletes a student from Db.
        /// </summary>
        /// <param name="studentId"></param>
        [HttpDelete("delete/{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DeleteStudent([FromBody] int studentId)
        {
            var studentToRemove = await ctx.Students.Include(a => a.Address).Where(s => s.Id == studentId).FirstOrDefaultAsync();

            if (studentToRemove == null)
            {
                return NotFound("Student does not exist.");
            }

            studentToRemove.Address = null;
            ctx.Students.Remove(studentToRemove);
            await ctx.SaveChangesAsync();

            return Ok();
        }

        /*• Modificare date student*/

        /// <summary>
        /// Changes student's First Name and/or Last Name and/or Age.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="studentData"></param>
        [HttpPut("change-data/{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> ChangeStudentData([FromRoute] int studentId, [FromBody] StudentData studentData)
        {
            var student = await ctx.Students.Where(s => s.Id == studentId).FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound($"Student with Id {studentId} does not exist.");
            }

            if (studentData.FirstName != string.Empty)
            {
                student.FirstName = studentData.FirstName;
            }
            if (studentData.LastName != string.Empty)
            {
                student.LastName = studentData.LastName;
            }
            if (studentData.Age != null)
            {
                student.Age = studentData.Age;
            }

            await ctx.SaveChangesAsync();

            return Ok();
        }

        /*• Modificare adresa student
     • In cazul in care studentul nu are adresa, aceasta va fi creeata*/

        /// <summary>
        /// Adds or changes student address.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="address"></param>
        [HttpPut("change-Address/{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> ChangeStudentAddress([FromRoute] int studentId, [FromBody] AddressToCreate address)
        {
            var student = await ctx.Students.Include(s => s.Address).Where(s => s.Id == studentId).FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound($"Student with Id {studentId} does not exist.");
            }

            if (student.Address == null)
            {
                student.Address = new Address
                {
                    City = address.City,
                    Street = address.Street,
                    Number = address.Number,
                };
            }
            else
            {
                var adresseToChange = student.Address;
                adresseToChange.City = address.City;
                adresseToChange.Street = address.Street;
                adresseToChange.Number = address.Number;
            }

            await ctx.SaveChangesAsync();

            return Ok();
        }

        /*• Stergerea unui student
     • Cu un parametru care va specifica daca adresa este la randul ei
stearsa*/

        /// <summary>
        /// Deletes stundent with or without address.
        /// </summary>
        /// <param name="studentId">Id of student to remove</param>
        /// <param name="deleteAddress">If true, deletes student with his address.</param>
        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DeleteStudentAndAddress([FromBody] int studentId, [FromQuery] bool deleteAddress)
        {
            var studentToRemove = await ctx.Students.Include(a => a.Address).Where(s => s.Id == studentId).FirstOrDefaultAsync();

            if (studentToRemove == null)
            {
                return NotFound($"Student with Id {studentId} does not exist.");
            }

            if (deleteAddress)
            {
                var adresseToRemove = await ctx.Adresses.Where(s => s.StudentId == studentId).FirstOrDefaultAsync();
                if (adresseToRemove != null)
                {

                    ctx.Adresses.Remove(adresseToRemove);
                }
                ctx.Students.Remove(studentToRemove);
            }
            else
            {
                ctx.Students.Remove(studentToRemove);
            }

            await ctx.SaveChangesAsync();

            if (deleteAddress)
            {
                return Ok("Student deleted with its address.");
            }
            return Ok("Student deleted without address.");
        }
    }
}
