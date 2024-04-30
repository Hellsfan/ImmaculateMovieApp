using ImmaculateMovieApp.Models.Domain;
using ImmaculateMovieApp.Services.Interfaces;

namespace ImmaculateMovieApp.Services.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly DatabaseContext context;
        public GenreService(DatabaseContext _context)
        {
            this.context = _context;
        }
        public bool Add(Genre model)
        {
            try
            {
                context.Genre.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                context.Genre.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Genre GetById(int id)
        {
            return context.Genre.Find(id);
        }

        public IQueryable<Genre> List()
        {
            var data = context.Genre.AsQueryable();
            return data;
        }

        public bool Update(Genre model)
        {
            try
            {
                context.Genre.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
