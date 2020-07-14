using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedulerAPI.Data;
using SchedulerAPI.Models;
using System.Linq;
using System.Net;

namespace SchedulerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobRevisionsController : ControllerBase
    {
        private readonly SchedulerContext _context;

        public JobRevisionsController(SchedulerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var jobRevs = _context.JobRevisions;

            if (jobRevs == null) return BadRequest();

            return Ok(jobRevs);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var jobRev = _context.JobRevisions.Find(id);

            if (jobRev == null) return NotFound("No record found against this id");

            return Ok(jobRev);
        }

        [HttpPost]
        [Authorize(Roles = "2")] //Left as DbAdmin to ensure entrypoint is from JobsController.Post() or Put()
        public IActionResult Post([FromBody] JobRevision jobRevision)
        {
            //If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobRev = new JobRevision
            {
                RevisionNumber = jobRevision.RevisionNumber,
                RevisionSummary = jobRevision.RevisionSummary
            };

            _context.JobRevisions.Add(jobRev);
            _context.SaveChanges();
            return StatusCode(201, HttpStatusCode.Created);
        }

        [HttpPut("{action}")]
        [Authorize(Roles = "1,2")]
        public IActionResult RevUpQuote(int id)
        {
            var jobRev = _context.JobRevisions.Find(id);

            if (jobRev == null) return NotFound("No record found against this id");

            jobRev.RevisionNumber += 1;

            return Ok("Quote successfully revised");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1,2")]
        public IActionResult Put(int id, [FromBody] JobRevision jobRevision)
        {
            //If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobRev = _context.JobRevisions.Where(i => i.Id == id).Single();

            if (jobRev == null)
            {
                return BadRequest("No record found against this id");
            }

            //Updates properties
            jobRev.RevisionNumber = jobRevision.RevisionNumber;
            jobRev.RevisionSummary = jobRevision.RevisionSummary;

            //Save changes
            _context.SaveChanges();

            return Ok("Record updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobRev = _context.JobRevisions.Find(id);

            if (jobRev == null) return BadRequest("There is no record of this id");

            //Remove entry and save changes
            _context.JobRevisions.Remove(jobRev);
            _context.SaveChanges();

            return Ok("Quote successfully deleted");
        }
    }
}