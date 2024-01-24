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

        ////// Flights ///////

    }
}