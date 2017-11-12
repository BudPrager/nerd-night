using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NerdNight.Models
{
    public class Group
    {
        public Group()
        {
            Players = new HashSet<Player>();
        }

        public int ID { get; set; }
        [Required]
        public string GroupName { get; set; }
        
        [Display(Name = "Time Between Sessions")]
        public int DefaultAvailabilityRange { get; set; }
        public AvailabilityUnit AvailabilityUnit { get; set; }

        public virtual ICollection<PreferredDay> PreferredDays { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }

        [Display(Name = "Main Campaign")]
        public int CampaignID { get; set; } //TODO - Should we rename this to MainCampaignID and bind to foreign key manually?
    }

    public enum AvailabilityUnit
    {
        Days,
        Weeks,
        Months,
    }
}