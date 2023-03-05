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
    [Route("api/v1/launchcontracts")]
    public class LaunchContractsController : Controller
    {
        private readonly SpaceXContext context;

        public LaunchContractsController(SpaceXContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllLaunchContracts(int? flightNumber, string customer, int? page, int length = 2)
        {
            IQueryable<LaunchContract> query = GetLaunchContractsWithIncludedProperties();

            if (flightNumber.HasValue)
                query = query.Where(c => c.Launch.FlightNumber == flightNumber);
            if (!string.IsNullOrWhiteSpace(customer))
                query = query.Where(c => c.Customer.Name == customer);

            if (page.HasValue)
            {
                query = query.Skip(page.Value * length);
                query = query.Take(length);
            }

            var result = query.ToList();
            if (!result.Any())
                return NotFound();

            return Ok(result);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetLaunchContract(int id)
        {
            var launchContract = GetLaunchContractsWithIncludedProperties().FirstOrDefault(c => c.Id == id);
            if (launchContract == null)
                return NotFound();
            return Ok(launchContract);
        }

        [HttpPost]
        public IActionResult CreateLaunchContract([FromBody] LaunchContract launchContract)
        {
            context.Add(launchContract);
            context.SaveChanges();
            return Created("", GetLaunchContractsWithIncludedProperties().FirstOrDefault(c => c.Id == launchContract.Id));
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteLaunchContract(int id)
        {
            var launchContract = context.LaunchContracts.Find(id);
            if (launchContract == null)
                return NotFound();
            context.LaunchContracts.Remove(launchContract);
            context.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateLaunchContract([FromBody] LaunchContract newLaunchContract)
        {
            var launchContract = GetLaunchContractsWithIncludedProperties().FirstOrDefault(c => c.Id == newLaunchContract.Id);
            if (launchContract == null)
                return NotFound();
            launchContract.LaunchId = newLaunchContract.LaunchId;
            launchContract.CustomerId = newLaunchContract.CustomerId;
            context.SaveChanges();
            return Ok(GetLaunchContractsWithIncludedProperties().FirstOrDefault(c => c.Id == newLaunchContract.Id));
        }

        private IQueryable<LaunchContract> GetLaunchContractsWithIncludedProperties()
        {
            return context.LaunchContracts.Include(c => c.Launch).Include(c => c.Launch.Rocket).Include(c => c.Launch.Location).Include(c => c.Customer);
        }
    }
}