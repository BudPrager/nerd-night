using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NerdNight.Models
{
    public class AvailabilityViewModel
    {
        public int GroupId { get; set; }

        public int DefaultAvailabilityRange { get; set; }
        public AvailabilityUnit AvailabilityUnit { get; set; }

        public virtual ICollection<PreferredDay> PreferredDays { get; set; }

        public int MainCampaignID { get; set; }
        //public virtual ICollection<Campaign> GroupCampaigns { get; set; }
    }
}