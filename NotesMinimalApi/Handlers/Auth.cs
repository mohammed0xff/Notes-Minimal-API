using NotesMinimalApi.Models;
using NotesMinimalApi.Services;

namespace NotesMinimalApi.Handlers
{
    internal static class Auth
    {
        public static async Task<AuthResult> Login( LoginModel model, IAuthentication auth)
        {
            return await 
                auth.LoginAsync(model);
        }
        public static async Task<AuthResult> Register(RegisterationModel model, IAuthentication auth)
        {
            return await
                auth.RegisterAsync(model);
        }
        public static async void Logout(IAuthentication auth)
        {
            auth.LogoutAsync();
        }
    }
}
