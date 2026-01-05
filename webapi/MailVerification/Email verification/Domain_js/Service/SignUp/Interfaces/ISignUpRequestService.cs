using AutoMapper;
using Domain_js.Service.SignUp.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_js.Service.SignUp.Interfaces
{
    public interface ISignUpRequestService
    {
        Task CreateJobseeker(Guid jobSeekerSignupRequestId, string password);

        //Task<Guid> AddJobseekerSignUpRequest(JobSeekerSignupRequest jobseekerCreateRequest);
        void CreateSignupRequest(JobSeekerSignupRequestDto data);
       
        Task<bool> VerifyEmailAsync(Guid jobSeekerSignupRequestId);
    }
}
