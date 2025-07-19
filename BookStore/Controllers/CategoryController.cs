
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStoreDataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork unitOfWork;
        public CategoryController(IUnitOfWork db)
        {
            unitOfWork = db;
        }
        public IActionResult Index()
        {
            var categories = unitOfWork.Category.GetAll();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category != null)
            {
                if (category.Name == category.Displayorder.ToString())
                {
                    ModelState.AddModelError("Displayorder", "The display order can not match the name");
                }

                if (category.Displayorder == 0 || category.Displayorder.ToString() == string.Empty)
                {
                    ModelState.AddModelError("Displayorder", "The Display Order Value can not be null or 0");
                }
            }

            if (ModelState.IsValid)
            {
                unitOfWork.Category.Add(category);
                unitOfWork.Save();

                TempData["success"] = "Category created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = unitOfWork.Category.Get(p => p.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category != null)
            {
                if (category.Name == category.Displayorder.ToString())
                {
                    ModelState.AddModelError("Displayorder", "The display order can not match the name");
                }

                if (category.Displayorder == 0 || category.Displayorder.ToString() == string.Empty)
                {
                    ModelState.AddModelError("Displayorder", "The Display Order Value can not be null or 0");
                }
            }

            if (ModelState.IsValid)
            {
                unitOfWork.Category.Update(category);
                unitOfWork.Save();

                TempData["success"] = "Category updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = unitOfWork.Category.Get(p => p.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var category = unitOfWork.Category.Get(p => p.Id == id);
            if (category != null && ModelState.IsValid)
            {
                unitOfWork.Category.Remove(category);
                unitOfWork.Save();

                TempData["success"] = "Category deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}
