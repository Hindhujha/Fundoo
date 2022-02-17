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


        protected override void
        OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
               .HasIndex(u => u.email)
               .IsUnique();

            //modelBuilder.Entity<Note>()
            ////.HasOne(u => u.User).WithMany()
            ////.HasForeignKey(u=>u.UserId);
            //.HasKey(x => new { x.NotesId, x.UserId });
        }
    }

}

