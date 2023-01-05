using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace MVC.Controllers
{
    public class DateCalculatorController : Controller
    {
        private IUserService _userService;

        public DateCalculatorController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var Id = Convert.ToInt32(HttpContext.User.Claims.Where(o => o.Type.ToString() == "Id").Select(o => o.Value).FirstOrDefault());
            var sonuc = _userService.GetId(Id);
            return View(sonuc);
        }
    }
}
