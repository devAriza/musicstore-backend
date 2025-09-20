using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;

namespace MusicStore.Persistence
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {


        }

        // Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Aplicar automáticamente todas las configuraciones

            //// Custom configurations can be added here
            //modelBuilder.Entity<Genre>().Property(x=>x.Name).HasMaxLength(50);
        }

        // Entities to table
        // public DbSet<Genre> Genres { get; set; }

    }
}
