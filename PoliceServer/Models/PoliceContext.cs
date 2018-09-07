using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using PoliceServer.Migrations;

namespace PoliceServer.Models
{
    public class PoliceContext : DbContext
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public PoliceContext() : base("WristBandContext")
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<PoliceContext,Configuration>());            
        }


        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Responsibility> Responsibilities { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<User>()
                .HasMany(e => e.Responsibilities)
                .WithRequired(e => e.User)
                .HasForeignKey(e => new {e.UserID })
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Tickets)
                .WithRequired(e => e.User)
                .HasForeignKey(e => new {e.UserID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Patients)
                .WithOptional(e => e.Dortor)
                .HasForeignKey(e => new {e.DoctorID })
                .WillCascadeOnDelete(false);

        }
    }
}