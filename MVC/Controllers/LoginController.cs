using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IToastNotification _toastNotification;

        public LoginController(IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
        }
        public IActionResult Index(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            Context c = new Context();
            var info = c.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (info != null)
            {
                var claims = new List<Claim>
                { new Claim(ClaimTypes.Name,info.Email),
                   new Claim("Id",info.UserID.ToString())
                };

                var identity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal princible = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(princible);

                _toastNotification.AddSuccessToastMessage("Hoşgeldiniz");

                if (TempData["returnUrl"] != null)
                {
                    if (Url.IsLocalUrl(TempData["returnUrl"].ToString()))
                    {
                        return Redirect(TempData["returnUrl"].ToString());
                    }
                }
                else
                {
                    return RedirectToAction("Index", "DateCalculator");
                }

            }

            _toastNotification.AddErrorToastMessage("Bilgilerinizi kontrol ediniz!");
            return View();

        }
    }
}
