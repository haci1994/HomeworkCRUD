using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeworkCRUD.Areas.Admin.Models
{
    public class ProductCreateViewModel
    {
        public required string Name { get; set; }

        public IFormFile CoverImageFile { get; set; } = null!;
        public List<IFormFile> ImageFiles { get; set; } = null!;

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; } = [];
    }
}
