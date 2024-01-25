using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace firstapi.Models;

public partial class AirportsJay
{
    [Key]
    [Required(ErrorMessage = "Airport code is required")]
    public string AirportCode { get; set; } = null!;

    [Required(ErrorMessage = "Airport name is required")]
    public string AirportName { get; set; } = null!;

    [Required(ErrorMessage = "City name is required")]
    public string City { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<FlightsJay> FlightsJayArrivalCodeNavigations { get; set; } = new List<FlightsJay>();

    [JsonIgnore]
    public virtual ICollection<FlightsJay> FlightsJayDepartureAirportCodeNavigations { get; set; } = new List<FlightsJay>();
}
