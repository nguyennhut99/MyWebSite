﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyShop.Backend.Models;

namespace MyShop.Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductCategory>()
            .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.OrderHeader)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.product)
                .WithMany(p => p.Carts)
                .HasForeignKey(c => c.ProductId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserID);

            modelBuilder.Entity<UserRating>()
            .HasKey(ur => new { ur.ProductId, ur.UserId });

            modelBuilder.Entity<UserRating>()
                .HasOne(ur => ur.Product)
                .WithMany(p => p.UserRatings)
                .HasForeignKey(ur => ur.ProductId);

            modelBuilder.Entity<UserRating>()
                .HasOne(ur => ur.User)
                .WithMany(c => c.UserRatings)
                .HasForeignKey(ur => ur.UserId);
        }
    }
}
