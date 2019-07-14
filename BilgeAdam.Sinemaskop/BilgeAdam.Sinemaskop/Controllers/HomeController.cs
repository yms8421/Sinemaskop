﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
