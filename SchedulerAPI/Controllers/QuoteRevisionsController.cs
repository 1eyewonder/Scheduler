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
    public class QuoteRevisionsController : ControllerBase
    {
        private readonly SchedulerContext _context;

        public QuoteRevisionsController(SchedulerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var quoteRevs = _context.QuoteRevisions;

            if (quoteRevs == null) return BadRequest();

            return Ok(quoteRevs);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var quoteRev = _context.QuoteRevisions.Find(id);

            if (quoteRev == null) return NotFound("No record found against this id");

            return Ok(quoteRev);
        }

        [HttpPost]
        [Authorize(Roles = "2")] //Left as DbAdmin to ensure entrypoint is from JobsController.Post() or Put()
        public IActionResult Post([FromBody] QuoteRevision quoteRevision)
        {
            //If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var quoteRev = new QuoteRevision
            {
                RevisionNumber = quoteRevision.RevisionNumber,
                RevisionSummary = quoteRevision.RevisionSummary
            };

            _context.QuoteRevisions.Add(quoteRev);
            _context.SaveChanges();
            return StatusCode(201, HttpStatusCode.Created);
        }

        [HttpPut("{action}")]
        [Authorize(Roles = "1,2")]
        public IActionResult RevUpQuote(int id)
        {
            var quoteRev = _context.QuoteRevisions.Find(id);

            if (quoteRev == null) return NotFound("No record found against this id");

            quoteRev.RevisionNumber += 1;

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

            var quoteRev = _context.QuoteRevisions.Where(i => i.Id == id).Single();

            if (quoteRev == null)
            {
                return BadRequest("No record found against this id");
            }

            //Updates properties
            quoteRev.RevisionNumber = quoteRevision.RevisionNumber;
            quoteRev.RevisionSummary = quoteRevision.RevisionSummary;

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

            var quoteRev = _context.QuoteRevisions.Find(id);

            if (quoteRev == null) return BadRequest("There is no record of this id");

            //Remove entry and save changes
            _context.QuoteRevisions.Remove(quoteRev);
            _context.SaveChanges();

            return Ok("Quote successfully deleted");
        }
    }
}