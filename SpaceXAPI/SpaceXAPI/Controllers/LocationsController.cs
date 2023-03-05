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
    [Route("api/v1/locations")]
    public class LocationsController : Controller
    {
        private readonly SpaceXContext _context;

        public LocationsController(SpaceXContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllLocations(string name, string region, int? page, string sort, int length = 2, string dir = "asc")
        {
            IQueryable<Location> query = _context.Locations;

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(l => l.Name == name);

            if (!string.IsNullOrWhiteSpace(region))
                query = query.Where(l => l.Region == region);

            if (page.HasValue)
            {
                query = query.Skip(page.Value * length);
                query = query.Take(length);
            }

            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort)
                {
                    case "name":
                        if (dir == "asc")
                            query = query.OrderBy(l => l.Name);
                        else if (dir == "desc")
                            query = query.OrderByDescending(l => l.Name);
                        break;
                    case "region":
                        if (dir == "asc")
                            query = query.OrderBy(l => l.Region);
                        else if (dir == "desc")
                            query = query.OrderByDescending(l => l.Region);
                        break;
                }
            }

            var result = query.ToList();
            if (!result.Any())
                return NotFound();

            return Ok(result);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetLocation(int id)
        {
            var location = _context.Locations.FirstOrDefault<Location>(l => l.Id == id);
            if (location == null)
                return NotFound();
            return Ok(location);
        }

        [HttpPost]
        public IActionResult CreateLocation([FromBody] Location newLocation)
        {
            _context.Locations.Add(newLocation);
            _context.SaveChanges();

            return Created("", newLocation);
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteLocation(int id)
        {
            var location = _context.Locations.Find(id);
            if (location == null)
                return NotFound();
            _context.Locations.Remove(location);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateLocation([FromBody] Location updateLocation)
        {
            var location = _context.Locations.Find(updateLocation.Id);
            if (location == null)
                return NotFound();
            location.Name = updateLocation.Name;
            location.Region = updateLocation.Region;
            _context.SaveChanges();
            return Ok(location);
        }
    }
}