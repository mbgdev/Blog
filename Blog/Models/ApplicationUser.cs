using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class ApplicationUser
    {
        [Required]
        public int ApplicationUserId { get; set; }

        [StringLength(128)]
        [Required]
        public string Email { get; set; }

        [StringLength(128)]
        [Required]
        public string NameSurname { get; set; }

        [StringLength(128)]
        [Required]
        public string Password { get; set; }

        public int ApplicationRoleId { get; set; }

        public ApplicationRole Role { get; set; }


    }
}
