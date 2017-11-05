using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NerdNight.Models
{
    public class Player
    {
        //TODO - do we want to do this at the account level instead?

        public Player()
        {
            this.Groups = new HashSet<Group>();
        }

        public int ID { get; set; }
        [Required]
        public string DisplayName { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<CampaignPlayer> Campaigns { get; set; }
    }
}