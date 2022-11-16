using Data.Models;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;

namespace CatalogApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupAndRestoreController : ControllerBase
    {
        private readonly CatalogueDbContext ctx;
        private readonly DataLayer dataLayer;
        
        public BackupAndRestoreController(CatalogueDbContext ctx, DataLayer dataLayer)
        {
            this.ctx = ctx;
            this.dataLayer = dataLayer;
        }


        [HttpPut("full-backup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult FullBackup()
        {
            dataLayer.FullBackup();
            return Ok();
        }

        [HttpPut("full-restore")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult FullRestore()
        {
            dataLayer.FullRestore();
            return Ok();
        }


        [HttpPut("backup-students")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult BackupStudents()
        {
            dataLayer.BackupStudents();
            return Ok();
        }
        
        [HttpPut("restore-students")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult RestoreStudents()
        {
            dataLayer.RestoreStudents();
            return Ok();
        }

        [HttpPut("backup-subjects")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult BackupSubjects()
        {
            dataLayer.BackupSubjects();
            return Ok();
        }

        [HttpPut("restore-subjects")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult RestoreSubjects()
        {
            dataLayer.RestoreSubjects();
            return Ok();
        }

        [HttpPut("backup-addresses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult BackupAddresses()
        {
            dataLayer.BackupAddresses();
            return Ok();
        }

        [HttpPut("restore-addresses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult RestoreAddresses()
        {
            dataLayer.RestoreAddresses();
            return Ok();
        }

        [HttpPut("backup-marks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult BackupMarks()
        {
            dataLayer.BackupMarks();
            return Ok();
        }

        [HttpPut("restore-marks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult RestoreMarks()
        {
            dataLayer.RestoreMarks();
            return Ok();
        }

        [HttpPut("backup-teachers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult BackupTeachers()
        {
            dataLayer.BackupTeacherss();
            return Ok();
        }

        [HttpPut("restore-teachers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult RestoreTeachers()
        {
            dataLayer.RestoreTeachers();
            return Ok();
        }
    }
}
