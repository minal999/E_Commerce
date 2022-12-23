using E_Commerce.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        ProductCRUD crud;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
            crud = new ProductCRUD(_context);
        }

        public IActionResult List()
        {
            //Product p = new Product();
            //HttpContext.Session.SetString("img", p.Product_Img);
            return View(crud.GetAllProducts());
           
        }
        public void LoadDLL()
        {
            try
            {
                List<Category> categories = new List<Category>();
                categories = _context.categories.ToList();
                categories.Insert(0, new Category { Category_Id = 0, Category_Name = "Please Select" });

                ViewBag.categories = categories;
            }
            catch (Exception ex)
            {

            }
        }
        // GET: ProductController
        //public ActionResult AllProducts()
        //{
        //    return View();
        //}


        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            LoadDLL();
            var result = crud.GetProductById(id);
            return View(result);
        }

        // GET: ProductController/Create
        // [Role(Role_Name ="Admin")]
        public ActionResult Create()
        {
            LoadDLL();
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                
                int result = crud.AddProduct(product);
                if (result == 1)
                    return RedirectToAction("List");
                else
                    return BadRequest();
            }
            catch
            {
                return BadRequest();
            }

        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            LoadDLL();
            var model = crud.GetProductById(id);
            return View(model);

        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            try
            {
                int result = crud.UpdateProduct(product);
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            LoadDLL();
            var model = crud.GetProductById(id);
            return View(model);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int result = crud.DeleteProduct(id);
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
        //public ActionResult AllCategories()
        //{
        //    LoadDLL();
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult AllCategories(int category_id)
        //{
        //   if (!string.IsNullOrWhiteSpace(category_id) && category_id.Length == 3)
        //    {
        //        var repo = new RegionsRepository();

        //        IEnumerable<SelectListItem> regions = repo.GetRegions(iso3);
        //        return Json(regions, JsonRequestBehavior.AllowGet);
        //    }
        //    return null;
        //}

    }
}
