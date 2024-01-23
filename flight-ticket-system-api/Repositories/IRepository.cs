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
        public Task<IEnumerable<AirlinesJay>> GetAllAirlines();

        public Task<AirlinesJay?> GetAirline(string id);

        public Task<IEnumerable<AirportsJay>> GetAllAirports();

        public Task<AirportsJay?> GetAirport(string id);

        public Task<IEnumerable<FlightsJay>> GetAllFlights();

        public Task<FlightsJay?> GetFlight(string id);

        public Task EditEntity<T>(T entityToEdit);

        public Task AddEntity<T>(T entityToAdd);

        public Task DeleteEntity<T>(T entityToRemove);

        public bool AirlineExists(string id);

        public bool AirportExists(string id);

        public bool FlightExists(string id);

        public Task<IEnumerable<PassengersJay>> GetAllPassengers();

        public Task<PassengersJay?> GetPassenger(int id);

        public Task<IEnumerable<BookingsJay>> ViewBookings(int id);

        public Task<BookingsJay?> DetailsBooking(int id);

    }
}
