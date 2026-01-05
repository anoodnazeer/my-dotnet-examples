using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Models;
using Domain.Service.SignUp.Interface;

namespace Domain.Service.SignUp
{
    public class SignUpRequestRepository : ISignUpRequestRepository
    {
        private readonly HireMeNowDbContext _context;

        public SignUpRequestRepository(HireMeNowDbContext context)
        {
            _context = context;
        }

        public async Task AddJobProviderAsync(JobProviderCompany jobProvider)
        {
            await _context.JobProviderCompanies.AddAsync(jobProvider);
            await _context.SaveChangesAsync();
        }

        public Guid AddSignupRequest(SignUpRequest signUpRequest)
        {
            signUpRequest.Status = Status.PENDING;
            _context.SignUpRequests.Add(signUpRequest);
            _context.SaveChanges();
            return signUpRequest.Id;
        }

        public async Task<SignUpRequest> GetSignupRequestByIdAsync(Guid jobProviderSignupRequestId)
        {
            return await _context.SignUpRequests.FindAsync(jobProviderSignupRequestId);
        }

        public void UpdateSignupRequest(SignUpRequest signUpRequest)
        {
            _context.SignUpRequests.Update(signUpRequest);
            _context.SaveChanges();
        }


        public async Task AddSystemUserAsync(SystemUser user)
        {
            _context.SystemUsers.Add(user);
            await _context.SaveChangesAsync();
        }


    }
}