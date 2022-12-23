using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.CodeAnalysis;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Helpers;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        ProductCRUD crud;
        //List<Cart> li=new List<Cart>();
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _context = context;
            crud = new ProductCRUD(_context);
            _contextAccessor = contextAccessor;
        }

        public IActionResult AllProducts()
        {
            TempData.Keep("cart");
            return View(crud.GetAllProducts());
        }

        public IActionResult ProductsByCategory(int? id)
        {
            ProductCategory pc = new ProductCategory();
            pc.Categories = _context.categories.ToList();
            if(id==null)
            {
                pc.Products=_context.products.ToList();
            }
            else
            {
                pc.Products= _context.products.Where(c=>c.Category_Id==id).ToList();
            }
            return View(pc);
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult AddToCart(int id)
        {
            
            var res = crud.GetProductById(id);
            return View(res);
        }
        List<Cart> cart = new List<Cart>();
        [HttpPost]
        public IActionResult AddToCart(Product prod, int quantity, int id)
        {
           
            Product p = _context.products.Where(x => x.Product_Id == id).SingleOrDefault();
            Cart c = new Cart();
            c.Product_Id = p.Product_Id;
            c.Product_Name = p.Product_Name;
            c.Product_Img = p.Product_Img;
            c.Price=p.Price;
            c.Quantity=quantity;
            c.Total = c.Quantity * c.Price;
            c.User_Id= (int)_contextAccessor.HttpContext.Session.GetInt32("userid");
            if (TempData["cart"]==null)
            {
                cart.Add(c);
                TempData["cart"] = JsonConvert.SerializeObject(cart);
                TempData.Keep("cart");
            }
            else
            {
                List<Cart> li = JsonConvert.DeserializeObject<List<Cart>>((string)TempData["cart"]);
                li.Add(c);
                TempData["cart"] = JsonConvert.SerializeObject(li);
                TempData.Keep("cart");
            }
            _context.cart.Add(c);
            _context.SaveChanges();
            return RedirectToAction("AllProducts");
        }

        public IActionResult CheckOut()
        {
            TempData.Keep("cart");
            if (TempData["cart"]!=null)
            {
                float total = 0;
                List<Cart> li = JsonConvert.DeserializeObject<List<Cart>>((string)TempData["cart"]);
                foreach (var item in li)
                {
                    total+=item.Total;
                }
                TempData["total"]=total;
            }
            return View();
        }

        //public IActionResult DeleteFromCart(int id)
        //{
        //    var c = _context.cart.Where(x => x.Product_Id == id).FirstOrDefault();
        //    _context.cart.Remove(c);
        //    _context.SaveChanges();
        //    int ci = cart.Count;
        //    return RedirectToAction("AllProducts");
        //}



        //if (TempData["cart"] != null)
        //{
        //    List<Cart> li = JsonConvert.DeserializeObject<List<Cart>>((string)TempData["cart"]);
        //    foreach (var item in cart)
        //    {
        //        if (item.Cart_Id == id)
        //        {
        //            li.Remove(item);

        //            break;
        //        }
        //    }
        //    TempData["cart"] = JsonConvert.SerializeObject(li);
        //    TempData.Keep();
        //}




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}