using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace flight_ticket_system.Models;

public partial class BookingsJay
{
    [Key]
    public int BookingId { get; set; }

    public string? FlightNumber { get; set; }

    public int? PassengerId { get; set; }

    public DateTime BookingDate { get; set; }

    public decimal? TotalCost { get; set; }

    public virtual FlightsJay? FlightNumberNavigation { get; set; }

    public virtual PassengersJay? Passenger { get; set; }
}
