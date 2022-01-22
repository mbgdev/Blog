using System.ComponentModel.DataAnnotations;

namespace Blog.Areas.Admin.ViewModels
{
    public class CreateCategoryViewModel
    {


        [StringLength(64)]
        [Required]
        public string Name { get; set; }
    }
}
