using ImmaculateMovieApp.Models.Domain;
using ImmaculateMovieApp.Services.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace ImmaculateMovieApp.Services.Implementations
{
    public class ActorService : IActorService
    {
        private readonly DatabaseContext context;
        public ActorService(DatabaseContext _context)
        {
            this.context = _context;
        }
        public bool Add(Actor model)
        {
            try
            {
                context.Actor.Add(model);
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
                context.Actor.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Actor GetById(int id)
        {
            return context.Actor.Find(id);
        }

        public IQueryable<Actor> List()
        {

            var data = context.Actor.ToList();
            foreach (var actor in data)
            {
                var movies = (from movie in context.Movie
                              join m in context.MovieActor
                              on movie.Id equals m.MovieId
                              where m.MovieId == movie.Id && m.ActorId==actor.Id
                              select movie.Title
                              ).ToList();
                var movieNames = string.Join(',', movies);
                actor.MovieNames = movieNames;
            }
            return data.AsQueryable();
        }

        public bool Update(Actor model)
        {
            try
            {
                context.Actor.Update(model);
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
