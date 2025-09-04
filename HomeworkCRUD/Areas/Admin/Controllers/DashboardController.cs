using Microsoft.AspNetCore.Mvc;

namespace HomeworkCRUD.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : AdminController
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
