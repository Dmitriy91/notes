using AutoMapper;
using Notes.Entities;
using Notes.Web.ViewModels;

namespace Notes.Web.Infrastructure.Mappings
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
            : base("DomainToViewModel")
        {
        }

        protected override void Configure()
        {
            CreateMap<Note, NoteViewModel>()
                .ForSourceMember(s => s.UserId, opts => opts.Ignore());
        }
    }
}
