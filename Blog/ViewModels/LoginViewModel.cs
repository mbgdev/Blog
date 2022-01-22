using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Compare("Password", ErrorMessage ="Şifre alanı aynı değil")]

    }
}
