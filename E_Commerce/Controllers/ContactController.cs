using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult ContactUs()
        {
            return View();
        }
        
        [HttpPost]
     //   [Authorize(Roles = "Admin")]
        public IActionResult ContactUs(IFormCollection form)
        {
            ViewBag.Name = form["name"];
            ViewBag.Email = form["email"];
            ViewBag.Contact = form["contact_no"];
            ViewBag.Message = form["message"];

            return View("Queries");
        }
    }
}
