using E_Commerce.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration configuration;
        UserDAL userDAL;
        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
            userDAL = new UserDAL(this.configuration);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            try
            {
                int result = userDAL.UserRegister(user);
                if (result == 1)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult? Login(User user)
        {
            User u = userDAL.UserLogin(user);
            
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, u.F_Name) },
                CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            HttpContext.Session.SetInt32("userid", u.User_Id);
            HttpContext.Session.SetString("email", u.Email);
            HttpContext.Session.SetString("fname", u.F_Name);
            HttpContext.Session.SetInt32("role_id", u.Role_Id);
            if (u != null)
            {
                //string decryptpass = userDAL.Decrypt(u.Password);
                //u.Password = decryptpass;
                if (u.Role_Id == 1)
                {
                    return RedirectToAction("List", "Product");
                }
                else if (u.Role_Id == 2)
                {
                    return RedirectToAction("AllProducts", "Home");
                }
                return View();
            }
            return View();

        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            var StoredCookies = Request.Cookies.Keys;
            foreach (var Cookie in StoredCookies)
            {
                Response.Cookies.Delete(Cookie);
            }
            return RedirectToAction("Login");
        }

    }
}
