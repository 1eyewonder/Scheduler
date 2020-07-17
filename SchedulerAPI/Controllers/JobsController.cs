using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchedulerAPI.Data;
using SchedulerAPI.Dtos;
using SchedulerAPI.Models;
using System.Linq;
using System.Net;

namespace SchedulerAPI.Controllers
{
    /// <summary>
    /// Controller for entries in the Jobs table
    /// </summary>
    /// Some actions also create, edit, and delete objects in the QuoteRevisions
    /// and JobRevisions tables since they are child entities
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobsController : ControllerBase
    {
        private readonly SchedulerContext _context;

        public JobsController(SchedulerContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var job = _context.Jobs.Find(id);

            if (job == null) return NotFound("No record found with this id");

            return Ok(job);
        }

        /// <summary>
        /// Returns entries in Jobs table
        /// </summary>
        /// <param name="sort">Sorts by job number: "asc" for ascending and "desc" for descending</param>
        /// <param name="pageNumber">Number of pages data is split into</param>
        /// <param name="pageSize">Data request size per page</param>
        /// <param name="onlyJobs">True to show only quotes</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult GetJobs()
        {
            var jobs = _context.Jobs;

            //If successfull request
            if (jobs != null)
            {
                return Ok(jobs);
            }

            //Could not connect to data context
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult FindJobs(string jobNo)
        {
            var jobs = _context.Jobs.Where(j => j.JobNumber.StartsWith(jobNo));

            if (jobs == null)
            {
                return NotFound();
            }

            return Ok(jobs);
        }

        [HttpPost]
        [Authorize(Roles = "1,2")]
        public IActionResult Post([FromBody] JobDto jobEntry)
        {
            //If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Translates values from dto
            var job = new Job
            {
                QuoteNumber = jobEntry.QuoteNumber,
                JobNumber = jobEntry.JobNumber,
                ProjectNumber = jobEntry.ProjectNumber
            };

            //Returns true if not null
            job.IsAJob = jobEntry.JobNumber != null;

            //Adds entry to db
            _context.Jobs.Add(job);

            //Adds quote revision upon quote creation that is tied to Job.Id
            var quoteRev = new QuoteRevision
            {
                RevisionNumber = 0,
                RevisionSummary = null
            };

            //Add to collection after job is in db will utilize entity to create child objects for you
            job.QuoteRevisions.Add(quoteRev);

            //Starts job revision if entered as a job from the get go
            if (jobEntry.JobNumber != null)
            {
                var jobRev = new JobRevision
                {
                    RevisionNumber = 0,
                    RevisionSummary = null
                };

                job.JobRevisions.Add(jobRev);
            }

            _context.SaveChanges();
            return StatusCode(201, HttpStatusCode.Created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1,2")]
        public IActionResult Put(int id, [FromBody] JobDto job)
        {
            //If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbJob = _context.Jobs.Find(id);

            if (dbJob == null)
            {
                return BadRequest("No record found against this id");
            }

            //checks if job number is changing from null to non-null
            if (dbJob.JobNumber == null && job.JobNumber != null)
            {
                //Creates entry in job revision table
                var jobRev = new JobRevision
                {
                    RevisionNumber = 0,
                    RevisionSummary = null
                };

                //Adds to collection and entity creates new entry
                dbJob.JobRevisions.Add(jobRev);
            }

            //Update values
            dbJob.QuoteNumber = job.QuoteNumber;
            dbJob.JobNumber = job.JobNumber;
            dbJob.ProjectNumber = job.ProjectNumber;
            dbJob.IsAJob = job.JobNumber != null;

            //Save changes
            _context.SaveChanges();

            return Ok("Record updated successfully");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "2")]
        public IActionResult Delete(int id)
        {
            //If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Queries entity
            var job = _context.Jobs.Include(j => j.JobRevisions).Include(q => q.QuoteRevisions).Where(i => i.Id == id).Single();

            if (job == null)
            {
                return BadRequest("There is no record of this id");
            }

            //Remove parent entry
            _context.Jobs.Remove(job);

            //Clears child entries
            _context.JobRevisions.Remove(job.JobRevisions.Single());
            _context.QuoteRevisions.Remove(job.QuoteRevisions.Single());
            _context.SaveChanges();

            return Ok("Record successfully deleted");
        }
    }
}