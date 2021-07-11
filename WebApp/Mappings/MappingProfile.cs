using AutoMapper;
using Core;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ConfigurationModel, ConfigurationViewModel>().ReverseMap();
            CreateMap<IEnumerable<ConfigurationModel>, IEnumerable<ConfigurationViewModel>>().ReverseMap();
        }
    }
}
