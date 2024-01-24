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
    public class AirportController(IRepository repository) : ControllerBase
    {
        IRepository _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<AirportsJay>> GetAirports()
        {
            return await _repository.GetAllAirports();
        }
    }
}
