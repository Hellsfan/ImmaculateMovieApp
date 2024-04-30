using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImmaculateMovieApp.Models.Domain
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? ReleaseYear { get; set; }

        public string? MovieImage { get; set; }

        [Required]
        public string? Director { get; set; }
        [Required]
        public List<int>? Actors { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [NotMapped]
        [Required]
        public List<int>? Genres { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? GenreList { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? ActorList { get; set; }
        [NotMapped]
        public string? GenreNames { get; set; }
        [NotMapped]
        public string? ActorNames { get; set; }

        [NotMapped]
        public MultiSelectList? MultiGenreList { get; set; }
        [NotMapped]
        public MultiSelectList? MultiActorList { get; set; }
    }
}
