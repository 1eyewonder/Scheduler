using AutoMapper;
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
        private readonly IMapper _mapper;

        public JobsController(SchedulerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            //Attempts to get entry with given id
            var job = _context.Jobs.Include(q => q.QuoteRevisions)
                .Include(j => j.JobRevisions)
                .Include(p => p.Project)
                .ThenInclude(c => c.Customer)
                .Where(i=>i.Id == id)
                .Single();

            //If not found
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
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetJobs()
        {
            //Attempts to return dbset
            var jobs = _context.Jobs.Include(q=>q.QuoteRevisions)
                .Include(j=>j.JobRevisions)
                .Include(p=>p.Project)
                .ThenInclude(c=>c.Customer);

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
            //Searches for entries based on job number
            var jobs = _context.Jobs.Include(q => q.QuoteRevisions)
                .Include(j => j.JobRevisions)
                .Include(p => p.Project)
                .ThenInclude(c => c.Customer)
                .Where(j => j.JobNumber.StartsWith(jobNo));

            //If no matching results
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
            var job = new Job();
            _mapper.Map(jobEntry, job);

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

                //Adds object to collection
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

            //Attempts to find entry with given id
            var dbJob = _context.Jobs.Include(q => q.QuoteRevisions)
                .Include(j => j.JobRevisions)
                .Include(p => p.Project)
                .ThenInclude(c => c.Customer)
                .Where(i => i.Id == id)
                .Single();

            //If not found
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
            _mapper.Map(job, dbJob);
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

            //Attempts to find entry with given id
            var job = _context.Jobs.Include(q => q.QuoteRevisions)
                .Include(j => j.JobRevisions)
                .Where(i => i.Id == id)
                .Single();

            //If not found
            if (job == null)
            {
                return BadRequest("There is no record of this id");
            }

            //Remove parent entry
            _context.Jobs.Remove(job);

            //Clears child entries and save changes
            //Project is not removed since it is not job specific
            if (job.JobRevisions.Count > 0)
            {
                _context.JobRevisions.Remove(job.JobRevisions.Single());
            }

            if (job.QuoteRevisions.Count > 0)
            {
                _context.QuoteRevisions.Remove(job.QuoteRevisions.Single());
            }


            //Save changes           
            _context.SaveChanges();
            return Ok("Record successfully deleted");
        }
    }
}