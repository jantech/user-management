using System.ComponentModel.DataAnnotations;

namespace UserManagement.Core.ViewModels
{
    public class AuthenticateViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
