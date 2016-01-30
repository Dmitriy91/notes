using AutoMapper;
using Notes.Model;
using Notes.Web.ViewModels;

namespace Notes.Web.MappingProfiles
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
            : base("DomainToViewModel")
        { }

        protected override void Configure()
        {
            CreateMap<Note, NoteViewModel>().ForSourceMember(s => s.UserId, opts => opts.Ignore());
        }
    }
}