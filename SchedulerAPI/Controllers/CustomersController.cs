using AutoMapper;
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
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly SchedulerContext _context;
        private readonly IMapper _mapper;

        public CustomersController(SchedulerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Customer>>> GetCustomers([FromQuery] PaginationDto pagination)
        {
            // Gets data from context
            var customers = _context.Customers.AsQueryable();

            // Paginates the list of data
            await HttpContext.InsertPaginationParameterInResponse(customers, pagination.QuantityPerPage);
            var item = await customers.Paginate(pagination).ToListAsync();
            return Ok(item);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            //Attempts to find customer with given id
            var customer = _context.Customers.Find(id);

            //If not found
            if (customer == null) return NotFound("No record found with this id");

            return Ok(customer);
        }

        [HttpPost]
        [Authorize(Roles = "1,2")]
        public IActionResult Post([FromBody] Customer customer)
        {
            //If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Adds customer and saves changes
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return StatusCode(201, HttpStatusCode.Created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1,2")]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            //If model state does not match
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Attempts to find customer with given id
            var dbCustomer = _context.Customers.Where(i => i.Id == id).Single();

            //If no result
            if (dbCustomer == null)
            {
                return BadRequest("No record found against this id");
            }

            //Updates properties
            dbCustomer.Name = customer.Name;

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

            //Attempts to find customer with given id
            var customer = _context.Customers.Find(id);

            //If not found
            if (customer == null)
            {
                return BadRequest("There is no record of this id");
            }

            //Remove entry and save changes
            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return Ok("Role successfully deleted");
        }
    }
}