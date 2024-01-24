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
    [Route("api/admin/[controller]")]
    [ApiController]
    public class FlightController(IRepository repository) : ControllerBase
    {
        IRepository _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<FlightsJay>> GetFlights()
        {
            return await _repository.GetAllFlights();
        }

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

        [HttpPut("{id}")]
        public async Task<IActionResult> EditFlight(string id, FlightsJay flightsJay)
        {
            if (id != flightsJay.FlightNumber)
            {
                return BadRequest();
            }

            try
            {
                await _repository.EditEntity<FlightsJay>(flightsJay);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.FlightExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> AddFlight(FlightsJay flightsJay)
        {
            try
            {
                await _repository.AddEntity<FlightsJay>(flightsJay);
            }
            catch (DbUpdateException)
            {
                if (_repository.FlightExists(flightsJay.FlightNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(string id)
        {
            FlightsJay? flightsJay = await _repository.GetFlight(id);
            if (flightsJay == null)
            {
                return NotFound();
            }

            await _repository.DeleteEntity<FlightsJay>(flightsJay);

            return NoContent();
        }
    }
}
