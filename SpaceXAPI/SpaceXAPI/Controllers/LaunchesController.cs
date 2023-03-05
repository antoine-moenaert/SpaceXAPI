using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceXAPI.Data;
using SpaceXAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXAPI.Controllers
{
    [ApiController]
    [Route("api/v1/launches")]
    public class LaunchesController : Controller
    {
        private readonly SpaceXContext context;

        public LaunchesController(SpaceXContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllLaunches(int? flightNumber, string missionName, string rocket, string location, int? page, string header = "flightNumber", int length = 2, string dir = "asc")
        {
            IQueryable<Launch> query = GetLaunchesWithIncludedProperties();

            if (flightNumber.HasValue)
                query = query.Where(l => l.FlightNumber == flightNumber);
            if (!string.IsNullOrWhiteSpace(missionName))
                query = query.Where(l => l.MissionName.Contains(missionName));
            if (!string.IsNullOrWhiteSpace(rocket))
                query = query.Where(l => l.Rocket.Name == rocket);
            if (!string.IsNullOrWhiteSpace(location))
                query = query.Where(l => l.Location.Name == location);

            if (!string.IsNullOrWhiteSpace(header))
            {
                switch (header)
                {
                    case "flightNumber":
                        if (dir == "asc")
                            query = query.OrderBy(l => l.FlightNumber);
                        else if (dir == "desc")
                            query = query.OrderByDescending(l => l.FlightNumber);
                        break;
                    case "missionName":
                        if (dir == "asc")
                            query = query.OrderBy(l => l.MissionName);
                        else if (dir == "desc")
                            query = query.OrderByDescending(l => l.MissionName);
                        break;
                }
            }

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

        [Route("count")]
        [HttpGet]
        public IActionResult GetCount()
        {
            return Ok(context.Launches.Count());
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetLaunch(int id)
        {
            var launch = GetLaunchesWithIncludedProperties().SingleOrDefault(l => l.Id == id);
            if (launch == null)     
                return NotFound();
            return Ok(launch);
        }

        [Route("{id}/rocket")]
        [HttpGet]
        public IActionResult GetLaunchRocket(int id)
        {
            var launch = GetLaunchesWithIncludedProperties().SingleOrDefault(l => l.Id == id);
            if (launch == null)
                return NotFound();
            return Ok(launch.Rocket);
        }

        [Route("{id}/location")]
        [HttpGet]
        public IActionResult GetLaunchLocation(int id)
        {
            var launch = GetLaunchesWithIncludedProperties().SingleOrDefault(l => l.Id == id);
            if (launch == null)
                return NotFound();
            return Ok(launch.Location);
        }

        [Route("{id}/customers")]
        [HttpGet]
        public IActionResult GetLaunchCustomers(int id)
        {
            var contracts = context.LaunchContracts.Include(c => c.Customer).Where(c => c.LaunchId == id);
            if (!contracts.Any())
                return NotFound();
            var customers = new List<Customer>();
            foreach (LaunchContract c in contracts)
            {
                if (c.Customer == null)
                    return NotFound();
                customers.Add(c.Customer);
            }
            return Ok(customers);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateLaunch([FromBody] Launch newLaunch)
        {
            context.Launches.Add(newLaunch);
            context.SaveChanges();
            return Created("", GetLaunchesWithIncludedProperties().SingleOrDefault(l => l.Id == newLaunch.Id));
        }

        [Route("{id}")]
        [HttpDelete]
        [Authorize]
        public IActionResult DeleteLaunch(int id)
        {
            var launch = context.Launches.Find(id);
            if (launch == null)
                return NotFound();
            context.Launches.Remove(launch);
            context.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateLaunch([FromBody] Launch newLaunch)
        {
            var launch = context.Launches.Find(newLaunch.Id);
            if (launch == null)
                return NotFound();
            launch.FlightNumber = newLaunch.FlightNumber;
            launch.MissionName = newLaunch.MissionName;
            //launch.Customer = newLaunch.Customer;
            launch.LocationId = newLaunch.LocationId;
            launch.RocketId = newLaunch.RocketId;
            context.SaveChanges();
            return Ok(launch);
        }

        private IQueryable<Launch> GetLaunchesWithIncludedProperties()
        {
            return context.Launches.Include(l => l.Rocket).Include(l => l.Location);
        }
    }
}