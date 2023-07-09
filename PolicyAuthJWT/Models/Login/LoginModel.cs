using System.ComponentModel.DataAnnotations;

namespace PolicyAuthJWT.Models.Login
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public Boolean KeepLoggedIn { get; set; } = false;
    }
}
