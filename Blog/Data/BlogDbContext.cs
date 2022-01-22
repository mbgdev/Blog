using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class BlogDbContext : DbContext
    {//nerede türetilirse orada ver
        public BlogDbContext(DbContextOptions options) : base(options)
        {

        }



        public DbSet<BlogPost> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

    }
}
