using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NerdNight.Models
{
    public class Campaign
    {
        public Campaign()
        {

        }

        public int ID { get; set; }
        [Required]
        public string CampaignName { get; set; }

        public int GroupID { get; set; }
        public virtual Group Group { get; set; }

        public virtual ICollection<CampaignPlayer> CampaignPlayers { get; set; }
        
    }

    public class CampaignPlayer
    {        
        public int ID { get; set; } //TODO - do we need to have an ID if we are unique on campaign and player
        public int CampaignID { get; set; }
        public int PlayerID { get; set; }

        [Required]
        public bool IsDM { get; set; }
        [Required]
        public string CharacterName { get; set; }

        public virtual Campaign Campaign { get; set; }
        public virtual Player Player { get; set; }
    }
}