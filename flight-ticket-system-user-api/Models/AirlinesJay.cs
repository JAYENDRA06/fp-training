using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace firstapi.Models;

public partial class AirlinesJay
{
    [Key]
    [Required(ErrorMessage = "Airline code is required")]
    public string AirlineCode { get; set; } = null!;

    [Required(ErrorMessage = "Airline name is required")]
    public string AirlineName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<FlightsJay> FlightsJays { get; set; } = new List<FlightsJay>();
}
