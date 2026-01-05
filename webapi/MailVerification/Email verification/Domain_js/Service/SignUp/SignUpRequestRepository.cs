using Domain_js.Enums;
using Domain_js.Models;
using Domain_js.Service.SignUp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain_js.Service.SignUp
{
    public class SignUpRequestRepository : ISignUpRequestRepository
    {
        private readonly HireMeNowDbContext _context;

        public SignUpRequestRepository(HireMeNowDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AddJobSeekerAsync(JobSeeker jobseeker)
        {
            await _context.JobSeekers.AddAsync(jobseeker);
            await _context.SaveChangesAsync();
        }

        public Guid AddSignupRequest(SignUpRequest signUpRequest)
        {
            signUpRequest.Status = Status.PENDING;
            _context.SignUpRequests.AddAsync(signUpRequest);
            _context.SaveChanges();
            return signUpRequest.Id;
        }

        public async Task<SignUpRequest> GetSignupRequestByIdAsync(Guid jobSeekerSignupRequestId)
        {
            return await _context.SignUpRequests.FindAsync(jobSeekerSignupRequestId);
        }

        public void UpdateSignupRequest(SignUpRequest signUpRequest)
        {
            _context.SignUpRequests.Update(signUpRequest);
            _context.SaveChanges();
        }
    }
}
