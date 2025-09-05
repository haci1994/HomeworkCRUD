using HomeworkCRUD.DataContext;
using HomeworkCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeworkCRUD.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _dbContext.Products.ToListAsync();
            var categories = await _dbContext.Categories.ToListAsync();

            if (products == null) return NotFound();
            if (categories == null) return NotFound();

            var model = new ShopViewModel
            {
                Products = products,
                Categories = categories
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();

            var product = await _dbContext.Products
                .Include(x=> x.Category)
                .Include(z=> z.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
