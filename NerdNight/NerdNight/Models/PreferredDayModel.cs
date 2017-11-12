using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NerdNight.Models
{
    public class PreferredDay
    {
        public int ID { get; set; }
        [Required]
        public int GroupID { get; set; }
        [Required]
        [Display(Name = "Day")]
        public DayOfWeek DayOfWeek { get; set; }
        [Required]
        [Display(Name = "All Day")]
        public bool AllDay { get; set; }
        [Required]
        public bool Morning { get; set; }
        [Required]
        public bool Afternoon { get; set; }
        [Required]
        public bool Evening { get; set; }        
    }
}