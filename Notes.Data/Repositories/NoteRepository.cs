using Notes.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Repositories
{
    public class NoteRepository : RepositoryBase<Note>
    {
        public NoteRepository(DbContext _dbContext)
            : base(_dbContext)
        { }
    }
}
