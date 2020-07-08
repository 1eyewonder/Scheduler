using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchedulerRest.Data;
using SchedulerRest.Models;

namespace SchedulerRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly SchedulerContext _context;

        public JobsController(SchedulerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var jobs = _context.Jobs;

            if (jobs != null)
            {
                return Ok(jobs);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var job = _context.Jobs.Where(i => i.Id == id).First();

            if (job == null) return NotFound("No record found with this id");

            return Ok(job);
        }

        [HttpPost]
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
    }
}
