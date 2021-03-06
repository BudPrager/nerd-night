﻿using NerdNight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NerdNight.DAL
{
    //TODO - change to code first migrations before publishing
    public class GroupInitialiser : System.Data.Entity.DropCreateDatabaseAlways<GroupContext>
    {
        protected override void Seed(GroupContext context)
        {
            var groups = new List<Group>
            {
                new Group {GroupName = "The Nerds", CampaignID = 2, DefaultAvailabilityRange = 1, AvailabilityUnit = AvailabilityUnit.Weeks},
                new Group {GroupName = "Not the Nerds" }
            };

            //TODO - be more flexible on later seeding
            groups.ForEach(g => context.Groups.Add(g));
            context.SaveChanges();

            var preferredDays = new List<PreferredDay>
            {
                new PreferredDay{GroupID = context.Groups.Single(g => g.GroupName == "The Nerds").ID, DayOfWeek = DayOfWeek.Monday, AllDay = false, Morning = false, Afternoon = false, Evening = true },
                new PreferredDay{GroupID = context.Groups.Single(g => g.GroupName == "The Nerds").ID, DayOfWeek = DayOfWeek.Tuesday, AllDay = false, Morning = false, Afternoon = false, Evening = true },
                new PreferredDay{GroupID = context.Groups.Single(g => g.GroupName == "The Nerds").ID, DayOfWeek = DayOfWeek.Wednesday, AllDay = false, Morning = false, Afternoon = false, Evening = true },
                new PreferredDay{GroupID = context.Groups.Single(g => g.GroupName == "The Nerds").ID, DayOfWeek = DayOfWeek.Thursday, AllDay = false, Morning = false, Afternoon = false, Evening = true },
                new PreferredDay{GroupID = context.Groups.Single(g => g.GroupName == "The Nerds").ID, DayOfWeek = DayOfWeek.Friday, AllDay = false, Morning = false, Afternoon = false, Evening = true }
            };

            preferredDays.ForEach(p => context.PreferredDays.Add(p));
            context.SaveChanges();

            var campaigns = new List<Campaign>
            {
                new Campaign {CampaignName = "Grunt Darkmagic", GroupID = context.Groups.Single(g => g.GroupName == "The Nerds").ID },
                new Campaign {CampaignName = "Out of the Abyss", GroupID = context.Groups.Single(g => g.GroupName == "The Nerds").ID },
                new Campaign {CampaignName = "Back to the Day Before", GroupID = context.Groups.Single(g => g.GroupName == "The Nerds").ID },
                new Campaign {CampaignName = "Not the Nerds Campaign", GroupID = context.Groups.Single(g => g.GroupName == "Not the Nerds").ID}
            };

            campaigns.ForEach(c => context.Campaigns.Add(c));
            context.SaveChanges();

            var players = new List<Player>
            {
                new Player {DisplayName = "Matt" },
                new Player {DisplayName = "Jack" },
                new Player {DisplayName = "Miles" },
                new Player {DisplayName =  "Tom" },
                new Player {DisplayName = "Lee" }
            };

            players.ForEach(p => context.Players.Add(p));
            context.SaveChanges();

            players.ForEach(p => LinkGroupAndPlayer(context, "The Nerds", p.DisplayName));
            context.SaveChanges();

            context.Players.Add(new Player { DisplayName = "Not a Nerd" });
            context.SaveChanges();
            LinkGroupAndPlayer(context, "Not the Nerds", "Not a Nerd");
            context.SaveChanges();

            var campaignPlayers = new List<CampaignPlayer>
            {
                new CampaignPlayer {PlayerID = context.Players.Single(p => p.DisplayName == "Matt").ID, CampaignID = context.Campaigns.Single(c => c.CampaignName == "Grunt Darkmagic").ID, CharacterName = "Grunt Darkmagic", IsDM = false},
                new CampaignPlayer {PlayerID = context.Players.Single(p => p.DisplayName == "Miles").ID, CampaignID = context.Campaigns.Single(c => c.CampaignName == "Grunt Darkmagic").ID, CharacterName = "DM", IsDM = true},
                new CampaignPlayer {PlayerID = context.Players.Single(p => p.DisplayName == "Tom").ID, CampaignID = context.Campaigns.Single(c => c.CampaignName == "Grunt Darkmagic").ID, CharacterName = "Lizard Thingy", IsDM = false},
                new CampaignPlayer {PlayerID = context.Players.Single(p => p.DisplayName == "Lee").ID, CampaignID = context.Campaigns.Single(c => c.CampaignName == "Grunt Darkmagic").ID, CharacterName = "Quis Whatever", IsDM = false },

                new CampaignPlayer {PlayerID = context.Players.Single(p => p.DisplayName == "Matt").ID, CampaignID = context.Campaigns.Single(c => c.CampaignName == "Out of the Abyss").ID, CharacterName = "Bryn Elface", IsDM = false},
                new CampaignPlayer {PlayerID = context.Players.Single(p => p.DisplayName == "Jack").ID, CampaignID = context.Campaigns.Single(c => c.CampaignName == "Out of the Abyss").ID, CharacterName = "DM", IsDM = true },
                new CampaignPlayer {PlayerID = context.Players.Single(p => p.DisplayName == "Miles").ID, CampaignID = context.Campaigns.Single(c => c.CampaignName == "Out of the Abyss").ID, CharacterName = "Miles' Character", IsDM = false},
                new CampaignPlayer {PlayerID = context.Players.Single(p => p.DisplayName == "Tom").ID, CampaignID = context.Campaigns.Single(c => c.CampaignName == "Out of the Abyss").ID, CharacterName = "Gnome the shortest", IsDM = false},
                new CampaignPlayer {PlayerID = context.Players.Single(p => p.DisplayName == "Lee").ID, CampaignID = context.Campaigns.Single(c => c.CampaignName == "Out of the Abyss").ID, CharacterName = "Wilson the rogue", IsDM = false },

                new CampaignPlayer {PlayerID = context.Players.Single(p => p.DisplayName == "Matt").ID, CampaignID = context.Campaigns.Single(c => c.CampaignName == "Back to the Day Before").ID, CharacterName = "Wizard Thingy", IsDM = false},
                new CampaignPlayer {PlayerID = context.Players.Single(p => p.DisplayName == "Jack").ID, CampaignID = context.Campaigns.Single(c => c.CampaignName == "Back to the Day Before").ID, CharacterName = "Drunk? Thingy", IsDM = false },
                new CampaignPlayer {PlayerID = context.Players.Single(p => p.DisplayName == "Miles").ID, CampaignID = context.Campaigns.Single(c => c.CampaignName == "Back to the Day Before").ID, CharacterName = "DM", IsDM = true},
                new CampaignPlayer {PlayerID = context.Players.Single(p => p.DisplayName == "Tom").ID, CampaignID = context.Campaigns.Single(c => c.CampaignName == "Back to the Day Before").ID, CharacterName = "What is Tom?", IsDM = false},

                new CampaignPlayer {PlayerID = context.Players.Single(p => p.DisplayName == "Not a Nerd").ID, CampaignID = context.Campaigns.Single(c => c.CampaignName == "Not the Nerds Campaign").ID, CharacterName = "Not a Nerd Character", IsDM = false}
            };

            campaignPlayers.ForEach(cp => context.CampaignPlayers.Add(cp));
            context.SaveChanges();
        }

        void LinkGroupAndPlayer(GroupContext context, string groupName, string playerName)
        {
            var grp = context.Groups.SingleOrDefault(g => g.GroupName == groupName);
            var per = grp.Players.SingleOrDefault(p => p.DisplayName == playerName);
            if(per == null)
            {
                grp.Players.Add(context.Players.Single(p => p.DisplayName == playerName));
            }
        }
    }
}