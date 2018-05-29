using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HeritageWalksTrail.Controllers
{
    public class StopsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}