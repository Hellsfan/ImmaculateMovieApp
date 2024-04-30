using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImmaculateMovieApp.Models.Domain
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [NotMapped]
        public string? MovieNames { get; set; }
    }
}
