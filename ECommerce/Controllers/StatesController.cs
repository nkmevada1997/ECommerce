using Ecommerce.BAL.Interface;
using Ecommerce.DAL.Data;
using Ecommerce.DAL.Models;
using Ecommerce.Models.Dropdown;
using Ecommerce.Models.States.AddState;
using Ecommerce.Models.States.EditState;
using ECommerce.Helper.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace ECommerce.Controllers
{
    [LoggedIn]
    public class StatesController : Controller
    {
        private readonly IService<State> service;
        private readonly IService<Country> countryService;
        private readonly ApplicationDbContext context;

        public StatesController(IService<State> service, IService<Country> countryService, ApplicationDbContext context)
        {
            this.service = service;
            this.countryService = countryService;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index(int? page, Guid? countryId)
        {
            var states = this.context.States.Where(x => x.IsDeleted == false).Include(x => x.Country).ToList();

            ViewBag.ShowPagination = false;
            ViewBag.Count = states.Count;

            if (states.Count > 5)
            {
                ViewBag.ShowPagination = true;

                if (countryId.HasValue)
                {
                    states = states.Where(x => x.CountryId == countryId.Value).ToList();
                }
            }
            return View(states.ToPagedList(page ?? 1, 5));
        }

        public IActionResult AddState()
        {
            ViewBag.CoutryItems = CountriesDropdownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddState(AddStateModel model)
        {
            if (ModelState.IsValid)
            {
                var state = new State
                {
                    Id = Guid.NewGuid(),
                    StateName = model.StateName,
                    CountryId = model.CountryId,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                };

                this.service.Insert(state);

                return RedirectToAction("Index", "States");
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetState(Guid stateId)
        {
            var state = this.service.Get(stateId);
            if (state == null)
            {
                return NotFound();
            }

            var country = this.countryService.Get(state.CountryId);
            state.Country = country;
            return View(state);
        }

        [HttpGet]
        public IActionResult EditState(Guid stateId)
        {
            var state = this.service.Get(stateId);
            if (state == null)
            {
                return NotFound();
            }
            else
            {
                var model = new EditStateModel
                {
                    StateId = state.Id,
                    CountryId = state.CountryId,
                    StateName = state.StateName,
                };
                ViewBag.CoutryItems = CountriesDropdownList();
                return View(model);
            }
        }

        [HttpPost, ActionName("EditState")]
        public IActionResult EditState(EditStateModel model)
        {
            if (ModelState.IsValid)
            {
                var state = this.service.Get(model.StateId);
                if (state != null)
                {
                    state.StateName = model.StateName;
                    state.CountryId = model.CountryId;
                    this.service.Update(state);

                    return RedirectToAction("Index", "States");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteState(Guid stateId)
        {
            var state = this.service.Get(stateId);
            if (state == null)
            {
                return NotFound();
            }
            var country = this.countryService.Get(state.CountryId);
            state.Country = country;

            return View(state);
        }

        [HttpPost, ActionName("DeleteState")]
        public IActionResult ConfirmDelete(Guid stateId)
        {
            var state = this.service.Get(stateId);

            if (state != null)
            {
                state.IsDeleted = true;
                this.service.Update(state);
            }
            return RedirectToAction("Index", "States");
        }

        private IList<CountriesDropdown> CountriesDropdownList()
        {
            IList<CountriesDropdown> countriesDropdownList = new List<CountriesDropdown>();
            var countries = this.countryService.GetAll().Where(x => x.IsDeleted == false).ToList();

            if (countries != null && countries.Count > 0)
            {
                foreach (var country in countries)
                {
                    countriesDropdownList.Add(new CountriesDropdown
                    {
                        CountryId = country.Id,
                        CountryName = country.CountryName,
                    });
                }

            }
            return countriesDropdownList;
        }
    }
}
