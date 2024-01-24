using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace firstapi.Models;

public partial class PassengersJay
{
    [Key]
    public int PassengerId { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public string? Name { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<BookingsJay> BookingsJays { get; set; } = new List<BookingsJay>();
}
