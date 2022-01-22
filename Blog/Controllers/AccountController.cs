using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {
        private readonly BlogDbContext context;
        private readonly ILogger<AccountController> logger;
        public AccountController(BlogDbContext _context, ILogger<AccountController> _logger)
        {
            context = _context;
            logger = _logger;
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await context.ApplicationUsers
                .Include(u => u.Role)
            
                .FirstOrDefaultAsync(u => u.Email == model.Username && u.Password == model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Başarısız oturum açma işlmeş !");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.ApplicationUserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,user.Role.RoleName),
                new Claim(ClaimTypes.Name,user.NameSurname),
               
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                // AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5),

            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // logger.LogInformation("User {NameSurname} oturum açtı {Time}",
            //    user.NameSurname,
            //  DateTime.UtcNow);



            return RedirectToAction("Index", "Blog");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Blog"); ;
        }




        [Route("/{id}")]
        public async Task<IActionResult> ProfilEdit(int id)
        {
            var data = await context.ApplicationUsers.FindAsync(id);

            if (data == null)
            {
                return RedirectToAction("index");
            }

         

            var model = new ProfilEditViewModel
            {
                NameSurname = data.NameSurname,
                Email = data.Email,
                Password = data.Password,
            };
           

            return View(model);
        }

        [HttpPost]
        [Route("/{id}")]
        [Authorize]
       
        public async Task<IActionResult> ProfilEdit( ProfilEditViewModel model,int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var data = await context.ApplicationUsers.FindAsync(id);

            data.NameSurname = model.NameSurname;
            data.Email = model.Email;
            data.Password = model.Password;

            context.ApplicationUsers.Update(data);
            await context.SaveChangesAsync();

            await HttpContext.RefreshLoginAsync();
            return RedirectToAction("Index", "Blog");
        }



    }
}
