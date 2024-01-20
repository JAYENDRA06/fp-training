using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace first_mvc_application.Models;

public class Customer
{
    [Key]
    public int Cid { get; set; }

    [Required]
    [Display(Name = "Customer name")]
    public string? Cname { get; set; }

    [Range(minimum: 5000, maximum: 100000, ErrorMessage = "salary should be in range")]
    public decimal? Salary { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? DateTime { get; set; }

    [Required(ErrorMessage = "Password required")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [NotMapped] // this property should be excluded from mapping
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; set; }

}