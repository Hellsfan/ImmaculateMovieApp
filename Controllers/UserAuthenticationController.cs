using ImmaculateMovieApp.Models.ViewModels;
using ImmaculateMovieApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImmaculateMovieApp.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationService authService;
        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this.authService = authService;
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel input)
        {
            var model = new RegistrationModel
            {
                Email = "sample@email.sample",
                Username = input.Username,
                Name = input.Name,
                Password = input.Password,
                PasswordConfirm = input.PasswordConfirm,
                Role = "Admin"
            };

            var result = await authService.RegisterAsync(model);
            return Ok(result.Message);
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(CredentialsModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await authService.LoginAsync(model);
            if (result.StatusCode == 1)
                return RedirectToAction("Index", "Home");
            else
            {
                TempData["msg"] = "Login failed!";
                return RedirectToAction(nameof(Login));
            }
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

    }
}
