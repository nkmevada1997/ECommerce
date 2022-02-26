using Ecommerce.BAL.Interface;
using Ecommerce.DAL.Models;
using Ecommerce.Models.Customers.AddCustomer;
using Ecommerce.Models.Customers.EditCustomer;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IService<Customer> service;
        private readonly IService<User> userService;

        public CustomersController(IService<Customer> service,IService<User> userService)
        {
            this.service = service;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(this.service.GetAll());
        }

        public IActionResult AddCustomer()
        {
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
                    Password = model.Password,
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
                    UserName = model.Email,
                    Password = model.Password,
                    UserType = "Customer",
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
            this.service.Delete(customerId);
            return RedirectToAction("Index", "Customers");
        }
    }
}
