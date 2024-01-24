using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using firstapi.Models;

namespace firstapi.Repositories
{

    public interface IRepository
    {
        public Task EditUser(PassengersJay user);

        public bool PassengerExists(int id);

        public Task<PassengersJay?> GetPassenger(int id);

        public Task<IEnumerable<BookingsJay>> ViewBookings(int id);

        public Task<BookingsJay?> DetailsBooking(int id);

        public Task<FlightsJay?> GetFlight(string id);

        public Task UpdateFlight(FlightsJay flight);

        public Task BookFlight(BookingsJay booking);
    }
}
