using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ABCEntities;
using ABCFacadeServices.Context;
using ABCInterface;

/// <summary>
/// The ABC Faced Services implements the interfaces correspoding to each functionality
/// 
/// Faced Deign Pattern loosly couple the methods, which is highly helpful in further enhancements and Testing process
/// </summary>

namespace ABCFacadeServices
{
    public class ABCServices : IABCIntereface
    {
        private readonly ProductDbContext _dbcontext;

        public ABCServices(ProductDbContext _dbcontext)
        {
            this._dbcontext = _dbcontext;
        }
        public string AddProduct(Product product)
        {
            string result = "Success";
            try { 
                _dbcontext.ProductTable.Add(product);
                _dbcontext.SaveChanges();
            }
            catch (Exception E)
            {

                result = E.StackTrace;
            }

            return result;
        }

        public string DeleteProduct(Guid id)
        {
            Product product = new Product();
            string result = "Success";
            try
            {
                product = _dbcontext.ProductTable.Where(e => e.ProductID.Equals(id)).FirstOrDefault();
                _dbcontext.ProductTable.Remove(product);
                _dbcontext.SaveChanges();

            }
           
            catch (Exception e)
            {
                result = e.StackTrace;
                return result;
            }
            return result;
        }
        public Product GetProductByID(Guid id)
        {
            Product product = new Product();
            try
            {
                product = _dbcontext.ProductTable.Where(e => e.ProductID.Equals(id)).FirstOrDefault();
                return product;
            }
            catch(Exception e)
            {
                return product;
            }
        }
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                products = _dbcontext.ProductTable.ToList<Product>();
                return products;
            }
             catch(Exception ex)
            {
                return products;
            }
            }

        public string UpdateProduct(Product product)
        {
            string result = "Success";
            try
            {
                _dbcontext.ProductTable.Update(product);
                _dbcontext.SaveChanges();
            }
            catch (Exception E)
            {

                result = E.StackTrace;
            }

            return result;
        }

        public Product GetProductByName(string searchName)
        {
            Product products = new Product();
            try
            {
                products = _dbcontext.ProductTable.Where(e => e.ProductName.ToLower().Equals(searchName.ToLower())).FirstOrDefault();
                return products;
            }
            catch (Exception e)
            {
                return products;
            }
        }

        public Product GetProductStatus(Product  product)
        {
            //Product product = new Product();

            try
            {
               // product = _dbcontext.ProductTable.Where(e => e.ProductName.Equals(name)).FirstOrDefault();
                int count = product.ProductQuantity;
                if (count >= 1)
                {
                    product.ProductStatus = "In Stock";
                }
                else
                {
                    product.ProductStatus = "Out of Stock";
                }
                return product;
            }
            catch (Exception e)
            {
                return product;
            }


        }
    }
}
