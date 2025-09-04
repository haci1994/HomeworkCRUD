using HomeworkCRUD.DataContext;
using HomeworkCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeworkCRUD.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public FooterViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var socials = await _dbContext.Socials.ToListAsync();
            var headerElement = await _dbContext.HeaderElements.FirstOrDefaultAsync();

            FooterViewModel model = new FooterViewModel
            {
                Socials = socials,
                HeaderElement = headerElement
            };

            return View(model);
        }
    }
}
