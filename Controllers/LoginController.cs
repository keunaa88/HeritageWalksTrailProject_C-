﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HeritageWalksTrail.Models;

namespace HeritageWalksTrail.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {

            AdminContext context = HttpContext.RequestServices.GetService(typeof(Models.AdminContext)) as AdminContext;
     
            return View(context.GetAllAdmin());
        }
    }
}