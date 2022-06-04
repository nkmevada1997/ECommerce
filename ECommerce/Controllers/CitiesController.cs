using Ecommerce.BAL.Interface;
using Ecommerce.DAL.Data;
using Ecommerce.DAL.Models;
using Ecommerce.Models.Cities.AddCity;
using Ecommerce.Models.Cities.EditCity;
using Ecommerce.Models.Dropdown;
using ECommerce.Helper.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace ECommerce.Controllers
{
    [LoggedIn]
    public class CitiesController : Controller
    {
        private readonly IService<City> service;
        private readonly ApplicationDbContext context;

        public CitiesController(IService<City> service, ApplicationDbContext context)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index(int? page)
        {
            var cities = this.context.Cities.Where(x => x.IsDeleted == false).Include(x => x.State).OrderBy(x => x.CityName).ToList();

            ViewBag.ShowPagination = false;
            ViewBag.Count = cities.Count;
            if (cities.Count > 5)
            {
                ViewBag.ShowPagination = true;
            }
            return View(cities.ToPagedList(page ?? 1, 5));
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
                    Id = Guid.NewGuid(),
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

        [HttpPost]
        public IActionResult GetCityByState(Guid stateId)
        {
            var cities = this.service.GetAll().Where(x => x.StateId == stateId).ToList();

            return Json(new
            {
                data = cities.OrderBy(x => x.CityName).ToList()
            }); ;
        }

        [HttpGet]
        public IActionResult GetCity(Guid cityId)
        {
            var city = this.context.Cities.Where(x => x.Id ==cityId && x.IsDeleted == false).Include(x => x.State).SingleOrDefault();
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
                var city = this.service.Get(model.CityId);
                if (city != null)
                {
                    city.CityName = model.CityName;
                    city.StateId = model.StateId;
                    this.service.Update(city);

                    return RedirectToAction("Index", "Cities");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteCity(Guid cityId)
        {
            var city = this.context.Cities.Where(x => x.Id == cityId && x.IsDeleted == false).Include(x => x.State).SingleOrDefault();
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        [HttpPost, ActionName("DeleteCity")]
        public IActionResult ConfirmDelete(Guid cityId)
        {
            var city = this.service.Get(cityId);

            if (city != null)
            {
                city.IsDeleted = true;
                this.service.Update(city);
            }

            return RedirectToAction("Index", "Cities");
        }
    }
}
