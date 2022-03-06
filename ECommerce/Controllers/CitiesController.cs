using Ecommerce.BAL.Interface;
using Ecommerce.DAL.Models;
using Ecommerce.Models.Cities.AddCity;
using Ecommerce.Models.Cities.EditCity;
using ECommerce.Helper.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [LoggedIn]
    public class CitiesController : Controller
    {
        private readonly IService<City> service;

        public CitiesController(IService<City> service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(this.service.GetAll());
        }

        public IActionResult AddCity()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCity(AddCityModel model)
        {
            if (ModelState.IsValid)
            {
                var city = new City
                {
                    CityId = Guid.NewGuid(),
                    CityName = model.CityName,
                    StateId = model.StateId,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                };

                this.service.Insert(city);

                return RedirectToAction("Index", "Cities");
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetCity(Guid cityId)
        {
            var city = this.service.Get(cityId);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        [HttpGet]
        public IActionResult EditCity(Guid cityId)
        {
            var city = this.service.Get(cityId);
            if (city == null)
            {
                return NotFound();
            }
            else
            {
                var model = new EditCityModel
                {
                    CityId = cityId,
                    StateId = city.StateId,
                    CityName = city.CityName,
                };
                return View(model);
            }
        }

        [HttpPost, ActionName("EditCity")]
        public IActionResult EditCity(EditCityModel model)
        {
            if (ModelState.IsValid)
            {
                var city= this.service.Get(model.CityId);
                if (city != null)
                {
                    city.CityName = model.CityName;
                    city.StateId= model.StateId;
                    this.service.Update(city);

                    return RedirectToAction("Index", "Cities");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteCity(Guid cityId)
        {
            var city = this.service.Get(cityId);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        [HttpPost, ActionName("DeleteCity")]
        public IActionResult ConfirmDelete(Guid stateId)
        {
            this.service.Delete(stateId);
            return RedirectToAction("Index", "Cities");
        }

        
    }
}
