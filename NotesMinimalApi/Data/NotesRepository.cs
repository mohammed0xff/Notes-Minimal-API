using Microsoft.EntityFrameworkCore;
using NotesMinimalApi.Models;

namespace NotesMinimalApi.Data
{
    public class NotesRepository : INotesRepositroy
    {

        private readonly AppDbContext _context;

        public NotesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> GetAll()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<Note?> GetById(int id)
        {
            return await _context.Notes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Note>> GetUserNotes(string userId)
        {
            return await _context.Notes.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task Create(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }

            await _context.AddAsync(note);
            await SaveChanges();

        }
        public async Task Update(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }
            _context.Update(note);
            await SaveChanges();

        }

        public async Task Remove(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }
            _context.Remove(note);
            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}
