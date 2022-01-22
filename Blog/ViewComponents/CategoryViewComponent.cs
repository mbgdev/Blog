using Blog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Blog.ViewComponets
{
    public class CategoryViewComponent:ViewComponent
    {
        private readonly BlogDbContext context;

        public CategoryViewComponent(BlogDbContext context)
        {
            this.context = context;
        }
        public async  Task<IViewComponentResult> InvokeAsync()
        {
             
            var model=(from q in context.Categories
                      select q).ToList();


            return View(model);
        }



    }
}
