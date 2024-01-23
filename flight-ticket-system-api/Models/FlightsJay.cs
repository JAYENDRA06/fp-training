using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace firstapi.Models;

public partial class FlightsJay
{
    [Key]
    [Required(ErrorMessage = "Flight number is required")]
    public string FlightNumber { get; set; } = null!;

    [Required(ErrorMessage = "Flight name is required")]
    public string? FlightName { get; set; }

    [Required(ErrorMessage = "Departure airport code is required")]
    public string? DepartureAirportCode { get; set; }

    [Required(ErrorMessage = "arrival airport code is required")]
    public string? ArrivalCode { get; set; }

    [Required(ErrorMessage = "Airline code is required")]
    public string? AirlineCode { get; set; }

    [Required(ErrorMessage = "Departure date is required")]
    [DataType(DataType.DateTime)]
    public DateTime DepartureDateTime { get; set; }

    [Required(ErrorMessage = "Arrival date is required")]
    [DataType(DataType.DateTime)]
    public DateTime ArrivalDateTime { get; set; }

    [Required(ErrorMessage = "Available seats is required")]
    public int AvailableSeats { get; set; }

    [Required(ErrorMessage = "Ticket price is required")]
    public decimal TicketPrice { get; set; }

    public virtual AirlinesJay? AirlineCodeNavigation { get; set; }

    public virtual AirportsJay? ArrivalCodeNavigation { get; set; }

    public virtual ICollection<BookingsJay> BookingsJays { get; set; } = new List<BookingsJay>();

    public virtual AirportsJay? DepartureAirportCodeNavigation { get; set; }
}
