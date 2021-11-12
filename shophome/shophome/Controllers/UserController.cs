using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopHome.Repository;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopHome.Controllers
{
    public class UserController : Controller
    {
        private ShopHomeRepository _repository;
        public UserController()
        {
            _repository = new ShopHomeRepository();
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckUser(IFormCollection frm)
        {
            string username = frm["username"];
            string password = frm["password"];
            _repository.ValidateUser(username, password);
            return RedirectToAction("ViewProducts", "Product");
        }
    }
}