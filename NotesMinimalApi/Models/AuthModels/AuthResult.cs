namespace NotesMinimalApi.Models
{
    public class AuthResult
    {
        public bool Success { get; set; } = false;
        public List<string> Errors { get; set; } = new List<string>();
    }
}
