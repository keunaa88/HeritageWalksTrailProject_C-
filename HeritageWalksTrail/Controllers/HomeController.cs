﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HeritageWalksTrail.Models;

namespace HeritageWalksTrail.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            AdminContext context = HttpContext.RequestServices.GetService(typeof(Models.AdminContext)) as AdminContext;
            return View(context.GetAllAdmin());

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}