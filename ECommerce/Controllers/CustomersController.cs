using Ecommerce.BAL.Interface;
using Ecommerce.Common.Enum;
using Ecommerce.DAL.Data;
using Ecommerce.DAL.Models;
using Ecommerce.Helper.Encode;
using Ecommerce.Models.Customers.AddCustomer;
using Ecommerce.Models.Customers.EditCustomer;
using ECommerce.Helper.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace ECommerce.Controllers
{
    [LoggedIn]
    public class CustomersController : Controller
    {
        private readonly IService<Customer> service;
        private readonly IService<User> userService;
        private readonly ApplicationDbContext context;

        public CustomersController(IService<Customer> service, IService<User> userService, ApplicationDbContext context)
        {
            this.service = service;
            this.userService = userService;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            var customers = this.context.Customers.Where(x => x.IsDeleted == false).Include(x => x.Country).Include(x => x.State).Include(x => x.City).ToList();
            ViewBag.ShowPagination = false;
            ViewBag.Count = customers.Count;

            if (customers.Count > 5)
            {
                ViewBag.ShowPagination = true;
            }
            return View(customers.ToPagedList(page ?? 1, 5));
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
                    Id = Guid.NewGuid(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DOB = Convert.ToDateTime(model.DOB),
                    Gender = model.Gender,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Password = EncodeBase.EncodeBase64(model.Password),
                    CountryId = new Guid(model.CountryId),
                    StateId = new Guid(model.StateId),
                    CityId = new Guid(model.CityId),
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                };
                this.service.Insert(customer);

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    CustomerId = customer.Id,
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
            var customer = this.context.Customers.Where(x => x.Id == customerId && x.IsDeleted == false).Include(x => x.Country).Include(x => x.State).Include(x => x.City).SingleOrDefault();
            if (customer == null)
            {
                return NotFound();
            }
            ViewBag.Header = customer.FirstName + " " + customer.LastName;

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
                    CountryId = customer.CountryId.ToString(),
                    StateId = customer.StateId.ToString(),
                    CityId = customer.CityId.ToString(),
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
                    customer.CountryId = new Guid(model.CountryId);
                    customer.StateId = new Guid(model.StateId);
                    customer.CityId = new Guid(model.CityId);
                    this.service.Update(customer);
                    return RedirectToAction("Index", "Customers");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult DeleteCustomer(Guid customerId)
        {
            var customer = this.context.Customers.Where(x => x.Id == customerId && x.IsDeleted == false).Include(x => x.Country).Include(x => x.State).Include(x => x.City).SingleOrDefault();
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

            var customer = this.service.Get(customerId);

            if (customer != null)
            {
                customer.IsDeleted = true;
                this.service.Update(customer);
            }

            if (users != null && users.Count() > 0)
            {
                var user = users.SingleOrDefault(x => x.CustomerId == customerId);
                if (user != null)
                {
                    this.userService.Delete(user.Id);
                }
            }
            return RedirectToAction("Index", "Customers");
        }
    }
}
