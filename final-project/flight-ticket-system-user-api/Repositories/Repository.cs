using firstapi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace firstapi.Repositories
{
    class Repository(Ace52024Context context) : IRepository
    {
        private readonly Ace52024Context _context = context;

        ////// Passengers ///////

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

        public async Task EditUser(PassengersJay user)
        {
            _context.PassengersJays.Update(user);
            await _context.SaveChangesAsync();
        }

        public bool PassengerExists(int id)
        {
            return _context.PassengersJays.Any(e => e.PassengerId == id);
        }

        ////// Passengers ///////


        ////// Flights ///////

        public async Task<FlightsJay?> GetFlight(string id)
        {
            FlightsJay? flights = await _context.FlightsJays.Include(f => f.AirlineCodeNavigation).Include(f => f.DepartureAirportCodeNavigation).Include(f => f.ArrivalCodeNavigation).FirstOrDefaultAsync(f => f.FlightNumber == id);
            return flights;
        }

        public async Task UpdateFlight(FlightsJay flight)
        {
            _context.FlightsJays.Update(flight);
            await _context.SaveChangesAsync();
        }

        public async Task BookFlight(BookingsJay booking)
        {
            _context.BookingsJays.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FlightsJay>> GetAllFlights()
        {
            List<FlightsJay> flights = await _context.FlightsJays.Include(f => f.AirlineCodeNavigation).Include(f => f.DepartureAirportCodeNavigation).Include(f => f.ArrivalCodeNavigation).ToListAsync();
            return flights;
        }

        ////// Flights ///////


        ////// Airports ///////

        public async Task<IEnumerable<AirportsJay>> GetAllAirports()
        {
            return await _context.AirportsJays.ToListAsync();
        }

        ////// Airports ///////

        ////// Passengers ///////

        public async Task Register(PassengersJay user)
        {
            PassengersJay? pass = _context.PassengersJays.FirstOrDefault(p => p.Email == user.Email);
            if (pass != null) throw new Exception("User already exists");

            await _context.PassengersJays.AddAsync(user);
        }
        public async Task<PassengersJay> Login(PassengersJay user)
        {
            PassengersJay? pass = await _context.PassengersJays.FirstOrDefaultAsync(p => p.Email == user.Email && p.Password == user.Password);
            if(pass == null) throw new Exception("Failed to login");

            return pass;
        }

        ////// Passengers ///////

    }
}