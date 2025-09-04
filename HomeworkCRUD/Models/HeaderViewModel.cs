using HomeworkCRUD.DataContext.Models;

namespace HomeworkCRUD.Models
{
    public class HeaderViewModel
    {
        public List<Social> Socials { get; set; } = [];
        public HeaderElement? HeaderElement { get; set; }
    }
}
