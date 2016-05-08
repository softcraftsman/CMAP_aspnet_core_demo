using CMAP.Model;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMAP.Models
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Use this approach since not all Entity Framework Model Types are available (TPT, etc.)
            modelBuilder.Entity<Event>()
                .HasDiscriminator<string>("EventType")
                .HasValue<VitalSignsEvent>(VitalSignsEvent.EventClass);
            // Additional Types to follow
        }

        // Model Sets
        public DbSet<Event> Events { get; set; }
        public DbSet<VitalSignsEvent> VitalSignsEvents { get; set; }

    }
}
