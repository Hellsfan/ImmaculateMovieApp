using ImmaculateMovieApp.Models.Domain;
using ImmaculateMovieApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImmaculateMovieApp.Controllers
{
    [Authorize]
    public class ActorController : Controller
    {
        private readonly IActorService directorService;
        public ActorController(IActorService _IDirectorService)
        {
            directorService = _IDirectorService;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Actor model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = directorService.Add(model);
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
            var data = directorService.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Actor model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = directorService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(ActorList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult ActorList()
        {
            var data = this.directorService.List().ToList();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = directorService.Delete(id);
            return RedirectToAction(nameof(ActorList));
        }



    }
}
