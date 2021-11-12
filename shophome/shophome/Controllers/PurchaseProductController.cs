using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShopHome.Models;
using ShopHome.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopHome.Controllers
{
    public class PurchaseProductController : Controller
    {
        private ShopHomeRepository _repository;
        public PurchaseProductController(IHostingEnvironment hostingEnvironment)
        {
            _repository = new ShopHomeRepository();
            _hostingEnvironment = hostingEnvironment;
        }
        private readonly IHostingEnvironment _hostingEnvironment;
        string jsonfilepath;
        // GET: /<controller>/
        [HttpGet]
        public IActionResult PurchaseProduct(Product product)
        {
            Purchase purchaseObj = new Purchase();
            purchaseObj.ProductId = product.ProductID;
            purchaseObj.ProductName = product.ProductName;
            purchaseObj.Price = product.Price;
          //  purchaseObj.PurchaseId = new Random().Next(1, 1000);
            return View(purchaseObj);
        }

        public ActionResult OrderPurchase(Purchase purchase)
        {

            jsonfilepath = _hostingEnvironment.WebRootPath + @"\PurchasedProducts.json";
            try
                {
                purchase.PurchaseId = _repository.AddPurchaseDetails(purchase, jsonfilepath);
                }
                catch (Exception)
                {
                }
                ViewData["PurchaseID"] = purchase.PurchaseId;
                ViewData["ProductName"] = purchase.ProductName;
                ViewData["Quantity"] = purchase.Quantity;
                return View("PurchaseSuccess");
            }
        }
    }