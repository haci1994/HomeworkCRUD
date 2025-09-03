using System.ComponentModel.DataAnnotations.Schema;

namespace HomeworkCRUD.DataContext.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        public int Stock { get; set; }

        public string? Description { get; set; }

        public decimal Weight { get; set; }

        public string? CoverImageUrl { get; set; }

        public int ReviewCount { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}
