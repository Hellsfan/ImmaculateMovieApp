using ImmaculateMovieApp.Models.Domain;
using ImmaculateMovieApp.Models.ViewModels;
using ImmaculateMovieApp.Services.Interfaces;

namespace ImmaculateMovieApp.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly DatabaseContext context;
        public MovieService(DatabaseContext _context)
        {
            context = _context;
        }
        public bool Add(Movie model)
        {
            try
            {

                context.Movie.Add(model);
                context.SaveChanges();
                foreach (int genreId in model.Genres)
                {
                    var movieGenre = new MovieGenre
                    {
                        MovieId = model.Id,
                        GenreId = genreId
                    };
                    context.MovieGenre.Add(movieGenre);
                }

                foreach (int actorId in model.Actors)
                {
                    var movieActor = new MovieActor
                    {
                        MovieId = model.Id,
                        ActorId = actorId
                    };
                    context.MovieActor.Add(movieActor);
                }
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
                var movieGenres = context.MovieGenre.Where(a => a.MovieId == data.Id);
                var movieActors = context.MovieActor.Where(a => a.MovieId == data.Id);
                foreach (var movieGenre in movieGenres)
                {
                    context.MovieGenre.Remove(movieGenre);
                }
                foreach (var movieActor in movieActors)
                {
                    context.MovieActor.Remove(movieActor);
                }
                context.Movie.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Movie GetById(int id)
        {
            return context.Movie.Find(id);
        }

        public MovieListVM List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new MovieListVM();

            var list = context.Movie.ToList();

            foreach (var movie in list)
            {
                var genres = (from genre in context.Genre
                              join mg in context.MovieGenre
                              on genre.Id equals mg.GenreId
                              where mg.MovieId == movie.Id
                              select genre.GenreName
                              ).ToList();
                var genreNames = string.Join(',', genres);
                movie.GenreNames = genreNames;

                var actors = (from actor in context.Actor
                              join ma in context.MovieActor
                              on actor.Id equals ma.ActorId
                              where ma.MovieId == movie.Id
                              select actor.Name
                              ).ToList();
                var actorNames = string.Join(',', actors);
                movie.ActorNames = actorNames;
            }

            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list
                    .Where(a => a.Title.ToLower().Contains(term)
                    || a.ActorNames.ToLower().Contains(term)
                    || a.GenreNames.ToLower().Contains(term)
                    || a.ReleaseYear.ToLower().Contains(term)
                    ||a.Director.ToLower().Contains(term))
                    .ToList();
            }

            if (paging)
            {
                // here we will apply paging
                int pageSize = 5;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }


            data.MovieList = list.AsQueryable();
            return data;
        }

        public bool Update(Movie model)
        {
            try
            {
                var genresToBeDeleted = context.MovieGenre.Where(a => a.MovieId == model.Id && !model.Genres.Contains(a.GenreId)).ToList();
                var actorsToBeDeleted = context.MovieActor.Where(a => a.MovieId == model.Id && !model.Actors.Contains(a.Id)).ToList();
                foreach (var mGenre in genresToBeDeleted)
                {
                    context.MovieGenre.Remove(mGenre);
                }
                foreach (var mActor in actorsToBeDeleted)
                {
                    context.MovieActor.Remove(mActor);
                }

                foreach (int genId in model.Genres)
                {
                    var movieGenre = context.MovieGenre.FirstOrDefault(a => a.MovieId == model.Id && a.GenreId == genId);
                    if (movieGenre == null)
                    {
                        movieGenre = new MovieGenre { GenreId = genId, MovieId = model.Id };
                        context.MovieGenre.Add(movieGenre);
                    }
                }

                foreach (int actorId in model.Actors)
                {
                    var movieActor = context.MovieActor.FirstOrDefault(a => a.MovieId == model.Id && a.Id == actorId);
                    if (movieActor == null)
                    {
                        movieActor = new MovieActor { ActorId = actorId, MovieId = model.Id };
                        context.MovieActor.Add(movieActor);
                    }
                }

                context.Movie.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<int> GetGenreByMovieId(int movieId)
        {
            var genreIds = context.MovieGenre.Where(a => a.MovieId == movieId).Select(a => a.GenreId).ToList();
            return genreIds;
        }

        public List<int> GetActorByMovieId(int movieId)
        {
            var genreIds = context.MovieActor.Where(a => a.MovieId == movieId).Select(a => a.MovieId).ToList();
            return genreIds;
        }
    }
}
