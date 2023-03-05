using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceXAPI.Data;
using SpaceXAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SpaceXAPI.Controllers
{
    [ApiController]
    [Route("api/v1/rockets")]
    public class RocketsController : Controller
    {
        private readonly SpaceXContext _context;
        
        public RocketsController(SpaceXContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllRockets(double? height, int? page, string sort, int length = 2, string dir = "asc")
        {
            IQueryable<Rocket> query = _context.Rockets;

            if (height.HasValue)
                query = query.Where(r => r.Height == height);

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
                            query = query.OrderBy(r => r.Name);
                        else if (dir == "desc")
                            query = query.OrderByDescending(r => r.Name);
                        break;
                    case "height":
                        if (dir == "asc")
                            query = query.OrderBy(r => r.Height);
                        else if (dir == "desc")
                            query = query.OrderByDescending(r => r.Height);
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
        public IActionResult GetRocket(int id)
        {
            var rocket = _context.Rockets.Find(id);
            if (rocket == null)
                return NotFound();
            return Ok(rocket);
        }

        [HttpPost]
        public IActionResult CreateRocket([FromBody] Rocket newRocket)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _context.Rockets.Add(newRocket);
            _context.SaveChanges();
            return Created("", newRocket);
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteRocket(int id)
        {
            var rocket = _context.Rockets.Find(id);
            if (rocket == null)
                return NotFound();
            _context.Rockets.Remove(rocket);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateRocket([FromBody] Rocket updateRocket)
        {
            var rocket = _context.Rockets.Find(updateRocket.Id);
            if (rocket == null)
                return NotFound();
            rocket.Name = updateRocket.Name;
            rocket.Height = updateRocket.Height;
            _context.SaveChanges();
            return Ok(rocket);
        }
    }
}