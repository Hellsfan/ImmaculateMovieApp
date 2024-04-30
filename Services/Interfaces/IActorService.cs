using ImmaculateMovieApp.Models.Domain;
using ImmaculateMovieApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ImmaculateMovieApp.Services.Interfaces
{
    public interface IActorService
    {
        bool Add(Actor model);
        bool Update(Actor model);
        Actor GetById(int id);
        bool Delete(int id);
        IQueryable<Actor> List();
    }
}
