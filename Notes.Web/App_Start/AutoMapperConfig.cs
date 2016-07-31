using AutoMapper;
using Notes.Web.Infrastructure.Mappings;

namespace Notes.Web
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappingProfiles()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DomainToViewModelProfile>();
                cfg.AddProfile<ViewModelToDomainProfile>();
            });
        }
    }
}