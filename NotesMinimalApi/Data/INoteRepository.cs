using NotesMinimalApi.Models;

namespace NotesMinimalApi.Data
{
    public interface INotesRepositroy
    {
        Task<IEnumerable<Note>> GetAll();
        Task<IEnumerable<Note>> GetUserNotes(string userId);
        Task<Note?> GetById(int id);
        Task Create(Note note);
        Task Update(Note note);
        Task Remove(Note note);
        Task SaveChanges();
    }
}
