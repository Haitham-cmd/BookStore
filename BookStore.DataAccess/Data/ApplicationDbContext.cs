﻿
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreDataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Action",
                    Displayorder = 1
                },
                new Category
                {
                    Id = 2,
                    Name = "Science Fiction",
                    Displayorder = 2
                },
                new Category
                {
                    Id = 3,
                    Name = "Romance",
                    Displayorder = 3
                }
                );
        }
    }
}
