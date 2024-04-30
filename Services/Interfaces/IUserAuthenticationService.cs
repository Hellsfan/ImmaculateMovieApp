using ImmaculateMovieApp.Models.ViewModels;

namespace ImmaculateMovieApp.Services.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(CredentialsModel model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegistrationModel model);
    }
}
