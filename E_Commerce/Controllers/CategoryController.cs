using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        CategoryCRUD crud;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
            crud = new CategoryCRUD(_context);
        }
        // GET: CategoryController
        public ActionResult List()
        {
            return View(crud.GetAllCategories());
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var result = crud.GetCategoryById(id);
            return View(result);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category cat)
        {
            try
            {
                int result = crud.AddCategory(cat);
                if (result == 1)
                    return RedirectToAction(nameof(List));
                else
                    return BadRequest();
            }
            catch
            {
                return BadRequest();
            }

        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = crud.GetCategoryById(id);
            return View(model);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category cat)
        {
            try
            {
                int result = crud.UpdateCategory(cat);
                if (result == 1)
                    return RedirectToAction(nameof(List));
                else
                    return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = crud.GetCategoryById(id);
            return View(model);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int result = crud.DeleteCategory(id);
                if (result == 1)
                    return RedirectToAction(nameof(List));
                else
                    return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
