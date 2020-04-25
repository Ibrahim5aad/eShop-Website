using System;
using System.Collections.Generic;
using System.Text;
using FinalProject.Models;
using FinalProject.Models.Entities;
using FinalProject.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<BasketItem>().HasOne(b => b.Basket).WithMany(b => b.Items).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<OrderItem>().HasOne(b => b.Order).WithMany(b => b.Items).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Address>().HasOne(b => b.Order).WithOne(b => b.Address).OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
