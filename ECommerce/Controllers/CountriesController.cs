﻿using Ecommerce.BAL.Interface;
using Ecommerce.DAL.Models;
using Ecommerce.Models.Counties.AddCountry;
using Ecommerce.Models.Countries.EditCountry;
using ECommerce.Helper.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [LoggedIn]
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
            return View(this.service.GetAll().OrderBy(x => x.CountryName));
        }

        public IActionResult AddCountry()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCountry(AddCountryModel model)
        {
            if (ModelState.IsValid)
            {
                var country = new Country
                {
                    CountryId = Guid.NewGuid(),
                    CountryName = model.CountryName,
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
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        [HttpGet]
        public IActionResult EditCountry(Guid CountryId)
        {
            var country = this.service.Get(CountryId);
            if (country == null)
            {
                return NotFound();
            }
            else
            {
                var model = new EditCountryModel
                {
                    CountryId = country.CountryId,
                    CountryName = country.CountryName,
                };
                return View(model);
            }
        }

        [HttpPost, ActionName("EditCountry")]
        public IActionResult EditCountry(EditCountryModel model)
        {
            if (ModelState.IsValid)
            {
                var country = this.service.Get(model.CountryId);
                if (country != null)
                {
                    country.CountryName = model.CountryName;
                    this.service.Update(country);

                    return RedirectToAction("Index", "Countries");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteCountry(Guid countryId)
        {
            var country = this.service.Get(countryId);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        [HttpPost, ActionName("DeleteCountry")]
        public IActionResult ConfirmDelete(Guid countryId)
        {
            this.service.Delete(countryId);
            return RedirectToAction("Index", "Countries");
        }

    }
}
