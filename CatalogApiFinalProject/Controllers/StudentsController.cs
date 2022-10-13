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
        /// Returns all students with name and age.
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StudentToGet>))]
        public IActionResult GetAllStudents()
        {
            var students = ctx.Students.Include(s => s.Subjects).ToList().Select(s => s.ToDto());
            return Ok(students);
        }

        /*• Obtinerea unui student dupa ID*/

        /// <summary>
        /// Returns name of a selected student by his Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToGet))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetStudentById([FromRoute] int id)
        {
            var student = ctx.Students.Where(s => s.Id == id).FirstOrDefault();
            if (student==null)
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StudentToGet))]
        public IActionResult CreateStudent([FromBody] StudentToCreate studentToCreate)
        {
            var newStudent = dataLayer.CreateStudent(studentToCreate.FirstName, studentToCreate.LastName, studentToCreate.Age);
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
        public IActionResult DeleteStudent([FromBody] int studentId)
        {
            var studentToRemove = ctx.Students.Include(a => a.Adresse).Where(s => s.Id == studentId).FirstOrDefault();

            if (studentToRemove == null)
            {
                return NotFound("Student does not exist.");
            }

            studentToRemove.Adresse = null;
            ctx.Students.Remove(studentToRemove);
            ctx.SaveChanges();

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
        public IActionResult ChangeStudentData([FromRoute] int studentId, [FromBody] StudentData studentData)
        {
            var student = ctx.Students.Where(s => s.Id == studentId).FirstOrDefault();

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

            ctx.SaveChanges();

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
        public IActionResult ChangeStudentAddress([FromRoute] int studentId, [FromBody] AddressToCreate address)
        {
            var student = ctx.Students.Include(s => s.Adresse).Where(s => s.Id == studentId).FirstOrDefault();

            if (student == null)
            {
                return NotFound($"Student with Id {studentId} does not exist.");
            }

            if (student.Adresse == null)
            {
                student.Adresse = new Address
                {
                    City = address.City,
                    Street = address.Street,
                    Number = address.Number,
                };
            }
            else
            {
                var adresseToChange = student.Adresse;
                adresseToChange.City = address.City;
                adresseToChange.Street = address.Street;
                adresseToChange.Number = address.Number;
            }

            ctx.SaveChanges();

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
        public IActionResult DeleteStudentAndAddress([FromBody] int studentId, [FromQuery] bool deleteAddress)
        {
            var studentToRemove = ctx.Students.Include(a => a.Adresse).Where(s => s.Id == studentId).FirstOrDefault();

            if (studentToRemove == null)
            {
                return NotFound($"Student with Id {studentId} does not exist.");
            }

            if (studentToRemove != null)
            {
                if (deleteAddress == true)
                {
                    var adresseToRemove = ctx.Adresses.Where(s => s.StudentId == studentId).FirstOrDefault();
                    if (adresseToRemove!=null)
                    {

                        ctx.Adresses.Remove(adresseToRemove);
                    }
                    ctx.Students.Remove(studentToRemove);
                }
                else
                {
                    ctx.Students.Remove(studentToRemove);
                }
            }

            ctx.SaveChanges();

            if (deleteAddress == false)
            {
                return Ok("Student deleted without address.");
            }

            return Ok("Student deleted with his address.");
        }
    }
}
