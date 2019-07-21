using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BilgeAdam.Sinemaskop.Models;
using BilgeAdam.Sinemaskop.Connection;

namespace BilgeAdam.Sinemaskop.Controllers
{
    public class HomeController : Controller
    {
        private readonly SinContext context;

        public HomeController(SinContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Movies()
        {
            var movies = context.Movies.Select(i => new { i.Id, Text = i.Name  }).ToList();
            return Json(movies);
        }

        [HttpPost]
        public IActionResult SaveSeats([FromBody]TicketSaleViewModel model)
        {
            if (model == null)
            {
                return Json(false);
            }
            foreach (var seat in model.Seats)
            {
                var ticket = new Ticket
                {
                    MovieId = model.MovieId,
                    SeatNumber = seat
                };
                context.Tickets.Add(ticket);
            }
            var result = context.SaveChanges();
            return Json(result > 0);
        }

        [HttpGet]
        public IActionResult GetSoldTickets(int id)
        {
            var seats = context.Tickets
                               .Where(i => i.MovieId == id)
                               .Select(i => i.SeatNumber)
                               .ToList();
            return Json(seats);
        }
    }
}
