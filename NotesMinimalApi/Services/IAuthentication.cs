using NotesMinimalApi.Models;

namespace NotesMinimalApi.Services
{
    public interface IAuthentication
    {
        Task<AuthResult> RegisterAsync(RegisterationModel model);
        Task<AuthResult> LoginAsync(LoginModel model);
        void LogoutAsync();
    }
}
