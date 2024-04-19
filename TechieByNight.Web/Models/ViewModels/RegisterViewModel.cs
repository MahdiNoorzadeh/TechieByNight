using System.ComponentModel.DataAnnotations;

namespace TechieByNight.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage ="Password Has to be at least 6 characters")]
        public string Password { get; set; }
    }
}
