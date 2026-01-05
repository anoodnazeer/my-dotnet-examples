using AutoMapper;
using JobPortalMVC.Dto;
using JobPortalMVC.Models;

namespace JobPortalMVC.Helper
{
    public class MappingProfile : Profile
    {
        
        public MappingProfile ()
        {

            CreateMap<JobDto, Job>().ReverseMap();

            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<ApplicationDto, Application>().ReverseMap();
        }

         
    }
}
