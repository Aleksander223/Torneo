using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Infrastructure
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Team>()
                .HasMany(c => c.Members)
                .WithOne(e => e.Team).HasForeignKey(s => s.TeamId).OnDelete(DeleteBehavior.SetNull);
            */

            modelBuilder.Entity<TeamMatch>().HasKey(tm => new { tm.TeamId, tm.MatchId });
            
            modelBuilder.Entity<TeamMatch>().HasOne(tm => tm.Team).WithMany(t => t.TeamMatches).HasForeignKey(tm => tm.TeamId);
            modelBuilder.Entity<TeamMatch>().HasOne(tm => tm.Match).WithMany(t => t.TeamMatches).HasForeignKey(tm => tm.MatchId);

            modelBuilder.Entity<TeamTournament>().HasKey(tm => new { tm.TeamId, tm.TournamentId });

            modelBuilder.Entity<TeamTournament>().HasOne(tm => tm.Team).WithMany(t => t.TeamTournaments).HasForeignKey(tm => tm.TeamId);
            modelBuilder.Entity<TeamTournament>().HasOne(tm => tm.Tournament).WithMany(t => t.TeamTournaments).HasForeignKey(tm => tm.TournamentId);

        }
        

        public AppDbContext() : base()
        {

        }

        public virtual DbSet<User> Users {get; set;}
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Tournament> Tournaments { get; set; }

    }
}
