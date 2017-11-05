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

        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}