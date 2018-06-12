using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HeritageWalksTrail.Models
{
    public class AdminViewModel
    {
        private AdminContext context;

        [Required]
        [Display(Name = "Id")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Admin Id")]
        public string adminId { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Display(Name = "New Password")]
        public string newPassword { get; set; }

        [Compare("newPassword", ErrorMessage = "The new password and cornfirmation password do not match.")]
        public string newConfirmPassword { get; set; }
        
        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Role")]
        public string role { get; set; }
    }
}
