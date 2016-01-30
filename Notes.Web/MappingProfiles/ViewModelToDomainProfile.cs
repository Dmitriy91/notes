using AutoMapper;
using Notes.Model;
using Notes.Web.ViewModels;

namespace Notes.Web.MappingProfiles
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
            : base("ViewModelToDomain")
        { }

        protected override void Configure()
        {
            Mapper.CreateMap<NoteViewModel, Note>();
        }
    }
}