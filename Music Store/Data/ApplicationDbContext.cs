using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Music_Store.Models;

namespace Music_Store.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable(nameof(User));
            builder.Entity<IdentityRole>().ToTable("Role");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");

            builder.Entity<Album>().ToTable(nameof(Album));
            builder.Entity<Artist>().ToTable(nameof(Artist));
            builder.Entity<Customer>().ToTable(nameof(Customer));
            builder.Entity<Employee>().ToTable(nameof(Employee));
            builder.Entity<Publisher>().ToTable(nameof(Publisher));
            builder.Entity<Review>().ToTable(nameof(Review));
            builder.Entity<Song>().ToTable(nameof(Song));
            builder.Entity<Genre>().ToTable(nameof(Genre));
            builder.Entity<Invoice>().ToTable(nameof(Invoice));
            builder.Entity<InvoiceDetail>().ToTable(nameof(InvoiceDetail));
            builder.Entity<CreditCard>().ToTable(nameof(CreditCard));
        }

        public DbSet<Music_Store.Models.Cart> Cart { get; set; }
        public DbSet<Music_Store.Models.Wishlist> Wishlist { get; set; }
    }
}
