using System.Diagnostics;
using HomeworkCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkCRUD.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
