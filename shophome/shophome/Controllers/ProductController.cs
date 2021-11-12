using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopHome.Repository;
using ShopHome.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopHome.Controllers
{
    public class ProductController : Controller
    {
        private ShopHomeRepository _repository;
        public ProductController(IHostingEnvironment hostingEnvironment)
        {
            _repository = new ShopHomeRepository();
            _hostingEnvironment = hostingEnvironment;
        }
        private readonly IHostingEnvironment _hostingEnvironment;
        List<Product> products;
        string jsonfilepath;
        // GET: /<controller>/
        public IActionResult ViewProducts()
        {
            jsonfilepath = _hostingEnvironment.WebRootPath + @"\products.json";
            products = _repository.ReadProducts(jsonfilepath);
            return View(products);
        }
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveAddedProduct(Product product)
        {
            jsonfilepath = _hostingEnvironment.WebRootPath + @"\products.json";
            int status = _repository.AddProduct(product, jsonfilepath, products);
            if (status == 1)
            {
                return RedirectToAction("ViewProducts", "Product");
            }
            else if (status == -1)
            { return View("Error.cshtml"); }
            else
            {
                return View("AddProduct");
            }
        }
       
    }
}
