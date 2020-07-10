using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedulerAPI.Data;
using SchedulerAPI.Dtos;
using SchedulerAPI.Models;
using System.Linq;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchedulerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly SchedulerContext _context;

        public RolesController(SchedulerContext context)
        {
            _context = context;
        }

        // GET: api/<RolesController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok(_context.Roles);
        }

        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var role = _context.Roles.Find(id);

            if (role == null) return NotFound("No record found with this id");

            return Ok(role);
        }

        // POST api/<RolesController>
        [HttpPost]
        public IActionResult Post([FromBody] RoleDto role)
        {
            //If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbRole = new Role
            {
                UserRole = role.UserRole,
                AuthorizationLevel = 0 //defaults to basic user privileges
            };

            _context.Roles.Add(dbRole);
            _context.SaveChanges();
            return StatusCode(201, HttpStatusCode.Created);
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1,2")]
        public IActionResult Put(int id, [FromBody] Role role)
        {
            //If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbRole = _context.Roles.Where(i => i.Id == id).First();

            if (dbRole == null)
            {
                return BadRequest("No record found against this id");
            }

            //Updates properties
            dbRole.UserRole = role.UserRole;
            dbRole.AuthorizationLevel = role.AuthorizationLevel;

            //Save changes
            _context.SaveChanges();

            return Ok("Record updated successfully");
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "2")]
        public IActionResult Delete(int id)
        {
            //If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = _context.Roles.Find(id);

            if (role == null)
            {
                return BadRequest("There is no record of this id");
            }

            //Remove entry and save changes
            _context.Roles.Remove(role);
            _context.SaveChanges();

            return Ok("Quote successfully deleted");
        }
    }
}