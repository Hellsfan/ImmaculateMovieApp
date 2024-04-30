using ImmaculateMovieApp.Models.Domain;

namespace ImmaculateMovieApp.Services.Interfaces
{
    public interface IGenreService
    {
        bool Add(Genre model);
        bool Update(Genre model);
        Genre GetById(int id);
        bool Delete(int id);
        IQueryable<Genre> List();
    }
}
