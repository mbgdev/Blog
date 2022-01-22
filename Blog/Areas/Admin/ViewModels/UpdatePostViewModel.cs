using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.ViewModels
{
    public class UpdatePostViewModel
    {
       
        [StringLength(128, MinimumLength = 10, ErrorMessage = "{0} alanı {2} - {1} karakter aralığında olmalı.")]
        [Required]
        [Display(Name = "Blog Başlığı")]
        public string Title { get; set; }

        [StringLength(128)]
        [Required]
        public string SlugUri { get; set; }

        [StringLength(300)]
        [Required]
        public string Brief { get; set; }
        public string Body { get; set; }
        [DataType(DataType.Date)]
        public DateTime PublisedDate { get; set; }
        [Required]
        public IFormFile PostImage { get; set; }
        public string? PostImageUri { get; set; }

        public int CategoryId { get; set; }
        public List<SelectListItem>? Categories { get; set; }
    }
}
