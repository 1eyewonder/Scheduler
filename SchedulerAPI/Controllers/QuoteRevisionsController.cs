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
    public class QuoteRevisionsController : ControllerBase
    {
        private readonly SchedulerContext _context;

        public QuoteRevisionsController(SchedulerContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<QuoteRevision>>> GetQuoteRevisions([FromQuery] PaginationDto pagination)
        {
            // Gets data from context
            var quoteRevs = _context.QuoteRevisions.AsQueryable();

            // Paginates the list of data
            await HttpContext.InsertPaginationParameterInResponse(quoteRevs, pagination.QuantityPerPage);
            var item = await quoteRevs.Paginate(pagination).ToListAsync();
            return Ok(item);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // Attempts to find revision
            var quoteRev = _context.QuoteRevisions.Find(id);

            // If not found
            if (quoteRev == null) return NotFound("No record found against this id");

            // Returns item to user
            return Ok(quoteRev);
        }

        [HttpPost]
        [Authorize(Roles = "2")] //Left as DbAdmin to ensure entrypoint is from JobsController.Post() or Put()
        public IActionResult Post([FromBody] QuoteRevision quoteRevision)
        {
            // If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Creates object from user request
            var quoteRev = new QuoteRevision
            {
                JobId = quoteRevision.JobId,
                RevisionNumber = quoteRevision.RevisionNumber,
                RevisionSummary = quoteRevision.RevisionSummary
            };

            // Adds to context, saves, and returns successful message
            _context.QuoteRevisions.Add(quoteRev);
            _context.SaveChanges();
            return StatusCode(201, HttpStatusCode.Created);
        }

        [HttpPut("{action}")]
        [Authorize(Roles = "1,2")]
        public IActionResult RevUpQuote(int id)
        {
            // Attempts to find revision with given id
            var quoteRev = _context.QuoteRevisions.Find(id);

            // If not found
            if (quoteRev == null) return NotFound("No record found against this id");

            // Find job reference
            var jobId = quoteRev.JobId;
            var job = _context.Jobs.Find(jobId);

            // Checks if job reference is a job. If yes, then denies user request
            if (job.IsAJob == true)
            {
                return BadRequest("No additional quote revisions are needed since this is already a job");
            }      

            // Save changes and return successful message
            _context.SaveChanges();
            return Ok("Quote successfully revised");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1,2")]
        public IActionResult Put(int id, [FromBody] QuoteRevision quoteRevision)
        {
            //If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Attempts to find quote revision with given id
            var quoteRev = _context.QuoteRevisions.Find(id);

            // If nothing is found
            if (quoteRev == null)
            {
                return BadRequest("No record found against this id");
            }

            // Updates properties
            quoteRev.RevisionNumber = quoteRevision.RevisionNumber;
            quoteRev.RevisionSummary = quoteRevision.RevisionSummary;

            // Save changes and return successful message
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

            // Attempts to find quote revision with given id
            var quoteRev = _context.QuoteRevisions.Find(id);

            // If nothing found
            if (quoteRev == null) return BadRequest("There is no record of this id");

            //Remove entry and save changes
            _context.QuoteRevisions.Remove(quoteRev);
            _context.SaveChanges();
            return Ok("Quote successfully deleted");
        }
    }
}