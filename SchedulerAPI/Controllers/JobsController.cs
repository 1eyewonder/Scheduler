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
        public IActionResult AllJobs(string sort = null, int pageNumber = 5, int pageSize = 20, bool onlyJobs = false)
        {
            var jobs = _context.Jobs;

            //If successfull request
            if (jobs != null)
            {
                //Returns only quotes
                if (onlyJobs == true)
                {
                    //Orders based on job number
                    return sort switch
                    {
                        "desc" => Ok(jobs.OrderByDescending(j => j.JobNumber).Where(x => x.JobNumber != null).Skip((pageNumber - 1) * pageSize).Take(pageSize)),
                        "asc" => Ok(jobs.OrderBy(j => j.JobNumber).Where(x => x.JobNumber != null).Skip((pageNumber - 1) * pageSize).Take(pageSize)),
                        _ => Ok(jobs.Where(x => x.JobNumber != null).Skip((pageNumber - 1) * pageSize).Take(pageSize)),
                    };
                }

                //Returns all entries
                else
                {
                    //Orders based on job number
                    return sort switch
                    {
                        "desc" => Ok(jobs.OrderByDescending(j => j.JobNumber).Skip((pageNumber - 1) * pageSize).Take(pageSize)),
                        "asc" => Ok(jobs.OrderBy(j => j.JobNumber).Skip((pageNumber - 1) * pageSize).Take(pageSize)),
                        _ => Ok(jobs.Skip((pageNumber - 1) * pageSize).Take(pageSize)),
                    };
                }
            }

            //Could not connect to data context
            else
            {
                return BadRequest();
            }
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
        public IActionResult AllQuotes(string sort = null, int pageNumber = 5, int pageSize = 20, bool onlyJobs = false)
        {
            var jobs = _context.Jobs;

            //If successfull request
            if (jobs != null)
            {
                //Returns only quotes
                if (onlyJobs == true)
                {
                    //Orders based on quote number
                    return sort switch
                    {
                        "desc" => Ok(jobs.OrderByDescending(j => j.QuoteNumber).Where(x => x.QuoteNumber != null).Skip((pageNumber - 1) * pageSize).Take(pageSize)),
                        "asc" => Ok(jobs.OrderBy(j => j.QuoteNumber).Where(x => x.QuoteNumber != null).Skip((pageNumber - 1) * pageSize).Take(pageSize)),
                        _ => Ok(jobs.Where(x => x.QuoteNumber != null).Skip((pageNumber - 1) * pageSize).Take(pageSize)),
                    };
                }

                //Returns all entries
                else
                {
                    //Orders based on quote number
                    return sort switch
                    {
                        "desc" => Ok(jobs.OrderByDescending(j => j.QuoteNumber).Skip((pageNumber - 1) * pageSize).Take(pageSize)),
                        "asc" => Ok(jobs.OrderBy(j => j.QuoteNumber).Skip((pageNumber - 1) * pageSize).Take(pageSize)),
                        _ => Ok(jobs.Skip((pageNumber - 1) * pageSize).Take(pageSize)),
                    };
                }
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
        public IActionResult Post([FromBody] Job job)
        {
            //If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Jobs.Add(job);
            _context.SaveChanges();
            return StatusCode(201, HttpStatusCode.Created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1,2")]
        public IActionResult Put(int id, [FromBody] Job job)
        {
            //If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbJob = _context.Jobs.Where(i => i.Id == id).First();

            if (dbJob == null)
            {
                return BadRequest("No record found against this id");
            }

            //Update values
            dbJob.QuoteNumber = job.QuoteNumber;
            dbJob.JobNumber = job.JobNumber;
            dbJob.IsAJob = job.IsAJob;

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

            var job = _context.Jobs.Find(id);

            if (job == null)
            {
                return BadRequest("There is no record of this id");
            }

            //Remove entry and save changes
            _context.Jobs.Remove(job);
            _context.SaveChanges();

            return Ok("Quote successfully deleted");
        }
    }
}