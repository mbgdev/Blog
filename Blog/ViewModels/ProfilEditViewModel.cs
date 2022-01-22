using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class ProfilEditViewModel
    {
        [StringLength(128)]
        [Required]
        public string Email { get; set; }

        [StringLength(128)]
        [Required]
        public string NameSurname { get; set; }

        [StringLength(128)]
        [Required]
        public string Password { get; set; }

    }
}
