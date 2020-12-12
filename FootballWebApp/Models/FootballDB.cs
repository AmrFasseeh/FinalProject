using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FootballWebApp.Models
{
    public partial class FootballDB : DbContext
    {
        public FootballDB()
            : base("name=FootballDB")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<goal> goals { get; set; }
        public virtual DbSet<league> leagues { get; set; }
        public virtual DbSet<match> matches { get; set; }
        public virtual DbSet<player> players { get; set; }
        public virtual DbSet<post> posts { get; set; }
        public virtual DbSet<red_cards> red_cards { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tag> tags { get; set; }
        public virtual DbSet<team> teams { get; set; }
        public virtual DbSet<TeamMatch> TeamMatches { get; set; }
        public virtual DbSet<yellow_cards> yellow_cards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<league>()
                .HasMany(e => e.teams)
                .WithOptional(e => e.league)
                .WillCascadeOnDelete();

            modelBuilder.Entity<match>()
                .HasMany(e => e.goals)
                .WithOptional(e => e.match)
                .WillCascadeOnDelete();

            modelBuilder.Entity<match>()
                .HasMany(e => e.red_cards)
                .WithOptional(e => e.match)
                .WillCascadeOnDelete();

            modelBuilder.Entity<match>()
                .HasMany(e => e.yellow_cards)
                .WithOptional(e => e.match)
                .WillCascadeOnDelete();

            modelBuilder.Entity<post>()
                .Property(e => e.post_content)
                .IsUnicode(false);

            modelBuilder.Entity<post>()
                .HasMany(e => e.tags)
                .WithRequired(e => e.post)
                .HasForeignKey(e => e.post_id);

            modelBuilder.Entity<team>()
                .HasMany(e => e.goals)
                .WithOptional(e => e.team)
                .WillCascadeOnDelete();

            modelBuilder.Entity<team>()
                .HasMany(e => e.players)
                .WithOptional(e => e.team)
                .WillCascadeOnDelete();

            modelBuilder.Entity<team>()
                .HasMany(e => e.red_cards)
                .WithOptional(e => e.team)
                .WillCascadeOnDelete();

            modelBuilder.Entity<team>()
                .HasMany(e => e.yellow_cards)
                .WithOptional(e => e.team)
                .WillCascadeOnDelete();
        }
    }
}
