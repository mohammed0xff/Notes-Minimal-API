using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotesMinimalApi.Models;
using System.Security.Claims;

namespace NotesMinimalApi.Services
{
    public class Authentication : IAuthentication
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public Authentication(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthResult> LoginAsync(LoginModel model)
        {
            var authResult = new AuthResult();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null){
                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
                if (passwordCheck){
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded){
                        authResult.Success = true;
                    }else{
                        authResult.Errors.Add(result.ToString());
                    }
                }
                else{
                    authResult.Errors.Add("Wrong password.");
                }
            }
            else{
                authResult.Errors.Add("User Doesn't Exist!");            
            }
            return authResult;
        }

        [HttpPost]
        public async Task<AuthResult> RegisterAsync(RegisterationModel model)
        {
            var authResult = new AuthResult();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                authResult.Errors.Add( "Email already registered!");
                return authResult;
            }
            var newUser = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email,
                AccessFailedCount = 2,
               
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, model.Password);
            if (newUserResponse.Succeeded)
            {
                authResult.Success = true;
                return authResult;
            }
            foreach(var error in newUserResponse.Errors)
                authResult.Errors.Append(error.ToString());
            return authResult;
        }

        [HttpPost]
        public async void LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
