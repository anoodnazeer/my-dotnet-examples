using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Enums;
//using Domain.Helper;
using Domain.Models;
using Domain.Service.Authuser.Interfaces;
using Domain.Service.Email.Interface;
using Domain.Service.SignUp.DTO;
using Domain.Service.SignUp.Interface;
using BCrypt.Net;

using Org.BouncyCastle.Crypto.Generators;
using Microsoft.EntityFrameworkCore;
using Domain.Service.Authuser;
using Domain.Service.JobProvider;


namespace Domain.Service.SignUp
{
    public class SignUpRequestService : ISignUpRequestService
    {
        private readonly ISignUpRequestRepository _signUpRequestRepository;
        private readonly IAuthUserRepository _authUserRepository;
        private readonly IProviderEmailService _emailService;
        private readonly HireMeNowDbContext _context;

        public SignUpRequestService(
            ISignUpRequestRepository signUpRequestRepository,
            IAuthUserRepository authUserRepository,
            IProviderEmailService emailService,
            HireMeNowDbContext context)
        {
            _signUpRequestRepository = signUpRequestRepository;
            _authUserRepository = authUserRepository;
            _emailService = emailService;
            _context = context;
        }

        // 1️⃣ Create Signup Request
        public void CreateSignupRequest(SignUpRequestDto data)
        {
            var signUpRequest = new SignUpRequest
            {
                UserName = data.UserName,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                Phone = data.Phone,
                Status = Status.PENDING
            };

            // Add request and get ID
            var signUpId = _signUpRequestRepository.AddSignupRequest(signUpRequest);

            // Send email
            _emailService.SendEmailAsync(
                data.Email,
                "Signup Received",
                $"Hello {data.FirstName}, your signup request has been received.\nYour Request ID: {signUpId}"
            ).GetAwaiter().GetResult();
        }

        // 2️⃣ Verify Email
        public async Task<bool> VerifyEmailAsync(Guid jobProviderSignupRequestId)
        {
            var signupRequest = await _signUpRequestRepository.GetSignupRequestByIdAsync(jobProviderSignupRequestId);

            if (signupRequest == null || signupRequest.Status != Status.PENDING)
                return false;

            signupRequest.Status = Status.VERIFIED;
            _signUpRequestRepository.UpdateSignupRequest(signupRequest);

            
            // ✅ Step 1: Create JobProviderCompany with unique ID
            var jobProvider = new JobProviderCompany
            {
                Id = Guid.NewGuid(),                   // ✅ Must generate a new unique ID
                LegalName = signupRequest.UserName,    // or another suitable name
                Email = signupRequest.Email,
                Address = "Not Provided",
                Summary = "Newly verified job provider.",
                Website = string.Empty,
                Location = null

            };

            _context.JobProviderCompanies.Add(jobProvider);
            await _context.SaveChangesAsync();

            // ✅ Step 2: Link AuthUser → JobProviderCompany
            var authUser = await _context.AuthUsers
                .FirstOrDefaultAsync(u => u.Email == signupRequest.Email);

            if (authUser != null)
            {
                authUser.JobProviderId = jobProvider.Id;
                await _context.SaveChangesAsync();
            }

            return true;
        }



        // 3️⃣ Create Job Provider (after verification)

        // 3️⃣ Set Password
        // ✅ 3. Create Job Provider and set password

        public async Task SetPasswordForLoginAsync(Guid signUpRequestId, SetPasswordRequest request)
        {
            // 1️⃣ Get signup request
            var signUpRequest = await _signUpRequestRepository.GetSignupRequestByIdAsync(signUpRequestId);
            if (signUpRequest == null)
                throw new Exception("Signup request not found");

            // 2️⃣ Try to get AuthUser by Email
            var authUser = await _context.AuthUsers
                .FirstOrDefaultAsync(u => u.Email == signUpRequest.Email);

            // 3️⃣ Hash password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            if (authUser != null)
            {
                // ✅ Update password if AuthUser already exists
                authUser.Password = hashedPassword;
                authUser.Role = Enums.Role.JOB_PROVIDER; // must be set
            }
            else
            {
                // ✅ Create new AuthUser (also acts as SystemUser)
                authUser = new AuthUser
                {
                    Id = Guid.NewGuid(),
                    Email = signUpRequest.Email,
                    UserName = signUpRequest.Email,
                    Phone = signUpRequest.Phone,
                    FirstName = signUpRequest.FirstName,
                    LastName = signUpRequest.LastName,
                    Password = hashedPassword
                };

                _context.AuthUsers.Add(authUser);
            }

            await _context.SaveChangesAsync();
        }
    }
}