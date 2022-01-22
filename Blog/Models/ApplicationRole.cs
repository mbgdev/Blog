using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class ApplicationRole
    {
        [Required]
        public int ApplicationRoleId { get; set; }
        [StringLength(128)]
        [Required]
        public string RoleName { get; set;}


        public ICollection<ApplicationUser> User { get; set; }
    }
}
