using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ABCWebApplication.Models;
using ABCInterface;
using ABCEntities;

namespace ABCWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IABCIntereface _iABCInterface;

        public HomeController(IABCIntereface _iABCInterface)
        {
            this._iABCInterface = _iABCInterface;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductModel product)
        {
            Product prd = new Product();
            prd.ProductName = product.ProductName;
            prd.ProductPrice = product.ProductPrice;
            prd.ProductQuantity = product.ProductQuantity;
            prd.ProductCategory = product.ProductCategory;
            // prd.ProductStatus = product.ProductStatus;
            product.ProductStatus = _iABCInterface.GetProductStatus(prd).ProductStatus;
            prd.ProductStatus = product.ProductStatus;
            string result = "";
            try
            {

                result = _iABCInterface.AddProduct(prd);


                if (result == "Success")
                {
                    ViewBag.Message = "Product added successfully";

                    return View(product);
                }
                else
                {
                    return View("Error");
                }

            }
            catch (Exception)
            {
                ViewBag.Message = "Product Name already exist";

                return View(product);
            }

        }
        //Get
        public IActionResult Edit(Guid id)
        {
            ProductModel prd = new ProductModel();
            Product product = new Product();
            try
            {
                product = _iABCInterface.GetProductByID(id);


                if (product.ProductID == id) {
                    prd.ProductID = product.ProductID.ToString();

                    prd.ProductName = product.ProductName;
                    prd.ProductPrice = product.ProductPrice;
                    prd.ProductQuantity = product.ProductQuantity;
                    prd.ProductCategory = product.ProductCategory;
                    return View(prd); }
                else
                {
                    return RedirectToAction("GetAllProduct");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("GetAllProduct");
            }
        }

        [HttpPost]
        public IActionResult Edit(ProductModel product)
        {
            Product prd = new Product();
            prd.ProductID = new Guid(product.ProductID);
            prd.ProductName = product.ProductName;
            prd.ProductPrice = product.ProductPrice;
            prd.ProductQuantity = product.ProductQuantity;
            prd.ProductCategory = product.ProductCategory;

            string result = "";
            try
            {
                result = _iABCInterface.UpdateProduct(prd);
                if (result == "Success")
                {
                    ViewBag.Message = "Product updated successfully";
                    return View(product);
                }
                else
                {
                    return View("Error");
                }

            }
            catch (Exception)
            {
                ViewBag.Message = "Product Name already exist";

                return View(product);
            }

        }

        public IActionResult Delete(string id)
        {
            string result = "";
            Guid productid = new Guid(id);
            try
            {
                result = _iABCInterface.DeleteProduct(productid);
                ViewBag.Message = "Product Deleted Successfully";
                return RedirectToAction("GetAllProduct");
            }

            catch (Exception)
            {
                ViewBag.Message = "Error occured while deleting Product";
                return RedirectToAction("GetAllProduct");
            }
        }




        public IActionResult GetAllProduct()
        {
            List<ProductModel> productModels = new List<ProductModel>();
            List<Product> products = new List<Product>();
            try
            {
                products = _iABCInterface.GetAllProducts();
                foreach (Product product in products)
                {
                    ProductModel prd = new ProductModel();
                    prd.ProductID = product.ProductID.ToString();

                    prd.ProductName = product.ProductName;
                    prd.ProductPrice = product.ProductPrice;
                    prd.ProductQuantity = product.ProductQuantity;
                    prd.ProductCategory = product.ProductCategory;

                    productModels.Add(prd);
                }
                if (productModels.Count() > 0)
                {
                    //ViewBag.Message = "Product Name already exist";

                    return View(productModels);
                }
                else
                {
                    ViewBag.Message = "Sorry No Products";

                    return View(productModels);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Sorry No Products";

                return View(productModels);
            }


        }
      
        public IActionResult SearchProduct(string name)
        {
            ProductModel prd = new ProductModel();
            Product product = new Product();
            string searchName = name;
            try
            {
                product = _iABCInterface.GetProductByName(searchName);
                if(product.ProductName!="")
                {
                    prd.ProductID = product.ProductID.ToString();
                    prd.ProductName = product.ProductName;
                    prd.ProductCategory = product.ProductCategory;
                    prd.ProductQuantity = product.ProductQuantity;
                    return View(prd);
                }
                else
                {
                    ViewBag.Message = "Product Name searched is not found please search different name";
                    return RedirectToAction("GetAllProduct");
                }
                
            }
            catch(Exception e)
            {
                ViewBag.Message = "Product Name searched is not found please search different name";
                return RedirectToAction("GetAllProduct");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
