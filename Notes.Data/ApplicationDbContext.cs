using Microsoft.AspNet.Identity.EntityFramework;
using Notes.Model;
using System.Data.Entity;

namespace Notes.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        { }
        public DbSet<Note> Notes { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
