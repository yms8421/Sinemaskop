using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BilgeAdam.Sinemaskop.Models;
using BilgeAdam.Sinemaskop.Connection;
using Microsoft.AspNetCore.Http;
using System;

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
            if (HttpContext.Session.IsAvailable)
            {
                if (!HttpContext.Session.Keys.Contains("userName"))
                {
                    HttpContext.Session.SetString("userName", "can.perk");
                }
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Date1"] = DateTime.Now;
            ViewBag.Date2 = DateTime.Now;

            var user = new UserSession
            {
                UserName = HttpContext.Session.GetString("userName")
            };
            return View(user);
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
