using ImmaculateMovieApp.Models.Domain;
using ImmaculateMovieApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace ImmaculateMovieApp.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IFileService fileService;
        private readonly IGenreService genreService;
        private readonly IActorService actorService;
        public MovieController(IMovieService _movieService, 
            IFileService _fileService, IGenreService _genreService, IActorService _actorservice)
        {
            movieService = _movieService;
            fileService = _fileService;
            genreService = _genreService;
            actorService = _actorservice;
        }
        public IActionResult Add()
        {
            var model = new Movie();
            model.GenreList = genreService.List().Select(a => 
            new SelectListItem { Text = a.GenreName, Value = a.Id.ToString() });

            model.ActorList = actorService.List().Select(a => 
            new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Movie model)
        {
            model.GenreList = genreService.List().Select(a => new SelectListItem { 
                Text = a.GenreName, Value = a.Id.ToString() });
            model.GenreList = actorService.List().Select(a => new SelectListItem { 
                Text = a.Name, Value = a.Id.ToString() });

            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileReult = this.fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.MovieImage = imageName;
            }
            var result = movieService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            var model = movieService.GetById(id);
            var selectedGenres = movieService.GetGenreByMovieId(model.Id);
            var selectedActors = movieService.GetActorByMovieId(model.Id);
            MultiSelectList multiGenreList = new MultiSelectList(genreService.List(), "Id", "GenreName", selectedGenres);
            MultiSelectList multiActorList = new MultiSelectList(actorService.List(), "Id", "Name", selectedActors);
            model.MultiGenreList = multiGenreList;
            model.MultiActorList = multiActorList;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Movie model)
        {
            var selectedGenres = movieService.GetGenreByMovieId(model.Id);
            var selectedActors = movieService.GetActorByMovieId(model.Id);
            MultiSelectList multiGenreList = new MultiSelectList(genreService.List(), "Id", "GenreName", selectedGenres);
            MultiSelectList multiActorList = new MultiSelectList(actorService.List(), "Id", "Name", selectedActors);
            model.MultiGenreList = multiGenreList;
            model.MultiActorList = multiActorList;
            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileReult = this.fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.MovieImage = imageName;
            }
            var result = movieService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(MovieList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult MovieList()
        {
            var data = this.movieService.List();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = movieService.Delete(id);
            return RedirectToAction(nameof(MovieList));
        }



    }
}
