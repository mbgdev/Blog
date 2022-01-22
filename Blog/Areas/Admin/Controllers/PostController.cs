using Blog.Areas.Admin.ViewModels;
using Blog.Data;
using Blog.Models;
using Blog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PostController : Controller
    {
        private readonly BlogDbContext context;
        private readonly IFileUploadService FileUploadService;
        public PostController(BlogDbContext _context, IFileUploadService _uploadService)
        {
            context = _context;
            FileUploadService = _uploadService;

        }
        public async Task<IActionResult> Index()
        {

            var model = await (from b in context.Blogs
                               select new PostIndexViewModel
                               {
                                   Id = b.Id,
                                   CategoryName = b.Category.Name,
                                   Title = b.Title,
                                   PublisedTime = b.PublisedDate,

                               })
                              .ToListAsync();


            return View(model);
        }


        public async Task<IActionResult> Create()
        {
            var model = new CreatePostViewModel();
            model.PublisedDate = DateTime.Now;// DateTime.UtcNow.AddHours(3);
            model.Categories = await GetCategorySelectListData();
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Lütfen girdiğiniz değerleri gözden geçirip tekrar deneyin.");
                return View(model);
            }

            var imageUri = await FileUploadService.Upload(model.PostImage);

            if (string.IsNullOrEmpty(imageUri))
            {
                ModelState.AddModelError("", "Blog resmi yüklenirken hata meydana geldi. Lütfen tekrar deneyin.");
                return View(model);
            }

            BlogPost post = new BlogPost
            {
                Body = model.Body,
                Brief = model.Brief,
                CategoryId = model.CategoryId,
                PostImage = imageUri,
                PublisedDate = model.PublisedDate,
                SlugUri = model.SlugUri,
                Title = model.Title,


            };
            context.Blogs.AddAsync(post);
            context.SaveChangesAsync();

            return RedirectToAction("Index");
        }




        public async Task<IActionResult> Edit(int id)
        {
            var data = await context.Blogs.FindAsync(id);

            if (data == null)
            {
                return RedirectToAction("index");
            }

            var model = new UpdatePostViewModel
            {
                Body = data.Body,
                Brief = data.Brief,
                CategoryId = data.CategoryId,
                PostImageUri = data.PostImage,
                SlugUri = data.SlugUri,
                Title = data.Title,
                PublisedDate = data.PublisedDate,
            };

            model.Categories = await GetCategorySelectListData(data.CategoryId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePostViewModel model, int Id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string newImagePath = null;
            if (model.PostImage != null && model.PostImage.Length > 0)
            {
                FileUploadService.Delete(model.PostImageUri);
                newImagePath = await FileUploadService.Upload(model.PostImage);
            }
            var data = await context.Blogs.FindAsync(Id);
            data.Body = model.Body;
            data.Brief = model.Brief;
            data.CategoryId = model.CategoryId;
            data.SlugUri = model.SlugUri;
            data.Title = model.Title;
            data.PublisedDate = model.PublisedDate;
            if (!String.IsNullOrEmpty(newImagePath))
                data.PostImage = newImagePath;

            context.Blogs.Update(data);
            await context.SaveChangesAsync();
            return RedirectToAction("index");

        }

        private async Task<List<SelectListItem>> GetCategorySelectListData(int selectedCategoryId = 0)
        {
            return await (from c in context.Categories
                          select new SelectListItem
                          {
                              Text = c.Name,
                              Value = c.Id.ToString(),
                              Selected = c.Id == selectedCategoryId
                          }).ToListAsync();
        }


        public async Task<IActionResult> Delete(int id)
        {
            var model = await context.Blogs.FirstOrDefaultAsync(c => c.Id == id);

            if (model == null)
            {
                ViewBag.Message = "Silmek istediğiniz post bulunamadı!";
            }
            return View(model);
        }


        //Todo: Ödev httppost isteği buraya neden başvuru yapamıyor.
        [HttpPost]
        public async Task<IActionResult> SubmitDelete(int Id)
        {
            var model = await context.Blogs.FirstOrDefaultAsync(c => c.Id == Id);


            if (model == null)
            {
                ViewBag.Message = "Silmek istediğiniz post bulunamadı!";


            }

            context.Remove(model);
            context.SaveChangesAsync();

            return RedirectToAction("Index");


        }


    }



}




