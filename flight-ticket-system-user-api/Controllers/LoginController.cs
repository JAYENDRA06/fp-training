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
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(IRepository repository) : ControllerBase
    {
        private readonly IRepository _repository = repository;

        [HttpPost("login")]
        public async Task<ActionResult<PassengersJay>> Login(PassengersJay user)
        {
            return await _repository.Login(user);
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> Register(PassengersJay user)
        {
            await _repository.Register(user);

            return NoContent();
        }
    }
}