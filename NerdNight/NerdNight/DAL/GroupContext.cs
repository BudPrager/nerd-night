using NerdNight.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace NerdNight.DAL
{
    public class GroupContext : DbContext
    {
        public GroupContext() : base("GroupContext") //TODO - worry about what this connection string is
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<CampaignPlayer> CampaignPlayers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //TODO - if we want pluralized then we need to remove this line

            modelBuilder.Entity<Group>() //TODO - this is supposed to map the many to many relationship of groups and players
                .HasMany<Player>(g => g.Players)
                .WithMany(p => p.Groups)
                .Map(gp =>
                {
                    gp.MapLeftKey("GroupID");
                    gp.MapRightKey("PlayerID");
                    gp.ToTable("GroupPlayer");
                });
        }
    }
}