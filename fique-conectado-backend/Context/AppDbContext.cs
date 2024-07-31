using fique_conectado_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace fique_conectado_backend.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<User> Users {  get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Entertainment> Entertainments{ get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<ListEntertainment> ListEntertainments { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<User>().ToTable("user");
           modelBuilder.Entity<List>().ToTable("list");
           modelBuilder.Entity<Entertainment>().ToTable("entertainment");
           modelBuilder.Entity<Rating>().ToTable("rating");
           modelBuilder.Entity<ListEntertainment>().ToTable("listEntertainments");

        }
    }
}
