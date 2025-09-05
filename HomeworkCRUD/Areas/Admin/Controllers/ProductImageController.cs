using HomeworkCRUD.DataContext;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkCRUD.Areas.Admin.Controllers
{
    public class ProductImageController : AdminController
    {
        private readonly AppDbContext _dbContext;
        public ProductImageController(AppDbContext dbContext) => _dbContext = dbContext;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var img = await _dbContext.ProductImages.FindAsync(id);
            if (img == null) return NotFound();

            _dbContext.ProductImages.Remove(img);
            await _dbContext.SaveChangesAsync();

            return Json(new { ok = true, removedId = id }); 
        }

    }
}
