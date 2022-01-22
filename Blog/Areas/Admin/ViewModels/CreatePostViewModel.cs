using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Blog.Areas.Admin.ViewModels
{
    public class CreatePostViewModel
    {
        [StringLength(128, MinimumLength = 10, ErrorMessage = "{0} alanı {2} - {1} karakter aralığında olmalı.")]
        [Required(ErrorMessage ="{0} boş bırakılamaz")]
        [Display(Name = "Blog Başlığı")]
        
        public string Title { get; set; }

        [StringLength(128)]
        [Required]
        public string SlugUri { get; set; }

        [StringLength(128)]
        [Required]
        public string Brief { get; set; }

        public string Body { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublisedDate { get; set; }

        [Required]
        public IFormFile PostImage { get; set; }

        public int CategoryId { get; set; }

        public List<SelectListItem>? Categories { get; set; }
    }
}
