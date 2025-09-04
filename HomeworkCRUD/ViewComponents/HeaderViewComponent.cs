using HomeworkCRUD.DataContext;
using HomeworkCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeworkCRUD.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public HeaderViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var socials = await _dbContext.Socials.ToListAsync();
            var headerElement = await _dbContext.HeaderElements.FirstOrDefaultAsync();

            HeaderViewModel model = new HeaderViewModel
            {
                Socials = socials,
                HeaderElement = headerElement
            };

            return View(model);
        }
    }
}
