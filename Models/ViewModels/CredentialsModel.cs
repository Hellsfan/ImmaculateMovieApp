using System.ComponentModel.DataAnnotations;

namespace ImmaculateMovieApp.Models.ViewModels
{
    public class CredentialsModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
