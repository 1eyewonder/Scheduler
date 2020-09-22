using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchedulerAPI.Data;
using SchedulerAPI.Dtos;
using SchedulerAPI.Helpers;
using SchedulerAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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

        [HttpGet("[action]")]
        public async Task<ActionResult<List<JobRevision>>> GetJobRevisions([FromQuery] PaginationDto pagination)
        {
            // Gets data from context
            var jobRevs = _context.JobRevisions.AsQueryable();

            // Paginates the list of data
            await HttpContext.InsertPaginationParameterInResponse(jobRevs, pagination.QuantityPerPage);
            var item = await jobRevs.Paginate(pagination).ToListAsync();
            return Ok(item);
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