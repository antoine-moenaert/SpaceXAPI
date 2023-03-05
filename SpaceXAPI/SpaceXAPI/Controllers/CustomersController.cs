using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceXAPI.Data;
using SpaceXAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXAPI.Controllers
{
    [ApiController]
    [Route("api/v1/customers")]
    public class CustomersController : Controller
    {
        private readonly SpaceXContext context;

        public CustomersController(SpaceXContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetCustomers(int? page, string name, string dir, int length = 3)
        {
            IQueryable<Customer> query = context.Customers;

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(c => c.Name == name);

            if (page.HasValue)
            {
                query = query.Skip(page.Value * length);
                query = query.Take(length);
            }

            if (dir == "asc")
                query = query.OrderBy(c => c.Name);
            else if (dir == "desc")
                query = query.OrderByDescending(c => c.Name);

            var result = query.ToList();
            if (!result.Any())
                return NotFound();

            return Ok(query);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetCustomer(int id)
        {
            var customer = context.Customers.Find(id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        [Route("{id}/launches")]
        [HttpGet]
        public IActionResult GetCustomerLaunches(int id)
        {
            var contracts = context.LaunchContracts.Include(c => c.Launch).ThenInclude(l => l.Rocket)
                .Include(c => c.Launch).ThenInclude(l => l.Location).Where(c => c.CustomerId == id);
            if (!contracts.Any())
                return NotFound();
            var launches = new List<Launch>();
            foreach (LaunchContract c in contracts)
            {
                if (c.Launch == null)
                    return NotFound();
                launches.Add(c.Launch);
            }
            return Ok(launches);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Customer newCustomer)
        {
            context.Customers.Add(newCustomer);
            context.SaveChanges();
            return Created("", newCustomer);
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = context.Customers.Find(id);
            if (customer == null)
                return NotFound();
            context.Customers.Remove(customer);
            context.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateCustomer([FromBody] Customer newCustomer)
        {
            var customer = context.Customers.Find(newCustomer.Id);
            if (customer == null)
                return NotFound();
            customer.Name = newCustomer.Name;
            context.SaveChanges();
            return Ok(customer);
        }
    }
}
