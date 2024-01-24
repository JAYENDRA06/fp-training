using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using firstapi.Models;
using firstapi.Repositories;

namespace firstapi.Controllers
{
    [Route("api/user/[controller]")]
    [ApiController]
    public class FlightController(IRepository repository) : ControllerBase
    {
        IRepository _repository = repository;

        [HttpGet("{id}")]
        public async Task<ActionResult<FlightsJay>> GetFlight(string id)
        {
            FlightsJay? flightsJay = await _repository.GetFlight(id);

            if (flightsJay == null)
            {
                return NotFound();
            }

            return flightsJay;
        }

        [HttpPost("update-flight")]
        public async Task<ActionResult> UpdateFlight(FlightsJay flight)
        {
            await _repository.UpdateFlight(flight);
            return NoContent();
        }

        [HttpPost("book-flight")]
        public async Task<ActionResult> BookFlight(BookingsJay booking)
        {
            await _repository.BookFlight(booking);
            return NoContent();
        }
    }
}
