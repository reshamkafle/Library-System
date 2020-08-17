using LMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LMS.Service.Data
{
    public class LMSContext : DbContext
    {
        public LMSContext(DbContextOptions<LMSContext> options) : base(options)
        {

        }
        public DbSet<Author> School { get; set; }
        public DbSet<Checkout> Checkout { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<MediaType> MediaType { get; set; }
        public DbSet<MediaAuthor> MediaAuthor { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Author> Author { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Checkout>().HasKey(o => new { o.CheckoutDate, o.MediaId, o.LibraryNumber});
            builder.Entity<MediaAuthor>().HasKey(o => new { o.BookId, o.AuthorId });
            builder.Entity<Reservation>().HasKey(o => new { o.MediaId, o.DateRequest, o.LibraryCardNumber });

            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

}
