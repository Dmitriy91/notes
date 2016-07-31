using AutoMapper;
using Notes.Entities;
using Notes.Web.ViewModels;

namespace Notes.Web.Infrastructure.Mappings
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
            : base("ViewModelToDomain")
        {
        }

        protected override void Configure()
        {
            CreateMap<NoteViewModel, Note>();
        }
    }
}
