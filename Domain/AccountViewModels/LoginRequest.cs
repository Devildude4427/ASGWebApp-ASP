using System.ComponentModel.DataAnnotations;

namespace Domain.AccountViewModels
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}