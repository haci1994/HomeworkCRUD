using HomeworkCRUD.DataContext.Models;

namespace HomeworkCRUD.Models
{
    public class ShopViewModel
    {
        public List<Product> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
    }
}
