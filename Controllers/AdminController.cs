using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HeritageWalksTrail.Models;

namespace HeritageWalksTrail.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminContext _context;

        public AdminController(AdminContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Get all the data from db
            // Send to View
            return View(_context.GetAllAdmin());

        }

        [HttpGet]
        public IActionResult New()
        {

            return View();
        }

        [HttpPost]
        public IActionResult New(AdminViewModel admin)
        {
             if (admin != null && ModelState.IsValid)
             {
                string msg = _context.InsertAdmin(admin);
                // If new data is successfully inserted, 
                if (msg == "Insert")
                {
                    return View("details", admin);
                }
                else
                {
                    if(msg == "Duplicated")
                        ViewBag.ErrorMessage = "The Admin Id already exists.";

                    return View(admin);
                }
             }
            return View(admin);
        }


         public IActionResult Details(int? id)
        {
            AdminViewModel admin = new AdminViewModel();
            if (id.HasValue)
            {
                admin = _context.GetAdminById(id);
                if (admin == null)
                {
                    admin = new AdminViewModel();
                }
                return View("Details", admin);
            }
            return View("Details", admin);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            AdminViewModel admin = new AdminViewModel();
            if (id.HasValue)
            {
                admin = _context.GetAdminById(id);
                if (admin == null)
                {
                    admin = new AdminViewModel();
                }
                return View("Edit", admin);
            }
            return View("Edit", admin);
        }


        [HttpPost]
        public IActionResult Edit(int id, AdminViewModel admin)
        {
            if (admin != null && ModelState.IsValid)
            {
                // If current password is not correct, User is not able to update personel details
                if(!_context.CheckPassword(id, admin.password))
                {
                    ViewBag.ErrorMessage = "Please check your current password.";
                    return View(admin);
                }
                else { 
                    _context.UpdateAdminById(id, admin);
                    return RedirectToAction("Details", new { id = id });
                }
            }
            return View(admin);
        }


       
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                _context.DeleteAdminById(id);
                return View("Index", _context.GetAllAdmin());
            }
        }

    }
}