#pragma warning disable CS8618
#pragma warning disable CS8600
#pragma warning disable CS8602
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models;

public class Wedding
{
    [Key]
    public int WeddingId { get; set; }

    [Required]
    [MaxLength(45)]
    [DisplayName("Wedder One")]
    public string WedderOne { get; set; }

    [Required]
    [MaxLength(45)]
    [DisplayName("Wedder Two")]
    public string WedderTwo { get; set; }

    [Required]
    [FutureDate]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMMM dd, yyyy}")]
    public DateTime? Date { get; set; }

    [Required]
    [MaxLength(45)]
    public string Address { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //! One-to-Many -> A Wedding can only have 1 Planner<User>
    public int UserId { get; set; }
    public User? Planner { get; set; }

    //! Many-to-Many -> A Wedding can have many Attendees<User>
    public List<Association> AttendeeAssociations { get; set; } = new();
}

public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime todaysDate = DateTime.Today;

        if (value == null || ((DateTime)value) <= todaysDate)
        {
            return new ValidationResult("Date selection must be a future date");
        }

        return ValidationResult.Success;
    }
}