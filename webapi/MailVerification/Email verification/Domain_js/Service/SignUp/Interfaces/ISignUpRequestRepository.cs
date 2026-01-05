using Domain_js.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_js.Service.SignUp.Interfaces
{
    public interface ISignUpRequestRepository
    {
        Task AddJobSeekerAsync(Models.JobSeeker jobseeker);
        Guid AddSignupRequest(SignUpRequest signUpRequest);
        Task<SignUpRequest> GetSignupRequestByIdAsync(Guid jobSeekerSignupRequestId);
        void UpdateSignupRequest(SignUpRequest signUpRequest);
    }
}
