using HomeworkCRUD.DataContext;
using HomeworkCRUD.DataContext.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeworkCRUD.Areas.Admin.Controllers
{

    public class CategoryController : AdminController
    {
        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _dbContext.Categories.ToListAsync();

            if(categories==null) return NotFound();

            return View(categories);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (!ModelState.IsValid) return View(category);

            var existCategory = _dbContext.Categories.Any(x=> x.Name == category.Name);

            if (existCategory) 
            {
                ModelState.AddModelError("Name", $"{category.Name} - this name is exist!");
                return View(category);
            }

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);

            if (category == null) return NotFound();

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));    
        }

        public IActionResult Update(int id)
        {
            var category = _dbContext.Categories.Find(id);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (!ModelState.IsValid) return View(category);

            if (id != category.Id) return BadRequest();

            var existCategory = await _dbContext.Categories.AnyAsync(x => x.Name == category.Name && x.Id != category.Id);
          
            if (existCategory)
            {
                ModelState.AddModelError("Name", $"{category.Name} - this name is exist!");
                return View(category);
            }
           
            var dbCategory = await _dbContext.Categories.FindAsync(id);
           
            if (dbCategory == null) return NotFound();
           
            dbCategory.Name = category.Name;
           
            await _dbContext.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
    }
}
