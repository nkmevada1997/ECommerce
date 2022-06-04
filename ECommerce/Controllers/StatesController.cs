using Ecommerce.BAL.Interface;
using Ecommerce.DAL.Data;
using Ecommerce.DAL.Models;
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
        private readonly ApplicationDbContext context;

        public StatesController(IService<State> service, ApplicationDbContext context)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index(int? page, Guid? countryId)
        {
            var states = this.context.States.Where(x => x.IsDeleted == false).Include(x => x.Country).OrderBy(x => x.StateName).ToList();

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
                    CountryId = model.CountryId.Value,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                };

                this.service.Insert(state);

                return RedirectToAction("Index", "States");
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetStates()
        {
            var states = this.service.GetAll().Where(x => x.IsDeleted == false).OrderBy(x => x.StateName).ToList();
            return Json(new
            {
                Data = states
            });
        }

        [HttpPost]
        public IActionResult GetStateByCountry(Guid countryId)
        {
            var states = service.GetAll().Where(x => x.CountryId == countryId).ToList();

            return Json(new
            {
                Data = states.OrderBy(x => x.StateName).ToList()
            }); ;
        }

        [HttpGet]
        public IActionResult GetState(Guid stateId)
        {
            var state = this.context.States.Where(x => x.Id == stateId && x.IsDeleted == false).Include(x => x.Country).OrderBy(x => x.StateName).FirstOrDefault();
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
                    StateId = state.Id,
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
            var state = this.context.States.Where(x => x.Id == stateId && x.IsDeleted == false).Include(x => x.Country).OrderBy(x => x.StateName).FirstOrDefault();
            if (state == null)
            {
                return NotFound();
            }
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
    }
}
