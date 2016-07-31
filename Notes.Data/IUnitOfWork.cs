using System;
using System.Threading.Tasks;

namespace Notes.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        Task CommitAsync();
    }
}
