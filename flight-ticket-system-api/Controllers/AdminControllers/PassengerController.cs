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
    public class PassengerController(IRepository repository) : ControllerBase
    {
        private readonly IRepository _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<PassengersJay>> GetPassengers()
        {
            return await _repository.GetAllPassengers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PassengersJay>> GetPassenger(int id)
        {
            var passengersJay = await _repository.GetPassenger(id);

            if (passengersJay == null)
            {
                return NotFound();
            }

            return passengersJay;
        }

        [HttpGet("view-bookings/{id}")]
        public async Task<IEnumerable<BookingsJay>> ViewBookings(int id)
        {
            return await _repository.ViewBookings(id);
        }

        [HttpGet("details-booking/{id}")]
        public async Task<ActionResult<BookingsJay?>> DetailsBooking(int id)
        {
            return await _repository.DetailsBooking(id);
        }
    }
}
