using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HeritageWalksTrail.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HeritageWalksTrail.Controllers
{
    public class TrailController : Controller
    {
        public readonly TrailContext _context;
       

        public TrailController(TrailContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Get all the data from db
            // Send to View
            return View(_context.GetAllTrails());

        }

       
        [HttpGet]
        public IActionResult New()
        {
            TrailViewModel trail = new TrailViewModel();
            trail = _context.getColorCodeList(trail);

            return View(trail);
        }

        [HttpPost]
        public IActionResult New(TrailViewModel trail)
        {
            if (trail != null && ModelState.IsValid)
            {
                string msg = _context.InsertTrail(trail);
                // If new data is successfully inserted, 
                if (msg == "Insert")
                {
                    return View("details", trail);
                }
                else
                {
                    return View(trail);
                }

               
            }
            return View(trail);
        }


        public IActionResult Details(int? id)
        {
            TrailViewModel trail = new TrailViewModel();
            trail = _context.getColorCodeList(trail);

            if (id.HasValue)
            {
                trail = _context.GetTrailById(id, trail);
                //if (trail == null)
                //{
                //    trail = new TrailViewModel();
                //}
                return View("Details", trail);
            }
            return View("Details", trail);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            TrailViewModel trail = new TrailViewModel();

            if (id.HasValue)
            {
                trail = _context.GetTrailById(id, trail);
                trail = _context.getColorCodeList(trail);
                //if (trail == null)
                //{
                //    trail = new TrailViewModel();
                //}
                return View("Edit", trail);
            }
            return View("Edit", trail);
        }


        [HttpPost]
        public IActionResult Edit(int id, TrailViewModel trail)
        {
            if (trail != null && ModelState.IsValid)
            {
                _context.UpdateTrailById(id, trail);
                return RedirectToAction("Details", new { id = id });
            }
            return View(trail);
        }



        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                _context.DeleteTrailById(id);
                return View("Index", _context.GetAllTrails());
            }
        }
    }
}