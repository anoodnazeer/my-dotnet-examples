
using AutoMapper;
using Domain.Models;
using Domain.Service.JobProvider.Dto;
using Domain.Service.Jobs.Dto;
using Domain.Service.Login.DTO;
using Job_Portal.API.JobProvider.RequestObjects;
using Job_Portal.API.Jobs.RequestObjects;
using JobSeekerModel = Domain.Models.JobSeeker;

namespace Job_Portal.Helper
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            // JobSeeker → JobSeekerDto
            CreateMap<JobSeekerModel, JobSeekerDto>().ReverseMap();
            CreateMap<JobProviderCompany, AuthUser>().ReverseMap();
            CreateMap<JobProviderLoginDto, AuthUser>().ReverseMap();
            CreateMap<AuthUser, JobProviderLoginDto>()
           .ForMember(dest => dest.JobProviderId, opt => opt.MapFrom(src => src.JobProviderId));


            // JobPost → JobPostDto
            CreateMap<JobPost, JobPostDto>()
            .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location != null ? src.Location.Name : string.Empty))
            .ForMember(dest => dest.IndustryName, opt => opt.MapFrom(src => src.Industry != null ? src.Industry.Name : string.Empty))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company != null ? src.Company.LegalName : string.Empty))
            .ReverseMap()
            .ForMember(dest => dest.Location, opt => opt.Ignore())
            .ForMember(dest => dest.Industry, opt => opt.Ignore())
            .ForMember(dest => dest.Company, opt => opt.Ignore())
            .ForMember(dest => dest.PostedByNavigation, opt => opt.Ignore());

            // CreateJobPostRequest → JobPostDto
            CreateMap<CreateJobPostRequest, JobPostDto>().ReverseMap();

            // UpdateJobPostRequest → JobPostDto
            CreateMap<UpdateJobPostRequest, JobPostDto>()
                .ForMember(dest => dest.Salary, opt => opt.Ignore())
                .ForMember(dest => dest.Experience, opt => opt.Ignore())
                .ForMember(dest => dest.JobType, opt => opt.Ignore())
                .ReverseMap();


            // ScheduleInterviewRequest → InterviewDto
            CreateMap<ScheduleInterviewRequest, InterviewDto>()
                .ForMember(dest => dest.DateScheduled, opt => opt.MapFrom(src => src.ScheduledDateTime))
                .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.JobId))
                .ForMember(dest => dest.JobSeekerId, opt => opt.MapFrom(src => src.JobSeekerId))
                .ForMember(dest => dest.Mode, opt => opt.MapFrom(src => src.Mode))
                // Ignore other properties not in request
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.JobTitle, opt => opt.Ignore())
                .ForMember(dest => dest.JobSeekerName, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.CompanyId, opt => opt.Ignore());

            // InterviewDto → Interview (database entity)
            CreateMap<InterviewDto, Interview>()
                .ForMember(dest => dest.interviewee, opt => opt.MapFrom(src => src.JobSeekerId))
                .ForMember(dest => dest.Job, opt => opt.Ignore())
                .ForMember(dest => dest.Jobseeker, opt => opt.Ignore())
                .ForMember(dest => dest.Company, opt => opt.Ignore());

            // Interview → InterviewDto
            CreateMap<Interview, InterviewDto>()
                .ForMember(dest => dest.JobTitle,
                    opt => opt.MapFrom(src => src.Job != null ? src.Job.JobTitle : null))
                .ForMember(dest => dest.JobSeekerId, opt => opt.MapFrom(src => src.interviewee))
                .ForMember(dest => dest.JobSeekerName,
                    opt => opt.MapFrom(src => src.Jobseeker != null
                        ? src.Jobseeker.FirstName + " " + (src.Jobseeker.LastName ?? "")
                        : null))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId));

            // UpdateInterviewRequest → InterviewDto
            CreateMap<UpdateInterviewRequest, InterviewDto>()
                .ForMember(dest => dest.DateScheduled,
                    opt => opt.MapFrom(src => CombineDateAndTime(src.Date, src.Time)))
                .ForMember(dest => dest.Mode, opt => opt.MapFrom(src => src.Mode))
                // Ignore all other members
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.JobId, opt => opt.Ignore())
                .ForMember(dest => dest.JobSeekerId, opt => opt.Ignore())
                .ForMember(dest => dest.JobTitle, opt => opt.Ignore())
                .ForMember(dest => dest.JobSeekerName, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.CompanyId, opt => opt.Ignore());

            // JobApplication → ApplicantDto
            CreateMap<JobApplication, ApplicantDto>()
      .ForMember(dest => dest.ApplicationId, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.JobPostId))
      .ForMember(dest => dest.ApplicantId, opt => opt.MapFrom(src => src.ApplicantId))
      .ForMember(dest => dest.ApplicantName,
          opt => opt.MapFrom(src => src.Seeker != null ? src.Seeker.FirstName + " " + (src.Seeker.LastName ?? "") : string.Empty))
      .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobPost != null ? src.JobPost.JobTitle : string.Empty))
      .ForMember(dest => dest.DateApplied, opt => opt.MapFrom(src => src.Datesubmitted))
      .ForMember(dest => dest.CoverLetter, opt => opt.MapFrom(src => src.CoverLetter));
        }

        // Helper method to combine date + time string
        private static DateTime CombineDateAndTime(DateTime date, string time)
        {
            if (DateTime.TryParse(time, out DateTime parsedTime))
            {
                return new DateTime(
                    date.Year,
                    date.Month,
                    date.Day,
                    parsedTime.Hour,
                    parsedTime.Minute,
                    parsedTime.Second
                );
            }

            return date; // fallback if time parsing fails
        }
    
    
    }
}




    

