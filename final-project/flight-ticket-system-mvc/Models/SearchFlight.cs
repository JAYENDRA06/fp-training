using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace flight_ticket_system.Models;

public partial class SearchFlight
{
    public string? DepartureAirportCode { get; set; } = null;

    public string? ArrivalAirportCode { get; set; } = null;

    [DataType(DataType.DateTime)]
    public DateTime? DepartureDateTime { get; set; } = null;
}
