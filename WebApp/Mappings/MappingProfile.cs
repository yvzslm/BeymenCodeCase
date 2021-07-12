using AutoMapper;
using Core;
using ConfigurationManagerUI.Models;

namespace ConfigurationManagerUI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ConfigurationModel, ConfigurationViewModel>().ReverseMap();
        }
    }
}
