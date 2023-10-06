using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers;

[SessionCheck]
public class AssociationController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public AssociationController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    //! RSVP to Wedding
    [HttpPost("associations/create")]
    public IActionResult RSVP(int weddingId, int attendeeId)
    {
        Association NewRSVP = new()
        {
            UserId = attendeeId,
            WeddingId = weddingId
        };
        _context.Add(NewRSVP);
        _context.SaveChanges();
        return RedirectToAction("AllWeddings", "Wedding");
    }

    //! Un-RSVP from Wedding
    [HttpPost("associations/delete/")]
    public IActionResult UnRSVP(int associationId, int attendeeId, int weddingId)
    {
        Association? RSVPToDelete = _context.Associations.SingleOrDefault(a => a.UserId == attendeeId && a.WeddingId == weddingId);
        _context.Associations.Remove(RSVPToDelete);
        _context.SaveChanges();
        return RedirectToAction("AllWeddings", "Wedding");
    }
}