using Ecommerce.BAL.Interface;
using Ecommerce.Common.Enum;
using Ecommerce.DAL.Models;
using Ecommerce.Helper.Encode;
using Ecommerce.Models.Suppliers.AddSupplier;
using Ecommerce.Models.Suppliers.EditSupplier;
using ECommerce.Helper.Attributes;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace ECommerce.Controllers
{
    [LoggedIn]
    public class SuppliersController : Controller
    {
        private readonly IService<Supplier> service;
        private readonly IService<User> userService;

        public SuppliersController(IService<Supplier> service, IService<User> userService)
        {
            this.service = service;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            var suppliers = this.service.GetAll().Where(x => x.IsDeleted == false).ToList();
            ViewBag.ShowPagination = false;
            ViewBag.Count = suppliers.Count;

            if (suppliers.Count > 5)
            {
                ViewBag.ShowPagination = true;
            }
            return View(suppliers.ToPagedList(page ?? 1, 5));
        }

        public IActionResult AddSupplier()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSupplier(AddSupplierModel model)
        {
            if (ModelState.IsValid)
            {
                var supplier = new Supplier
                {
                    Id= Guid.NewGuid(),
                    SupplierName = model.SupplierName,
                    Email = model.Email,
                    Password = EncodeBase.EncodeBase64(model.Password),
                    CountryId = model.CountryId,
                    StateId = model.StateId,
                    CityId = model.CityId,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                };
                this.service.Insert(supplier);

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    SupplierId = supplier.Id,
                    Email = model.Email,
                    Password = EncodeBase.EncodeBase64(model.Password),
                    UserName = model.SupplierName,
                    UserType = UserType.Supplier,
                    CanLogin = true,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                };

                this.userService.Insert(user);
                return RedirectToAction("Index", "Suppliers");
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetSupplier(Guid supplierId)
        {
            var supplier = this.service.Get(supplierId);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        [HttpGet]
        public IActionResult EditSupplier(Guid supplierId)
        {
            var supplier = this.service.Get(supplierId);
            if (supplier == null)
            {
                return NotFound();
            }
            else
            {
                var model = new EditSupplierModel
                {
                    SupplierId = supplier.Id,
                    SupplierName = supplier.SupplierName,
                    CountryId = supplier.CountryId,
                    StateId = supplier.StateId,
                    CityId = supplier.CityId,
                };
                return View(model);
            }
        }

        [HttpPost, ActionName("EditSupplier")]
        public IActionResult EditSupplier(EditSupplierModel model)
        {
            if (ModelState.IsValid)
            {
                var supplier = this.service.Get(model.SupplierId);
                if (supplier != null)
                {
                    supplier.SupplierName = model.SupplierName;
                    supplier.CountryId = model.CountryId;
                    supplier.StateId = model.StateId;
                    supplier.CityId = model.CityId;

                    this.service.Update(supplier);
                    return RedirectToAction("Index", "Suppliers");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult DeleteSupplier(Guid supplierId)
        {
            var supplier = this.service.Get(supplierId);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        [HttpPost, ActionName("DeleteSupplier")]
        public IActionResult ConfirmDelete(Guid supplierId)
        {
            var users = this.userService.GetAll();
            var supplier = this.service.Get(supplierId);

            if (supplier != null)
            {
                supplier.IsDeleted = true;
                this.service.Update(supplier);
            }
            if (users != null && users.Count() > 0)
            {
                var user = users.SingleOrDefault(x => x.SupplierId == supplierId);
                if (user != null)
                {
                    this.userService.Delete(user.Id);
                }
            }
            return RedirectToAction("Index", "Suppliers");
        }
    }
}
