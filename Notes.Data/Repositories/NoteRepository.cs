using Notes.Entities;
using System.Data.Entity;

namespace Notes.Data.Repositories
{
    public class NoteRepository : RepositoryBase<Note>
    {
        public NoteRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }
    }
}
