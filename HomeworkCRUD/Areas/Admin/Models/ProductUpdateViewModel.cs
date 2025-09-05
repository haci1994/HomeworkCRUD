using HomeworkCRUD.DataContext.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeworkCRUD.Areas.Admin.Models
{
    public class ProductUpdateViewModel
    {
        public required string Name { get; set; }

        public string? CoverImageUrl { get; set; }

        public List<ProductImage>? Images { get; set; }= [];

        public IFormFile? CoverImageFile { get; set; }

        public List<IFormFile>? ImageFiles { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; } = [];
    }
}
