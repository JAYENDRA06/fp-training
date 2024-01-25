using firstapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace firstapi.Repositories
{
    class Repository(Ace52024Context context) : IRepository
    {
        private readonly Ace52024Context _context = context;

        public async Task AddEntity<T>(T entityToAdd)
        {
            if(entityToAdd != null) _context.Add(entityToAdd);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntity<T>(T entityToRemove)
        {
            if(entityToRemove != null) _context.Remove(entityToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task EditEntity<T>(T entityToEdit)
        {
            if(entityToEdit != null) _context.Update(entityToEdit);
            await _context.SaveChangesAsync();
        }

        ////// Airlines ///////

        public async Task<IEnumerable<AirlinesJay>> GetAllAirlines()
        {
            return await _context.AirlinesJays.ToListAsync();
        }

        public async Task<AirlinesJay?> GetAirline(string id)
        {
            return await _context.AirlinesJays.FindAsync(id);
        }

        public bool AirlineExists(string id)
        {
            return _context.AirlinesJays.Any(e => e.AirlineCode == id);
        }

        ////// Airlines ///////

        ////// Airports ///////
        
        public async Task<IEnumerable<AirportsJay>> GetAllAirports()
        {
            return await _context.AirportsJays.ToListAsync();
        }

        public async Task<AirportsJay?> GetAirport(string id)
        {
            return await _context.AirportsJays.FindAsync(id);
        }

        public bool AirportExists(string id)
        {
            return _context.AirportsJays.Any(e => e.AirportCode == id);
        }

        ////// Airports ///////
        

        ////// Flights ///////
        
        public async Task<IEnumerable<FlightsJay>> GetAllFlights()
        {
            List<FlightsJay> flights = await _context.FlightsJays.Include(f => f.AirlineCodeNavigation).Include(f => f.DepartureAirportCodeNavigation).Include(f => f.ArrivalCodeNavigation).ToListAsync();
            return flights;
        }

        public async Task<FlightsJay?> GetFlight(string id)
        {
            FlightsJay? flights = await _context.FlightsJays.Include(f => f.AirlineCodeNavigation).Include(f => f.DepartureAirportCodeNavigation).Include(f => f.ArrivalCodeNavigation).FirstOrDefaultAsync(f => f.FlightNumber == id);
            return flights;
        }

        public bool FlightExists(string id)
        {
            return _context.FlightsJays.Any(e => e.FlightNumber == id);
        }

        ////// Flights ///////
        

        ////// Passengers ///////
        
        public async Task<IEnumerable<PassengersJay>> GetAllPassengers()
        {
            return await _context.PassengersJays.ToListAsync();
        }

        public async Task<PassengersJay?> GetPassenger(int id)
        {
            PassengersJay? passenger = await _context.PassengersJays.FindAsync(id);
            return passenger;
        }

        public async Task<IEnumerable<BookingsJay>> ViewBookings(int id)
        {
            return await _context.BookingsJays.Where(b => b.PassengerId == id).ToListAsync();
        }

        public async Task<BookingsJay?> DetailsBooking(int id)
        {
            BookingsJay? booking = await _context.BookingsJays.Include(b => b.FlightNumberNavigation).ThenInclude(f => f.AirlineCodeNavigation).Include(b => b.FlightNumberNavigation).ThenInclude(f => f.ArrivalCodeNavigation).Include(b => b.FlightNumberNavigation).ThenInclude(f => f.DepartureAirportCodeNavigation).FirstOrDefaultAsync(b => b.BookingId == id);
            return booking;
        }

        public bool PassengerExists(int id)
        {
            return _context.PassengersJays.Any(e => e.PassengerId == id);
        }

        ////// Passengers ///////
        
    }
}