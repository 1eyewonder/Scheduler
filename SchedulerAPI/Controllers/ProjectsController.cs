using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchedulerAPI.Data;
using SchedulerAPI.Dtos;
using SchedulerAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SchedulerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly SchedulerContext _context;
        private readonly IMapper _mapper;

        public ProjectsController(SchedulerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            //Returns project dbcontext
            return Ok(_context.Projects.Include(c=>c.Customer));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            //Attempts to find project with given id
            var project = _context.Projects.Include(c => c.Customer).Where(i => i.Id == id).Single();

            //If not found
            if (project == null) return NotFound("No record found with this id");

            return Ok(project);
        }

        [HttpPost]
        [Authorize(Roles = "1,2")]
        public IActionResult Post([FromBody] ProjectDto project)
        {
            //If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Create new project class and pass data into it from dto
            var dbProject = new Project();
            _mapper.Map(project, dbProject);

            //Add entry to db
            _context.Projects.Add(dbProject);
            
            //Save changes
            _context.SaveChanges();
            return StatusCode(201, HttpStatusCode.Created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1,2")]
        public IActionResult Put(int id, [FromBody] ProjectDto project)
        {
            //If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Attempts to find project with given id
            var dbProject = _context.Projects.Include(c => c.Customer).Where(i => i.Id == id).Single();

            //If no result
            if (dbProject == null)
            {
                return BadRequest("No record found against this id");
            }

            //Updates entry in db
            _mapper.Map(project, dbProject);

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

            //Attempts to find project with given id
            var project = _context.Projects.Include(c => c.Customer).Where(i => i.Id == id).Single();

            //If not found
            if (project == null)
            {
                return BadRequest("There is no record of this id");
            }

            //Removes entry
            _context.Projects.Remove(project);

            //Save changes
            _context.SaveChanges();

            return Ok("Role successfully deleted");
        }
    }
}