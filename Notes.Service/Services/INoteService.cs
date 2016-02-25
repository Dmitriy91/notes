using Notes.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Service
{
    public interface INoteService
    {
        string UserId { get; set; }
        Note GetNoteById(int noteId);
        IEnumerable<Note> GetAllNotes();
        IEnumerable<Note> GetFilteredNotes(string name, string text, DateTime? date, int page, int notesPerPage, out int notesFound);
        void RemoveNoteById(int noteId);
        void UpdateNote(Note note);
        void AddNote(Note note);
        Task CommitAsync();
        void Commit();
    }
}
