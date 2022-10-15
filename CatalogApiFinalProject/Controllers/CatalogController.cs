using CatalogApiFinalProject.DTOs;
using Data.Models;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogApiFinalProject.Extensions;
using System.Collections.Generic;

namespace CatalogApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogueDbContext ctx;
        private readonly DataLayer dataLayer;
        private readonly ILogger<CatalogController> logger;

        public CatalogController(CatalogueDbContext ctx, DataLayer dataLayer, ILogger<CatalogController> logger)
        {
            this.ctx = ctx;
            this.dataLayer = dataLayer;
            this.logger = logger;
        }


        /* • Adaugarea unui curs*/

        /// <summary>
        /// Adds a subject to catalog.
        /// </summary>
        /// <param name="subjectToCreate"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubjectToGet))]
        public IActionResult AddSubject([FromBody] SubjectToCreate subjectToCreate)
        {
            var newSubject = dataLayer.AddSubject(subjectToCreate.Name).ToDto();
            return Created("New Subject Created.", newSubject);
        }

        /* Acordarea unei note unui student
                • La adaugarea notei serverul va complete automat data si ora
                acordarii ca fiind data si ora curenta*/
        /// <summary>
        /// Adds mark to a student. Put as parameters Id of student and Id of subject for the mark.
        /// </summary>
        /// <param name="markToCreate"></param>
        /// <returns></returns>
        [HttpPut("give-mark/{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult AddMarkToStudent([FromBody] MarkToCreate markToCreate)
        {
            var student = ctx.Students.Where(s => s.Id == markToCreate.StudentId).FirstOrDefault();

            if (student == null)
            {
                return NotFound($"Student with Id {markToCreate.StudentId} does not exist.");
            }

            var subject = ctx.Subjects.Where(s => s.Id == markToCreate.SubjectId).FirstOrDefault();

            if (subject == null)
            {
                return NotFound($"Subject with Id {markToCreate.SubjectId} does not exist.");
            }

            var newMark = new Mark
            {
                Value = markToCreate.Value,
                SubjectId = markToCreate.SubjectId,
                DateTime = DateTime.Now,
            };
            student.Marks.Add(newMark);

            ctx.SaveChanges();

            return Ok();
        }

        /* • Obtinerea tuturor notelor unui student. */
        /// <summary>
        /// Returns all marks for selected student.
        /// </summary>
        /// <param name="id">Id of selected student.</param>
        /// <returns></returns>
        [HttpGet("all-marks-for/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<int>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetAllMarks([FromRoute] int id)
        {
            var student = ctx.Students.Include(m => m.Marks).Where(s => s.Id == id).FirstOrDefault();
            if (student == null)
            {
                return NotFound($"Student does not exist.");
            }

            var marks = student.Marks.Select(v => v.Value).ToList();

            return Ok(marks);
        }

        /*• Obtinerea notelor unui student pentru un anumit curs*/
        /// <summary>
        /// Returns all the marks for a selected student and subject.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        [HttpGet("all-marks-for/{studentId}/in-{subjectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<int>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetAllMarksForSpecificSubject([FromRoute] int studentId, [FromRoute] int subjectId)
        {
            var student = ctx.Students.Include(m => m.Marks).Where(s => s.Id == studentId).FirstOrDefault();
            if (student == null)
            {
                return NotFound($"Student does not exist.");
            }

            var subject = ctx.Subjects.Where(s => s.Id == subjectId).FirstOrDefault();
            if (subject == null)
            {
                return NotFound($"Subject does not exist.");
            }

            var marks = student.Marks.Where(s => s.SubjectId == subjectId).Select(v => v.Value).ToList();

            return Ok(marks);
        }

        /*• Obtinerea mediilor per materie ale unui student*/
        /// <summary>
        /// Returns a list of all averages for a selected student.
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpGet("all-average-for/{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetAllAveragesForOneStudent([FromRoute] int studentId)
        {
            var student = ctx.Students.Include(s => s.Subjects).Include(m => m.Marks).Where(s => s.Id == studentId).FirstOrDefault();
            if (student == null)
            {
                return NotFound($"Studnet does not exist.");
            }

            var marks = student.Marks.Where(m => m.SubjectId != null).Select(v => v.Value).ToList();
            if (marks.Count == 0)
            {
                return NotFound("Sutdent has no marks.");
            }

            var averages = student.Marks.Where(m => m.Subject.Name != null).GroupBy(m => m.Subject.Name).Select(
                g => new AverageForSubject
                {
                    Name= g.Key,
                    Average = g.Average(v => v.Value)
                }).ToList();

            return Ok(averages);
        }

        /* Obtinerea listei studentilor in ordine a mediilor
                • Ordinea este configurabila ascending/descending
                • DTO-ul va returna informatiile despre student, fara adresa, note,
                dar cu media generala calculate
        */
        /// <summary>
        /// Returns a list stundents and their avereage in ascending or descending order.
        /// </summary>
        /// <param name="ascendingOrder"></param>
        /// <returns></returns>
        [HttpGet("all-averages")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IOrderedEnumerable<StudentWithAverage>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetAllAveragesInOrder([FromQuery] bool ascendingOrder)
        {
            var students = ctx.Students.Include(s => s.Subjects).Include(m => m.Marks).ToList();
            if (students == null)
            {
                return NotFound($"There are no students in catalog.");
            }

            var subjects = ctx.Subjects.ToList();
            if (subjects == null)
            {
                return NotFound($"There are no subjects in catalog.");
            }

            List < StudentWithAverage > studentsWithAverages = new List<StudentWithAverage>();

            foreach (var student in students)
            {
                var average = student.Marks.Where(m => m.Subject.Name != null).GroupBy(m => m.Subject.Name).Select(
                g => new AverageForSubject
                {
                    Name = g.Key,
                    Average = g.Average(v => v.Value)
                }).ToList();

                studentsWithAverages.Add(new StudentWithAverage
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Average = average.Average(a => a.Average),
                });
            }

            IOrderedEnumerable<StudentWithAverage> studentsWithAveragesOrdered;

            if (ascendingOrder == true)
            {
                studentsWithAveragesOrdered = studentsWithAverages.OrderBy(a => a.Average);
                return Ok(studentsWithAveragesOrdered);
            }

            studentsWithAveragesOrdered = studentsWithAverages.OrderByDescending(a => a.Average);
            return Ok(studentsWithAveragesOrdered);
        }
        /*
         Obtinerea tuturor notelor acordate de catre un
         teacher:
               • Va returna o lista ce va contine DTO-uri continand
               valoarea notei, data acordarii precum si id-ul
               studentului
         */
        /// <summary>
        /// Returns a list of all marks given by a teacher.
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        [HttpGet("all-marks-from/{teacherId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetAllMarksGivenByTeacher([FromRoute] int teacherId)
        {
            var teacher = ctx.Teachers.Include(m => m.Subject).Where(s => s.Id == teacherId).FirstOrDefault();
            if (teacher == null)
            {
                return NotFound($"Teacher does not exist.");
            }

            var marks = ctx.Marks.Where(m => m.SubjectId == teacher.SubjectId).Select(v => v.Value).ToList();

            return Ok(marks);
        }
        /* Stergerea unui curs
             • Ce alte stergeri implica?*/

        /// <summary>
        /// Deletes a selected subjects. The teacher remains with NULL to SubjectId.
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="keepMarks">If true, all marks from the deleted subject remain with NULL to SubjectId.</param>
        /// <returns></returns>
        [HttpDelete("delete-subject/{subjectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult DeleteSubject([FromBody] int subjectId, [FromQuery] bool keepMarks)
        {
            var subjectToDelete = ctx.Subjects.Include(m => m.Marks).Include(t => t.Teacher).Where(s => s.Id == subjectId).FirstOrDefault();
            if (subjectToDelete == null)
            {
                return NotFound("Subject not found.");
            }

            if (keepMarks != true)
            {
                var marks = ctx.Marks.Where(m => m.SubjectId == subjectId).ToList();
                ctx.Marks.RemoveRange(marks);
            }

            ctx.Subjects.Remove(subjectToDelete);
            ctx.SaveChanges();

            return Ok();
        }
    }
}
