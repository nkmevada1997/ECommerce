using Ecommerce.BAL.Interface;
using Ecommerce.Common.Enum;
using Ecommerce.DAL.Models;
using Ecommerce.Helper.Encode;
using Ecommerce.Models.Customers.AddCustomer;
using Ecommerce.Models.Customers.EditCustomer;
using Ecommerce.Models.Dropdown;
using ECommerce.Helper.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [LoggedIn]
    public class CustomersController : Controller
    {
        private readonly IService<Customer> service;
        private readonly IService<User> userService;
        private readonly IService<Country> countryService;

        public CustomersController(IService<Customer> service, IService<User> userService, IService<Country> countryService)
        {
            this.service = service;
            this.userService = userService;
            this.countryService = countryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(this.service.GetAll());
        }

        public IActionResult AddCustomer()
        {
            IList<CountryDropdown> countryDropdown = new List<CountryDropdown>();
            var countries = this.countryService.GetAll();

            if (countries != null && countries.Count > 0)
            {
                foreach (var country in countries)
                {
                    countryDropdown.Add(new CountryDropdown
                    {
                        CountryId = country.CountryId,
                        CountryName = country.CountryName,
                    });
                }

                ViewBag.CoutryItems = countryDropdown;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCustomer(AddCustomerModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    CustomerId = Guid.NewGuid(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DOB = model.DOB,
                    Gender = model.Gender,
                    Email = model.Email,
                    Password = EncodeBase.EncodeBase64(model.Password),
                    Country = model.Country,
                    State = model.State,
                    City = model.City,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                };
                this.service.Insert(customer);

                var user = new User
                {
                    UserId = Guid.NewGuid(),
                    CustomerId = customer.CustomerId,
                    Email = model.Email,
                    Password = EncodeBase.EncodeBase64(model.Password),
                    UserName = model.FirstName + " " + model.LastName,
                    UserType = UserType.Customer,
                    CanLogin = true,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                };

                this.userService.Insert(user);

                return RedirectToAction("Index", "Customers");
            }

            return View();
        }

        [HttpGet]
        public IActionResult GetCustomer(Guid customerId)
        {
            var customer = this.service.Get(customerId);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpGet]
        public IActionResult EditCustomer(Guid customerId)
        {
            var customer = this.service.Get(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                var model = new EditCustomerModel
                {
                    CustomerId = customerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    DOB = customer.DOB,
                    Gender = customer.Gender,
                    Country = customer.Country,
                    State = customer.State,
                    City = customer.City,
                };
                return View(model);
            }
        }

        [HttpPost, ActionName("EditCustomer")]
        public IActionResult EditCustomer(EditCustomerModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = this.service.Get(model.CustomerId);
                if (customer != null)
                {
                    customer.FirstName = model.FirstName;
                    customer.LastName = model.LastName;
                    customer.DOB = model.DOB;
                    customer.Gender = model.Gender;
                    customer.Country = model.Country;
                    customer.State = model.State;
                    customer.City = model.City;

                    this.service.Update(customer);
                    return RedirectToAction("Index", "Customers");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult DeleteCustomer(Guid customerId)
        {
            var customer = this.service.Get(customerId);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("DeleteCustomer")]
        public IActionResult ConfirmDelete(Guid customerId)
        {
            var users = this.userService.GetAll();
            this.service.Delete(customerId);

            if (users != null && users.Count() > 0)
            {
                var user = users.SingleOrDefault(x => x.CustomerId == customerId);
                if (user != null)
                {
                    this.userService.Delete(user.UserId);
                }
            }
            return RedirectToAction("Index", "Customers");
        }
    }
}
