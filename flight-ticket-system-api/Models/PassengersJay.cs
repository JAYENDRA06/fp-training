﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

    [Required(ErrorMessage = "Confirm password is required")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password and confirm passwords don't match")]
    public string ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Address is required")]
    [DataType(DataType.MultilineText)]
    public string Address { get; set; } = null!;

    public virtual ICollection<BookingsJay> BookingsJays { get; set; } = new List<BookingsJay>();
}
