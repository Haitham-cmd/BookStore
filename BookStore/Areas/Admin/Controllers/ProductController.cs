using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public ProductController(IUnitOfWork db)
        {
            unitOfWork = db;
        }
        public IActionResult Index()
        {
            var products = unitOfWork.Product.GetAll();
        
            return View(products);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> categoryList = unitOfWork.Category.GetAll()
            .Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            ViewBag.CategoryList = categoryList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        { 
            if (ModelState.IsValid)
            {
                unitOfWork.Product.Add(product);
                unitOfWork.Save();

                TempData["success"] = "Record created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = unitOfWork.Product.Get(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Product.Update(product);
                unitOfWork.Save();

                TempData["success"] = "Record updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = unitOfWork.Product.Get(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var product = unitOfWork.Product.Get(p => p.Id == id);
            if (product != null && ModelState.IsValid)
            {
                unitOfWork.Product.Remove(product);
                unitOfWork.Save();

                TempData["success"] = "Record deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
    }
}
