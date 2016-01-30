using AutoMapper;
using Notes.Web.MappingProfiles;

namespace Notes
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

            Mapper.AssertConfigurationIsValid<ViewModelToDomainProfile>();
        }
    }
}