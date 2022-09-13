using System.ComponentModel.DataAnnotations;

namespace NotesMinimalApi.Models
{
    public class RegisterationModel
    {

        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
