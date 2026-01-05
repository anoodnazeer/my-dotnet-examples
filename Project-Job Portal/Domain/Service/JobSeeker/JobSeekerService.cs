 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Enums;
using Azure.Core;
using Domain.Mail;
using Domain.Models;
using Domain.Service.Authuser.Interfaces;
using Domain.Service.JobProvider;
using Domain.Service.JobSeeker.DTOs;
using Domain.Service.JobSeeker.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper.Internal;
using Domain.Service.JobProvider.Interfaces;
using Domain.Service.Jobs.Interfaces;
using Domain.Service.Jobs.Dto;
namespace Domain.Service.JobSeeker
{
    public class JobSeekerService : IJobSeekerService
    {
        IJobSeekerRepository jobSeekerRepository;
        IJobService jobService;
        IAuthUserRepository authUserRepository;
        IMapper mapper;
        IEmailService emailService;
        private readonly HireMeNowDbContext _context;
        public JobSeekerService(IJobSeekerRepository _jobSeekerRepository, IMapper _mapper, IEmailService _emailService, IAuthUserRepository _authUserRepository, HireMeNowDbContext context, IJobService _jobService)
        {
            jobSeekerRepository = _jobSeekerRepository;
            mapper = _mapper;
            emailService = _emailService;
            authUserRepository = _authUserRepository;
            jobService = _jobService;
            _context = context;
        }

        public async void CreateSignupRequest(JobSeekerSignupRequestDto data)
        {
            var signUpRequest = mapper.Map<SignUpRequest>(data);
            var signUpId = jobSeekerRepository.AddSignupRequest(signUpRequest);
            MailRequest mailRequest = new MailRequest();

            mailRequest.Subject = "HireMeNow SignUp Verification";
            mailRequest.Body = signUpId.ToString();
            mailRequest.ToEmail = signUpRequest.Email;
            await emailService.SendEmailAsync(mailRequest);
        }

        public async Task<bool> VerifyEmailAsync(Guid jobSeekerSignupRequestId)
        {
            SignUpRequest signUpRequest = await jobSeekerRepository.GetSignupRequestByIdAsync(jobSeekerSignupRequestId);
            if (signUpRequest != null)
            {
                signUpRequest.Status = Status.VERIFIED;
                jobSeekerRepository.UpdateSignupRequest(signUpRequest);
                return true;
            }
            return false;
        }

        public async Task CreateJobseeker(Guid jobSeekerSignupRequestId, string password)
        {
            try
            {
                SignUpRequest signUpRequest = await jobSeekerRepository.GetSignupRequestByIdAsync(jobSeekerSignupRequestId);


                if (signUpRequest.Status == Status.VERIFIED)
                {
                    Domain.Models.AuthUser authUser = mapper.Map<Domain.Models.AuthUser>(signUpRequest);
                    authUser.Password = password;

                    authUser = await authUserRepository.AddAuthUserJS(authUser);
                    signUpRequest.Status = Status.CREATED;
                    jobSeekerRepository.UpdateSignupRequest(signUpRequest);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<bool> ApplyJobAsync(Guid jobSeekerId, ApplyJobRequestDto requestDto)
        {
            try
            {
                var entity = mapper.Map<JobApplication>(requestDto);
                entity.Id = Guid.NewGuid();
                entity.ApplicantId = jobSeekerId;
                entity.Datesubmitted = DateTime.UtcNow;

                return await jobSeekerRepository.CreateJobApplicationAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error applying job: " + ex.Message, ex);
            }
        }

        public async Task<bool> HasAlreadyAppliedAsync(Guid jobSeekerId, Guid jobPostId)
        {
            return await jobSeekerRepository.HasAlreadyAppliedAsync(jobSeekerId, jobPostId);
        }

        public async Task<List<AppliedJobDto>> GetAppliedJobsAsync(Guid jobSeekerId)
        {
            var appliedJobs = await jobSeekerRepository.GetAppliedJobsAsync(jobSeekerId);
            return appliedJobs.Select(j => new AppliedJobDto
            {
                JobApplicationId = j.Id,
                JobPostId = j.JobPostId,
                JobTitle = j.JobPost.JobTitle,
                JobSummary = j.JobPost.JobSummary,
                PostedDate = j.JobPost.PostedDate,
                AppliedDate = j.Datesubmitted
            }).ToList();
        }

        public async Task<List<AppliedJobDto>> GetAppliedJobsByTitleAsync(Guid jobSeekerId, string jobTitle)
        {
            var appliedJobs = await jobSeekerRepository.GetAppliedJobsByTitleAsync(jobSeekerId, jobTitle);
            return appliedJobs.Select(j => new AppliedJobDto
            {
                JobApplicationId = j.Id,
                JobPostId = j.JobPostId,
                JobTitle = j.JobPost.JobTitle,
                JobSummary = j.JobPost.JobSummary,
                PostedDate = j.JobPost.PostedDate,
                AppliedDate = j.Datesubmitted
            }).ToList();
        }

        public async Task<bool> CancelAppliedJobAsync(Guid jobSeekerId, Guid jobApplicationId)
        {
            return await jobSeekerRepository.CancelAppliedJobAsync(jobSeekerId, jobApplicationId);
        }

        public async Task<bool> SaveJobAsync(Guid jobSeekerId, SavedJobDto dto)
        {
            bool jobExists = await jobSeekerRepository.JobExistsAsync(dto.JobId);
            if (!jobExists)
                return false;
            var savedJob = new SavedJob
            {
                Job = dto.JobId,
                SavedBy = jobSeekerId,
                DateSaved = DateTime.UtcNow
            };
            return await jobSeekerRepository.SaveJobAsync(savedJob);
        }

        public async Task<List<SavedJobDto>> GetSavedJobsAsync(Guid jobSeekerId)
        {
            var entities = await jobSeekerRepository.GetSavedJobsAsync(jobSeekerId);

            var result = entities.Select(s => new SavedJobDto
            {
                SavedJobId = s.Id,
                JobId = s.Job,
                JobTitle = s.JobPost.JobTitle,
                DateSaved = s.DateSaved
            }).ToList();

            return result;
        }



        public async Task<List<SavedJobDto>> GetSavedJobsByTitleAsync(Guid jobSeekerId, string title)
        {
            var entities = await jobSeekerRepository.GetSavedJobsByTitleAsync(jobSeekerId, title);

            var result = entities.Select(s => new SavedJobDto
            {
                SavedJobId = s.Id,
                JobId = s.Job,
                JobTitle = s.JobPost.JobTitle,
                DateSaved = s.DateSaved
            }).ToList();

            return result;
        }


        public async Task<bool> RemoveSavedJobAsync(Guid jobSeekerId, Guid savedJobId)
        {
            return await jobSeekerRepository.RemoveSavedJobAsync(jobSeekerId, savedJobId);
        }

        public async Task<IEnumerable<InterviewDto>> GetScheduledInterviewsAsync(Guid jobSeekerId)
        {
            var interviews = await jobSeekerRepository.GetAllByJobSeekerIdAsync(jobSeekerId);
            return mapper.Map<IEnumerable<InterviewDto>>(interviews);
        }

        public async Task<JobPostDto?> GetJobByTitleAsync(string title)
        {
            var jobs = await jobService.GetAllJobsAsync();
            return jobs.FirstOrDefault(j => j.JobTitle.Equals(title, StringComparison.OrdinalIgnoreCase));
        }



    }

}


