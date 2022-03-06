using Ecommerce.BAL.Interface;
using Ecommerce.DAL.Data;
using Ecommerce.DAL.Models;
using Ecommerce.Models.Dropdown;
using Ecommerce.Models.States.AddState;
using Ecommerce.Models.States.EditState;
using ECommerce.Helper.Attributes;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            return View(this.countryService.GetAll());
        }

        public IActionResult AddState()
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
        public IActionResult AddState(AddStateModel model)
        {
            if (ModelState.IsValid)
            {
                var state = new State
                {
                    StateId = Guid.NewGuid(),
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
                    StateId = state.StateId,
                    CountryId = state.CountryId,
                    StateName = state.StateName,
                };
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

            return View(state);
        }

        [HttpPost, ActionName("DeleteState")]
        public IActionResult ConfirmDelete(Guid stateId)
        {
            this.service.Delete(stateId);
            return RedirectToAction("Index", "States");
        }
    }
}
