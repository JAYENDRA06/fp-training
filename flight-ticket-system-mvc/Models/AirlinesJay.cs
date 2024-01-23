using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace flight_ticket_system.Models;

public partial class AirlinesJay
{
    [Key]
    [Required(ErrorMessage = "Airline code is required")]
    public string AirlineCode { get; set; } = null!;

    [Required(ErrorMessage = "Airline name is required")]
    public string AirlineName { get; set; } = null!;

    public virtual ICollection<FlightsJay> FlightsJays { get; set; } = new List<FlightsJay>();
}
