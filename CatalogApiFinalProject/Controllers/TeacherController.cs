using CatalogApiFinalProject.DTOs;
using Data.Models;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CatalogApiFinalProject.Extensions;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace CatalogApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly CatalogueDbContext ctx;
        private readonly DataLayer dataLayer;
        public TeacherController(CatalogueDbContext ctx, DataLayer dataLayer)
        {
            this.ctx = ctx;
            this.dataLayer = dataLayer;
        }

        /*• Adaugarea unui teacher*/

        /// <summary>
        /// Adds a teacher to catalog.
        /// </summary>
        /// <param name="teacherToCreate"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TeacherToGet))]
        public IActionResult CreateTeacher([FromBody] TeacherToCreate teacherToCreate)
        {
            var newTeacher = dataLayer.CreateTeacher(teacherToCreate.FirstName, teacherToCreate.LastName, teacherToCreate.Rank);

            return Created("New Student Created.", newTeacher.ToDto());
        }

        /* Stergerea unui teacher
               • Cum tratati stergerea?*/

        /// <summary>
        /// Deletes a teacher and its address.
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        [HttpDelete("delete-teacher/{teacherId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult DeleteTeacher([FromBody] int teacherId)
        {
            var teacherToDelete = ctx.Teachers.Include(a => a.Address).Where(s => s.Id == teacherId).FirstOrDefault();

            if (teacherToDelete == null)
            {
                return NotFound("Teacher not found.");
            }

            var addressToRemove = ctx.Adresses.Where(t => t.TeacherId == teacherId).FirstOrDefault();

            if (addressToRemove != null && addressToRemove.StudentId == null)
            {
                ctx.Adresses.Remove(addressToRemove);
            }

            teacherToDelete.Subject = null;
            ctx.Teachers.Remove(teacherToDelete);
            ctx.SaveChanges();

            return Ok();
        }

        /*• Schimbarea adresei unui teacher*/

        /// <summary>
        /// Changes or adds address(if doesnt't have) to a teacher.
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPut("change-address/{teacherId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult ChangeTeachersAddress([FromRoute] int teacherId, [FromBody] AddressToCreate address)
        {
            var teacher = ctx.Teachers.Include(s => s.Address).Where(t => t.Id == teacherId).FirstOrDefault();

            if (teacher == null)
            {
                return NotFound($"Teacher with Id {teacherId} does not exist.");
            }

            if (teacher.Address == null)
            {
                teacher.Address = new Address
                {
                    City = address.City,
                    Street = address.Street,
                    Number = address.Number,
                };
            }
            else
            {
                var addressToChange = teacher.Address;
                addressToChange.City = address.City;
                addressToChange.Street = address.Street;
                addressToChange.Number = address.Number;
            }

            ctx.SaveChanges();

            return Ok();
        }

        /*• Alocarea unui curs pentru un teacher
            */

        /// <summary>
        /// Gives a subject to a teacher who doesn't have one.
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="subjectId">Id of subject to give.</param>
        /// <returns></returns>
        [HttpPut("gives-subject-to-teacher/{teacherId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GivesCourseToTeacher([FromRoute] int teacherId, [FromBody] int subjectId)
        {
            var teacher = ctx.Teachers.Where(t => t.Id == teacherId).Include(s => s.Subject).FirstOrDefault();

            if (teacher == null)
            {
                return NotFound($"Teacher with Id {teacherId} does not exist.");
            }
            if (teacher.Subject != null)
            {
                return BadRequest($"Teacher with Id {teacherId} is appointed to another subject.");
            }
            if (ctx.Teachers.Any(s => s.SubjectId == subjectId)== true)
            {
                return BadRequest($"Subject is appointed to another teacher.");
            }

            var subject = ctx.Subjects.Where(s => s.Id == subjectId).FirstOrDefault();
            if (subject==null)
            {
                return NotFound($"Subject with Id {subjectId} does not exist.");
            }

            teacher.Subject = subject;
            ctx.SaveChanges();

            return Ok($"Subject with Id {subjectId} has been asigned to teacher with Id {teacherId}.");
        }

        /* Promovarea teacher-ului
                • Instructor devine assistant professor
                • Assistant professor devine associate professor
                • Samd*/

        /// <summary>
        /// Promotes a teacher to next rank.
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        [HttpPut("promote-teacher/{teacherId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult PromoteTeacher([FromRoute] int teacherId)
        {
            var teacher = ctx.Teachers.Where(t => t.Id == teacherId).FirstOrDefault();

            if (teacher == null)
            {
                return NotFound($"Teacher with Id {teacherId} does not exist.");
            }

            if (teacher.Rank == Rank.Professor)
            {
                return BadRequest("Teacher allready has the highest rank.");
            }

            teacher.Rank++;
            ctx.SaveChanges();

            return Ok("Teacher has been promoted.");
        }


    }
}
