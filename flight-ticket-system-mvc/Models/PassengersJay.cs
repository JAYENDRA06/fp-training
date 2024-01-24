using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace flight_ticket_system.Models;

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
    
    [Required(ErrorMessage = "Confirm password is required")]
    [DataType(DataType.Password)]
    [NotMapped]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    [JsonIgnore]
    public string ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Address is required")]
    [DataType(DataType.MultilineText)]
    public string Address { get; set; } = null!;

    public virtual ICollection<BookingsJay> BookingsJays { get; set; } = new List<BookingsJay>();
}
