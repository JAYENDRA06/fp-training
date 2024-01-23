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
    public class AirlineController(IRepository repository) : ControllerBase
    {
        IRepository _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<AirlinesJay>> GetAllAirlines()
        {
            return await _repository.GetAllAirlines();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AirlinesJay>> GetAirline(string id)
        {
            AirlinesJay? airlinesJay = await _repository.GetAirline(id);

            if (airlinesJay == null)
            {
                return NotFound();
            }

            return airlinesJay;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAirline(string id, AirlinesJay airlinesJay)
        {
            if (id != airlinesJay.AirlineCode)
            {
                return BadRequest();
            }

            try
            {
                await _repository.EditEntity<AirlinesJay>(airlinesJay);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.AirlineExists(id))
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
        public async Task<ActionResult<AirlinesJay>> AddAirline(AirlinesJay airlinesJay)
        {
            try
            {
                await _repository.AddEntity<AirlinesJay>(airlinesJay);
            }
            catch (DbUpdateException)
            {
                if (_repository.AirlineExists(airlinesJay.AirlineCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAirlinesJay", new { id = airlinesJay.AirlineCode }, airlinesJay);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirline(string id)
        {
            AirlinesJay? airlinesJay = await _repository.GetAirline(id);
            if (airlinesJay == null)
            {
                return NotFound();
            }

            await _repository.DeleteEntity<AirlinesJay>(airlinesJay);

            return NoContent();
        }
    }
}
