using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Services
{
    public class FundooDbContext:DbContext
    {
        public FundooDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Note> Note { get; set; }

        public DbSet<Label> Label { get; set; }

        public DbSet<UserAddress> UserAddresses { get; set; }

        public DbSet<Collab> Collab { get; set; }




        protected override void
        OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
               .HasIndex(u => u.email)
               .IsUnique();

            modelBuilder.Entity<UserAddress>()
            .Property(b => b.Type)
            .HasDefaultValue("Home");
        }

    }

}

