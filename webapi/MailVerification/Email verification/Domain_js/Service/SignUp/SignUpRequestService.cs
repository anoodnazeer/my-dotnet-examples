using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain_js.Enums;
using Domain_js.Helpers;
using Domain_js.Models;
using Domain_js.Service.Authuser.Interfaces;
using Domain_js.Service.SignUp.DTOs;
using Domain_js.Service.SignUp.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Domain_js.Service.SignUp
{
    public class SignUpRequestService : ISignUpRequestService
    {
        ISignUpRequestRepository jobSeekerRepository;
        IAuthUserRepository authUserRepository;
        IMapper mapper;
        IEmailService emailService;
        public SignUpRequestService(ISignUpRequestRepository _jobSeekerRepository, IMapper _mapper, IEmailService _emailService, IAuthUserRepository _authUserRepository)
        {
            jobSeekerRepository = _jobSeekerRepository;
            mapper = _mapper;
            emailService = _emailService;
            authUserRepository = _authUserRepository;
        }

        public async Task CreateJobseeker(Guid jobSeekerSignupRequestId, string password)
        {
            try
            {
                SignUpRequest signUpRequest = await jobSeekerRepository.GetSignupRequestByIdAsync(jobSeekerSignupRequestId);


                if (signUpRequest.Status == Status.VERIFIED)
                {
                    Domain_js.Models.AuthUser authUser = mapper.Map<Domain_js.Models.AuthUser>(signUpRequest);
                    authUser.Password = password;

                    authUser = await authUserRepository.AddAuthUser(authUser);
                    signUpRequest.Status = Status.CREATED;
                    jobSeekerRepository.UpdateSignupRequest(signUpRequest);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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




    }
}
