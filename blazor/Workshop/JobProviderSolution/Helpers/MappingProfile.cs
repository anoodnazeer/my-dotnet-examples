using AutoMapper;
using JobProviderSolution.Dto;
using JobProviderSolution.Model;

namespace JobProviderSolution.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<JobProvider, JobProviderDto>().ReverseMap();
            CreateMap<Job, JobDto>().ReverseMap();
        }
    }
}
