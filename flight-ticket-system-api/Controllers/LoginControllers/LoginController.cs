using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using firstapi.Models;

namespace firstapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(Ace52024Context context) : ControllerBase
    {
        private readonly Ace52024Context _context = context;

        [HttpPost("register")]
        public async Task<ActionResult> Register(PassengersJay passenger)
        {
            if (UserExists(passenger.Email)) return Conflict();
            if (passenger.Password != passenger.ConfirmPassword)
            {
                throw new Exception("Password and confirm passwords don't match");
            }
            _context.PassengersJays.Add(passenger);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(PassengersJay passenger)
        {
            if (UserExists(passenger.Email)) return Conflict();
            if (passenger.Password != passenger.ConfirmPassword)
            {
                throw new Exception("Password and confirm passwords don't match");
            }
            _context.PassengersJays.Add(passenger);
            await _context.SaveChangesAsync();

            return Ok();
        }


        private bool UserExists(string email)
        {
            return _context.PassengersJays.Any(e => email == e.Email);
        }
    }
}
