using System;
using System.Collections.Generic;
using System.Text;
using ABCEntities;
using Microsoft.EntityFrameworkCore;

namespace ABCFacadeServices.Context
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }
    
        /// <summary>
        /// Model creator for creating a Model in Database 
        /// The productID is kept as primarykey
        /// The Product Name is considered as unique Product Identification attribute
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(eb =>
            {
                eb.HasKey(o => new { o.ProductID });
                eb.HasIndex(o => new { o.ProductName }).IsUnique();
            });

        }

        public DbSet<Product> ProductTable { get; set; }

    }
}
