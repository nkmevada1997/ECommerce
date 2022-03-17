using Ecommerce.BAL.Interface;
using Ecommerce.DAL.Models;
using Ecommerce.Helper.Encode;
using Ecommerce.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IService<User> service;

        public AuthenticationController(IService<User> service)
        {
            this.service = service;
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return RedirectToAction("Index", "Customers");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var users = this.service.GetAll();

                if (users != null && users.Count > 0)
                {
                    var user = users.SingleOrDefault(x => x.Email == model.Email && x.Password == EncodeBase.EncodeBase64(model.Password));

                    if (user != null)
                    {
                        HttpContext.Session.SetString("UserId", user.UserId.ToString());
                        HttpContext.Session.SetString("UserEmail", user.Email);
                        HttpContext.Session.SetString("UserName", user.UserName);
                        HttpContext.Session.SetInt32("UserType", (int)user.UserType);

                        return RedirectToAction("Index", "Customers");
                    }
                    else
                    {
                        ViewBag.Message = "Invalid UserName or Password !!!";
                        return View();
                    }
                }
            }
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Authentication");
        }
    }
}
