using Microsoft.AspNetCore.Identity;

namespace ImmaculateMovieApp.Models.Domain
{
    public class User : IdentityUser
    {
        public int Id {  get; set; }
        public string Name { get; set; }
    }
}
