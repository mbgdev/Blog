using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace Blog.Controllers
{

    public class BlogController : Controller
    {
        private readonly BlogDbContext context;

        public BlogController(BlogDbContext _context)
        {
           context = _context;
        }


        public async Task<IActionResult> Index()
        {

            ViewBag.Title = "Blog Ana Sayfa";
            var model =  await (from q in context.Blogs
                         orderby q.PublisedDate descending
                         select q).ToListAsync();
           

            return View(model);
        }


        [Route("~/detay/{sluguri}")]
        public async Task<IActionResult> Detail(string sluguri)
        {

            ViewBag.Title = "detay";
            var model = await context.Blogs.FirstOrDefaultAsync(b => b.SlugUri.Equals(sluguri));
          
     
            return View(model);
        }
      
       [Route("~/ara/")]
        public async Task<IActionResult> Search(string keyword)
        {
         

            if(String.IsNullOrEmpty(keyword))
            {
                return View("Index");
            }

            ViewBag.Title = "Arama Sonuçları";

            var model=from q in context.Blogs   
                      select q;
        
             model = context.Blogs.Where(b => b.Body.ToLower().Contains(keyword.ToLower()));

        

            return View("Index",await model.ToListAsync());
        }




        [Route("~/kategori/{catName}")]
        public async Task<IActionResult> GetByCategoryId(string catName)
        {
            var model =await context.Blogs.Where(b => b.Category.Name.ToLower() ==catName.ToLower()).ToListAsync();

            if (model == null)
            {
                return View("Index");
            }

            return View("Index",model);
        }



  




    }
}