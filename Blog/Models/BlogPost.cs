using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class BlogPost:BaseEntity
    {

        [StringLength(100)]
        [Required]
        public string SlugUri { get; set; }

        [StringLength(100)]
     
        public string Title { get; set; }
        [StringLength(128)]
        [Required]
        public string Brief { get; set; }

        public string Body { get; set; }

        [StringLength(256)]
        public string PostImage { get; set; }

        public DateTime PublisedDate { get; set; }
        
        public int CategoryId { get; set; }

        public Category Category { get; set; }




    }
}
