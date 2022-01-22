using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Category:BaseEntity
    {
        [StringLength(64)]
        [Required]
        public string Name { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }
       
    }
}
