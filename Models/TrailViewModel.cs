using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HeritageWalksTrail.Models
{
    public class TrailViewModel
    {
        private TrailContext context;

        public TrailViewModel()
        {
            colorCodeSelectList = new List<SelectListItem>
            {
                new SelectListItem { Value = "White", Text = "White" },
                new SelectListItem { Value = "Black", Text = "Black" },
                new SelectListItem { Value = "Blue", Text = "Blue"  },
                new SelectListItem { Value = "Red", Text = "Red"  },
                new SelectListItem { Value = "Yellow", Text = "Yellow"  },
                new SelectListItem { Value = "Pink", Text = "Pink"  },
                new SelectListItem { Value = "Orange", Text = "Orange"  },
                new SelectListItem { Value = "Grey", Text = "Grey"  },
                new SelectListItem { Value = "Green", Text = "Green"  },
            };
        }
        [Required]
        [Display(Name = "Id")]
        public int id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Required]
        [Display(Name = "Color")]
        public string colorCode { get; set; }

        [Display(Name = "Photo")]
        public string photoName { get; set; }

        [Required]
        [Display(Name = "Distance")]
        public string distance { get; set; }

        [Required]
        [Display(Name = "Total Time")]
        public string totalTime { get; set; }

        public List<SelectListItem> colorCodeSelectList { get; set; }

        //public List<SelectListItem> ColorCodeSelectList() {

        //    List<SelectListItem> colorCodeSelectList = new List<SelectListItem>
        //    {
        //        new SelectListItem { Value = "White", Text = "White" },
        //        new SelectListItem { Value = "Black", Text = "Black" },
        //        new SelectListItem { Value = "Blue", Text = "Blue"  },
        //        new SelectListItem { Value = "Red", Text = "Red"  },
        //        new SelectListItem { Value = "Yellow", Text = "Yellow"  },
        //        new SelectListItem { Value = "Pink", Text = "Pink"  },
        //        new SelectListItem { Value = "Orange", Text = "Orange"  },
        //        new SelectListItem { Value = "Grey", Text = "Grey"  },
        //        new SelectListItem { Value = "Green", Text = "Green"  },
        //    };

        //    return colorCodeSelectList;
        //}
      

    }
}
