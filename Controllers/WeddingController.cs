using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers;

[SessionCheck]
public class WeddingController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public WeddingController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    //! Home View -> Show All Weddings
    [HttpGet("weddings")]
    public ViewResult AllWeddings()
    {
        //* Include Attendee Association list for Guest Count & Include Planner for Delete Action
        List<Wedding> WeddingsList = _context.Weddings.Include(w => w.AttendeeAssociations).ThenInclude(a => a.Attendee).Include(w => w.Planner).ToList();
        return View(WeddingsList);
    }

    //! Plan Wedding View - FORM
    [HttpGet("Weddings/new")]
    public ViewResult PlanWedding() => View();

    //! Create Wedding
    [HttpPost("weddings/create")]
    public IActionResult CreateWedding(Wedding newWedding)
    {

        if (!ModelState.IsValid)
        {
            return View("PlanWedding");
        }

        _context.Add(newWedding);
        _context.SaveChanges();
        return RedirectToAction("ShowWedding", "Wedding", new { weddingId = newWedding.WeddingId });
    }

    //! Show Single Wedding
    [HttpGet("weddings/{weddingId}")]
    public ViewResult ShowWedding(int weddingId)
    {
        Wedding? OneWedding = _context.Weddings.Include(w => w.AttendeeAssociations).ThenInclude(a => a.Attendee).FirstOrDefault(w => w.WeddingId == weddingId);
        return View(OneWedding);
    }

    //! Delete a Wedding
    [HttpPost("weddings/{weddingId}/delete")]
    public IActionResult DeleteWedding(int weddingId)
    {
        Wedding? WeddingToDelete = _context.Weddings.SingleOrDefault(w => w.WeddingId == weddingId);
        _context.Weddings.Remove(WeddingToDelete);
        _context.SaveChanges();
        return RedirectToAction("AllWeddings", "Wedding");
    }
}

//! Check if User is in Session
public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {

        int? userId = context.HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}