using Ecommerce.BAL.Interface;
using Ecommerce.DAL.Models;
using Ecommerce.Models.Counties.AddCountry;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class CountriesController : Controller
    {
        private readonly IService<Country> service;

        public CountriesController(IService<Country> service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(this.service.GetAll());
        }

        public IActionResult AddCountry()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCountry(AddCountryModel model)
        {
            if(ModelState.IsValid)
            {
                var country = new Country
                {
                    CountryId = Guid.NewGuid(),
                    Name = model.Name,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                };

                this.service.Insert(country);

                return RedirectToAction("Index", "Countries");
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetCountry(Guid countryId)
        {
            var country = this.service.Get(countryId);
            if(country == null)
            {
                return NotFound();
            }

            return View(country);
        }
    }
}
