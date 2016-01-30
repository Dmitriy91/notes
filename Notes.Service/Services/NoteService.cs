using Notes.Data.Repositories;
using Notes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IEnumerable<Note> GetNotesByName(string name)
        {
            return _noteRepository.GetMany(n => n.Name.Contains(name) && n.UserId == UserId).ToList();
        }

        public void RemoveNoteById(int noteId)
        {
            bool noteExists = _noteRepository.Exists(n => n.UserId == UserId && n.Id == noteId);

            if (noteExists)
                _noteRepository.Delete(new Note { Id = noteId });
        }

        public void UpdateNote(Note note)
        {
            bool noteExists = _noteRepository.Exists(n => n.UserId == UserId && n.Id == note.Id);

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
