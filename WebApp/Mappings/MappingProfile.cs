using AutoMapper;
using Core;
using WebApp.Models;

namespace WebApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ConfigurationModel, ConfigurationViewModel>().ReverseMap();
        }
    }
}
