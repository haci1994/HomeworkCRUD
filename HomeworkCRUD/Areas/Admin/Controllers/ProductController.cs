using HomeworkCRUD.DataContext;
using HomeworkCRUD.DataContext.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeworkCRUD.Areas.Admin.Controllers
{
    public class ProductController : AdminController
    {
        private readonly AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _dbContext.Products.ToListAsync();

            if (products == null) return NotFound();

            return View(products);
        }

        public IActionResult Delete(int id, Product product)
        {
            var existProduct = _dbContext.Products.Find(id);

            if (existProduct == null) return NotFound();

            _dbContext.Products.Remove(existProduct);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
