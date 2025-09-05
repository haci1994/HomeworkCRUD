using HomeworkCRUD.Areas.Admin.Data;
using HomeworkCRUD.Areas.Admin.Models;
using HomeworkCRUD.DataContext;
using HomeworkCRUD.DataContext.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> Create()
        {
            var categories = await _dbContext.Categories.ToListAsync();

            var categorySelectListItems = new List<SelectListItem>();

            categories.ForEach(c =>
            {
                categorySelectListItems.Add(new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
            });

            var productCreateViewModel = new ProductCreateViewModel
            {
                Categories = categorySelectListItems,
                CoverImageFile = null!,
                Name = string.Empty
            };

            return View(productCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            if (!model.CoverImageFile.IsImage())
            {

                ModelState.AddModelError("CoverImageFile", "The uploaded file must be an image.");
                model.Categories = await GetCategories();
                return View(model);
            }

            if (model.CoverImageFile.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("CoverImageFile", "The image size must not exceed 2MB.");
                model.Categories = await GetCategories();
                return View(model);
            }

            if (!await _dbContext.Categories.AnyAsync(x => x.Id == model.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "The selected category does not exist.");
                model.Categories = await GetCategories();
                return View(model);
            }

            var uniqueFileName = await model.CoverImageFile.GenerateFileAsync(PathConstants.ProductPath);

            foreach(var imageFile in model.ImageFiles)
            {
                if (!imageFile.IsImage() || imageFile.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImageFiles", "All additional images must be valid images and not exceed 2MB each.");
                    model.Categories = await GetCategories();
                    return View(model);
                }                
            }

            var productImages = new List<ProductImage>();

            foreach (var imageFile in model.ImageFiles)
            {
                var imageFileName = await imageFile.GenerateFileAsync(PathConstants.ProductPath);
                var productImage = new ProductImage
                {
                    ImageUrl = imageFileName
                };

                productImages.Add(productImage);
            }

            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId,
                CoverImageUrl = uniqueFileName,
                ProductImages = productImages
            };

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<List<SelectListItem>> GetCategories()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            var categorySelectListItems = new List<SelectListItem>();
            categories.ForEach(c =>
            {
                categorySelectListItems.Add(new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
            });

            return categorySelectListItems;
        }
    }

    public static class FormFileExtensions
    {
        public static bool IsImage(this IFormFile file)
        {
            return file != null && file.ContentType.StartsWith("image/");
        }

        public static async Task<string> GenerateFileAsync(this IFormFile file, string rootPath)
        {
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(rootPath, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return uniqueFileName;
        }
    }
}
