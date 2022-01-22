using Blog.Areas.Admin.ViewModels;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly BlogDbContext context;

        public CategoriesController(BlogDbContext _context)
        {
            context = _context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await (from c in context.Categories
                               select new CategoryIndexViewModel
                               {
                                   Id = c.Id,
                                   Name = c.Name,
                                   PostCount = c.BlogPosts.Count
                               }).ToListAsync();

            return View(model);

        }




        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {


            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Lütfen girdiğiniz değerleri gözden geçirip tekrar deneyin.");
                return View(model);
            }


            Category cat = new Category
            {
                Name = model.Name,
            };

            await context.Categories.AddAsync(cat);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");

        }



        public async Task<IActionResult> Edit(int id)
        {
            var data = await context.Categories.FindAsync(id);

            if (data == null)
            {
                return RedirectToAction("index");
            }

            var model = new UpdateCategoryViewModel
            {
                Name = data.Name,
            };


            return View(model);


        }


        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategoryViewModel model, int Id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var data = await context.Categories.FindAsync(Id);
         
            data.Name = model.Name;

            context.Categories.Update(data);
            await context.SaveChangesAsync();
            return RedirectToAction("index");

        }




        public async Task<IActionResult> Delete(int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                ViewBag.Message = "Silmek istediğiniz kategori bulunamadı!";
            }
            return View(category);
        }


        //Todo: Ödev httppost isteği buraya neden başvuru yapamıyor.
        [HttpPost]
        public async Task<IActionResult> SubmitDelete(int Id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == Id);


            if (category == null)
            {
                ViewBag.Message = "Silmek istediğiniz kategori bulunamadı!";


            }

            context.Remove(category);
            context.SaveChangesAsync();

            return RedirectToAction("Index");


        }
    }
}
