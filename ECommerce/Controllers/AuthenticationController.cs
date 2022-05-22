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
            LoginModel model = new LoginModel();
            string email, password;
            bool remember = false;


            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return RedirectToAction("Index", "Customers");
            }

            HttpContext.Request.Cookies.TryGetValue("UserEmail", out email);
            HttpContext.Request.Cookies.TryGetValue("UserPassword", out password);

            model.Email = email;
            model.Password = password;

            if (HttpContext.Request.Cookies["RememberMe"] != null)
            {
                bool.TryParse(HttpContext.Request.Cookies["RememberMe"], out remember);
                model.RememberMe = remember;
            }


            return View(model);
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
                        HttpContext.Session.SetString("UserId", user.Id.ToString());
                        HttpContext.Session.SetString("UserEmail", user.Email);
                        HttpContext.Session.SetString("UserName", user.UserName);
                        HttpContext.Session.SetInt32("UserType", (int)user.UserType);

                        if (model.RememberMe)
                        {
                            CookieOptions options = new CookieOptions();
                            options.Expires = DateTime.Now.AddDays(365);
                            HttpContext.Response.Cookies.Append("UserEmail", model.Email.ToString(), options);
                            HttpContext.Response.Cookies.Append("UserPassword", model.Password.ToString(), options);
                            HttpContext.Response.Cookies.Append("RememberMe", model.RememberMe.ToString(), options);
                        }
                        else
                        {
                            HttpContext.Response.Cookies.Delete("UserEmail");
                            HttpContext.Response.Cookies.Delete("UserPassword");
                            HttpContext.Response.Cookies.Delete("RememberMe");
                        }


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
