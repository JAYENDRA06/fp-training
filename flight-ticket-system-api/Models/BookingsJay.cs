using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace firstapi.Models;

public partial class BookingsJay
{
    [Key]
    public int BookingId { get; set; }

    [Required(ErrorMessage = "Flight number is required")]
    public string? FlightNumber { get; set; }

    [Required(ErrorMessage = "Passenger id is required")]
    public int? PassengerId { get; set; }

    public DateTime BookingDate { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Total cost is required")]
    public decimal? TotalCost { get; set; }

    [Required(ErrorMessage = "Enter number of passengers")]
    [Range(minimum: 1, maximum: 4, ErrorMessage = "Enter a value between 1 and 4")]
    public int Passengers { get; set; }

    public virtual FlightsJay? FlightNumberNavigation { get; set; }

    public virtual PassengersJay? Passenger { get; set; }
}
