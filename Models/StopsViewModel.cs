using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HeritageWalksTrail.Models
{
    public class StopsViewModel
    {
        private StopsContext context;

        [Required]
        [Display(Name = "Id")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Photo")]
        public string photoName { get; set; }

        [Display(Name = "Time")]
        public string timeSpent { get; set; }

        [Required]
        [Display(Name = "Latitude")]
        public string latitude { get; set; }

        [Required]
        [Display(Name = "longitude")]
        public string longitude { get; set; }

        [Display(Name = "Audio")]
        public string audioName { get; set; }

        public int trail_id { get; set; }

        public int admin_id { get; set; }


        public List<SelectListItem> trailList { get; set; }

    }
}
