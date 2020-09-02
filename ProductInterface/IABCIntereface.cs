using System;
using System.Collections.Generic;
using System.Text;
using ABCEntities;

/// <summary>
/// Interfaces for required functional logic
/// </summary>

namespace ABCInterface
{
    public interface IABCIntereface
    {
        string AddProduct(Product product);
        string UpdateProduct(Product product);
        string DeleteProduct(Guid id);
        Product GetProductByID(Guid id);
        List<Product> GetAllProducts();

        Product GetProductByName(string productName);

        Product GetProductStatus(Product product);
    }
}
