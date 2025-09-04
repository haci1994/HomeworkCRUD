using HomeworkCRUD.DataContext;
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

            return View(products);
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
