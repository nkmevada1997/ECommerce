using Ecommerce.BAL.Interface;
using Ecommerce.DAL.Models;
using Ecommerce.Models.Suppliers.AddSupplier;
using Ecommerce.Models.Suppliers.EditSupplier;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly IService<Supplier> service;

        public SuppliersController(IService<Supplier> service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(this.service.GetAll());
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
                    SupplierId = Guid.NewGuid(),
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    Country = model.Country,
                    State = model.State,
                    City = model.City,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                };
                this.service.Insert(supplier);
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
                    SupplierId = supplier.SupplierId,
                    Name = supplier.Name,
                    Country = supplier.Country,
                    State = supplier.State,
                    City = supplier.City,
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
                    supplier.Name= model.Name;
                    supplier.Country = model.Country;
                    supplier.State = model.State;
                    supplier.City = model.City;

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
            this.service.Delete(supplierId);
            return RedirectToAction("Index", "Suppliers");
        }
    }
}
