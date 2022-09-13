using NotesMinimalApi.Data;
using System.Security.Claims;
using static Microsoft.AspNetCore.Http.Results;
using NotesMinimalApi.Models;
using NotesMinimalApi.Services;

namespace NotesMinimalApi.Handlers
{
    internal static class Notes
    {
        public static async Task<IResult> GetAllNotes(INotesRepositroy notesRepository, IHttpContextAccessor httpContextAccessor)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var notes = await notesRepository.GetUserNotes(userId);
            return Ok(notes);
        }

        public static async Task<IResult> GetNoteById(INotesRepositroy notesRepository, int id, IHttpContextAccessor httpContextAccessor)
        {
            var note = await notesRepository.GetById(id);
            if (note == null) return NotFound();
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (note.UserId != userId ) return Unauthorized();
            return Ok(note);
        }

        public static async Task<IResult> CreateNewNote(INotesRepositroy notesRepository,Note newNote, IHttpContextAccessor httpContextAccessor)
        {
            if(newNote == null) return BadRequest();
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            newNote.UserId = userId;
            newNote.Created = DateTime.Now;
            newNote.Updated = DateTime.Now;
            await notesRepository.Create(newNote);
            return Ok(newNote);
        }

        public static async Task<IResult> UpdateNote(INotesRepositroy notesRepository, int id, Note modifiedNote, IHttpContextAccessor httpContextAccessor)
        {
            if (modifiedNote == null) return BadRequest();
            var note = await notesRepository.GetById(id);
            if (note == null) return BadRequest();
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (note.UserId != userId) return Unauthorized();
            note.Content = modifiedNote.Content;
            note.Title = modifiedNote.Title;
            note.Updated = DateTime.UtcNow;
            await notesRepository.Update(note);
            return Ok();
        }

        public static async Task<IResult> DeleteNote(INotesRepositroy notesRepository, int id, IHttpContextAccessor httpContextAccessor)
        {
            var note = await notesRepository.GetById(id);
            if (note == null) return BadRequest();
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (note.UserId != userId) return Unauthorized();
            await notesRepository.Remove(note);
            return Ok();
        }
    }
}
