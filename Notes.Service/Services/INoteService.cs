using Notes.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Service
{
    public interface INoteService
    {
        string UserId { get; set; }
        Note GetNoteById(int noteId);
        IEnumerable<Note> GetAllNotes();
        IEnumerable<Note> GetNotesByName(string name);
        void RemoveNoteById(int noteId);
        void UpdateNote(Note note);
        void AddNote(Note note);
        Task CommitAsync();
        void Commit();
    }
}
