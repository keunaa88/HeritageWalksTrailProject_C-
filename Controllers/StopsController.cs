using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HeritageWalksTrail.Models;

namespace HeritageWalksTrail.Controllers
{
    public class StopsController : Controller
    {
        private readonly StopsContext _context;

        public StopsController(StopsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Get all the data from db
            // Send to View

            //////////// have to doooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            return View(_context.GetAllStops());

        }

        [HttpGet]
        public IActionResult New()
        {
            StopsViewModel stop = new StopsViewModel();
            stop.trailList = _context.GetAllTrailsSelectListItem();
            stop.admin_id = 1; ///////////////////////////////////////////////////////test

            return View(stop);
        }

        [HttpPost]
        public IActionResult New(StopsViewModel stop)
        {
            stop.trailList = _context.GetAllTrailsSelectListItem();
            if (stop != null && ModelState.IsValid)
            {
                string msg = _context.InsertStop(stop);
                // If new data is successfully inserted, 
                if (msg == "Insert")
                {
                    return View("details", stop);
                }
                else
                {
                    return View(stop);
                }
            }
            return View(stop);
        }


        public IActionResult Details(int? id)
        {
            StopsViewModel stop = new StopsViewModel();
            if (id.HasValue)
            {
                stop = _context.GetStopById(id);
                if (stop == null)
                {
                    stop = new StopsViewModel();
                }
                return View("Details", stop);
            }
            return View("Details", stop);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            StopsViewModel stop = new StopsViewModel();
            stop.trailList = _context.GetAllTrailsSelectListItem();

            if (id.HasValue)
            {
                stop = _context.GetStopById(id);
                if (stop == null)
                {
                    stop = new StopsViewModel();
                }
                return View("Edit", stop);
            }
            return View("Edit", stop);
        }


        [HttpPost]
        public IActionResult Edit(int id, StopsViewModel stop)
        {
            if (stop != null && ModelState.IsValid)
            {
                
                _context.UpdateStopById(id, stop);
                return RedirectToAction("Details", new { id = id });
            }
            return View(stop);
        }



        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                _context.DeleteStopById(id);
                return View("Index", _context.GetAllStops());
            }
        }
    }
}