using Ale_Ink.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Ale_Ink.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Note-Person many-to-many relationship
            modelBuilder.Entity<Note>()
                .HasMany(n => n.People)
                .WithMany(p => p.Notes);

            // Note-Place many-to-many relationship
            modelBuilder.Entity<Note>()
                .HasMany(n => n.Places)
                .WithMany(p => p.Notes);

            // Note-Item many-to-many relationship
            modelBuilder.Entity<Note>()
                .HasMany(n => n.Items)
                .WithMany(i => i.Notes);

            // Place-Person many-to-many relationship
            modelBuilder.Entity<Place>()
                .HasMany(p => p.People)
                .WithMany(person => person.Places);

            // Place-Item many-to-many relationship
            modelBuilder.Entity<Place>()
                .HasMany(p => p.Items)
                .WithMany(i => i.Places);

            // Person-Item many-to-many relationship
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Items)
                .WithMany(i => i.People);
        }
    }
}
