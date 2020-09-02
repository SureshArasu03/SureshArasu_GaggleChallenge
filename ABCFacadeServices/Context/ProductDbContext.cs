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
        /// model creator for creating a model in Database 
        ///setting productID as primaryKey
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
