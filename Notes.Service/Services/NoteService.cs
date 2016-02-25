using Notes.Data.Repositories;
using Notes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Notes.Service
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> _noteRepository;

        public NoteService(IRepository<Note> noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public string UserId { get; set; }

        public Note GetNoteById(int noteId)
        {
            return _noteRepository.Get(n => n.Id == noteId && n.UserId == UserId);
        }

        public IEnumerable<Note> GetAllNotes()
        {
            return _noteRepository.GetMany(n => n.UserId == UserId).ToList();
        }

        public IEnumerable<Note> GetFilteredNotes(string name, string text, DateTime? date, int page, int notesPerPage, out int notesFound)
        {
            IQueryable<Note> notes = null;
            Expression<Func<Note, bool>> condition = null;

            if (String.IsNullOrWhiteSpace(name) && String.IsNullOrWhiteSpace(text) && date == null)
            {
                condition = n => n.UserId == UserId;
            }
            else if (!String.IsNullOrWhiteSpace(name) && String.IsNullOrWhiteSpace(text) && date == null)
            {
                condition = n => n.UserId == UserId && n.Name.Contains(name);
            }
            else if (!String.IsNullOrWhiteSpace(name) && !String.IsNullOrWhiteSpace(text) && date == null)
            {
                condition = n => n.UserId == UserId && n.Name.Contains(name) && n.Text.Contains(text);
            }
            else if (!String.IsNullOrWhiteSpace(name) && !String.IsNullOrWhiteSpace(text) && date != null)
            {
                condition = n => n.UserId == UserId && n.Name.Contains(name) && n.Text.Contains(text) && n.Date == date.Value;
            }
            else if (String.IsNullOrWhiteSpace(name) && !String.IsNullOrWhiteSpace(text) && date != null)
            {
                condition = n => n.UserId == UserId && n.Text.Contains(text) && n.Date == date.Value;
            }
            else if (String.IsNullOrWhiteSpace(name) && String.IsNullOrWhiteSpace(text) && date != null)
            {
                condition = n => n.UserId == UserId && n.Date == date.Value;
            }
            else if (!String.IsNullOrWhiteSpace(name) && String.IsNullOrWhiteSpace(text) && date == null)
            {
                condition = n => n.UserId == UserId && n.Name.Contains(name);
            }
            else
            {
                condition = n => n.UserId == UserId && n.Text.Contains(text);
            }

            notes = _noteRepository.GetMany(condition);
            notesFound = notes.Select(n => n.Id).Count();// "Select" is used to reduce time consumed.

            return notes.OrderBy(note => note.Id).
                Skip((page - 1) * notesPerPage).
                Take(notesPerPage).
                ToList();
        }

        public void RemoveNoteById(int noteId)
        {
            bool noteExists = _noteRepository.Exists(n => n.Id == noteId && n.UserId == UserId);

            if (noteExists)
                _noteRepository.Delete(new Note { Id = noteId });
        }

        public void UpdateNote(Note note)
        {
            bool noteExists = _noteRepository.Exists(n => n.Id == note.Id && n.UserId == UserId);

            if (noteExists)
            {
                note.UserId = UserId;
                _noteRepository.Update(note);
            }
        }

        public void AddNote(Note note)
        {
            note.UserId = UserId;
            _noteRepository.Add(note);
        }

        async public Task CommitAsync()
        {
            await _noteRepository.CommitAsync();
        }

        public void Commit()
        {
            _noteRepository.Commit();
        }
    }
}
