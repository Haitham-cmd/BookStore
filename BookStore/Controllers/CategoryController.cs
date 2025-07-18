using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        // Injecting the ApplicationDbContext to interact with the database
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var categories = _db.Categories
                .OrderBy(c => c.Displayorder)
                .ToList();
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
                _db.Categories.Add(category);
                _db.SaveChanges();
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
            var category = _db.Categories.Find(id);
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
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}
