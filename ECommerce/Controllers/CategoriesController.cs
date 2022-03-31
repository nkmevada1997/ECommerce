using Ecommerce.BAL.Interface;
using Ecommerce.DAL.Data;
using Ecommerce.DAL.Models;
using Ecommerce.Models.Categories.AddCategory;
using Ecommerce.Models.Categories.EditCategory;
using ECommerce.Helper.Attributes;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace ECommerce.Controllers
{
    [LoggedIn]
    public class CategoriesController : Controller
    {
        private readonly IService<Category> service;
        private readonly ApplicationDbContext context;

        public CategoriesController(IService<Category> service, ApplicationDbContext context)
        {
            this.service = service;
            this.context = context;
        }

        public IActionResult Index(int? page)
        {
            var categories = this.context.Categories.Where(x => x.IsDeleted == false).ToList();

            ViewBag.ShowPagination = false;
            ViewBag.Count = categories.Count;
            if (categories.Count > 5)
            {
                ViewBag.ShowPagination = true;
            }
            return View(categories.ToPagedList(page ?? 1, 5));
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(AddCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = model.CategoryName,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                };

                this.service.Insert(category);

                return RedirectToAction("Index", "Categories");
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetCategory(Guid categoryId)
        {
            var category = this.service.Get(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult EditCategory(Guid categoryId)
        {
            var category = this.service.Get(categoryId);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                var model = new EditCategoryModel
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                };

                return View(model);
            }
        }

        [HttpPost, ActionName("EditCategory")]
        public IActionResult EditCategory(EditCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = this.service.Get(model.CategoryId);
                if (category != null)
                {
                    category.CategoryName = model.CategoryName;
                    this.service.Update(category);

                    return RedirectToAction("Index", "Categories");
                }
            }
            return View();
        }

        public IActionResult DeleteCategory(Guid categoryId)
        {
            var category = this.service.Get(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost,ActionName("DeleteCategory")]
        public IActionResult ConfirmDelete(Guid categoryId)
        {
            var category = this.service.Get(categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                this.service.Update(category);
            }
            return RedirectToAction("Index", "Categories");
        }
    }
}
