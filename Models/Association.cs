#pragma warning disable CS8618
#pragma warning disable CS8600
#pragma warning disable CS8602
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models;

public class Association
{
    [Key]
    public int AssociationId { get; set; }
    public int UserId { get; set; }
    public int WeddingId { get; set; }
    public User? Attendee { get; set; }
    public Wedding? Wedding { get; set; }
}