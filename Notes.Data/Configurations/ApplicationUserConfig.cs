using Notes.Model;
using System.Data.Entity.ModelConfiguration;

namespace Notes.Data.Configurations
{
    public class ApplicationUserConfig : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfig()
        {
            HasMany(u => u.Notes)
           .WithRequired()
           .HasForeignKey(n => n.UserId);
        }
    }
}
