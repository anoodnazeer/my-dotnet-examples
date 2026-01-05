using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
//using Domain.Helper;
using Domain.Models;
using Domain.Service.JobProvider.Dto;
using Domain.Service.JobProvider.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using JobSeekerModel = Domain.Models.JobSeeker;

namespace Domain.Service.JobProvider
{
    public class JobProviderService : IJobProviderService
    {
        private readonly IJobProviderRepository _repository;
        private readonly HireMeNowDbContext _context; // add this
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public JobProviderService(IJobProviderRepository repository, HireMeNowDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
            _configuration = configuration;   // ✅ Added

        }



        // ================== Profile Picture ==================
        public async Task<string> AddProfilePictureAsync(Guid jobProviderId, IFormFile file)
        {
            var jobProvider = await _context.JobProviderCompanies.FindAsync(jobProviderId);
            if (jobProvider == null)
                return "JobProvider not found";

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                jobProvider.ProfilePictureData = ms.ToArray();
            }

            await _context.SaveChangesAsync();
            return "Profile picture added successfully";
        }

        public async Task<string> UpdateProfilePictureAsync(Guid jobProviderId, IFormFile file)
        {
            var jobProvider = await _context.JobProviderCompanies.FindAsync(jobProviderId);
            if (jobProvider == null)
                return "JobProvider not found";

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                jobProvider.ProfilePictureData = ms.ToArray();
            }

            // Force EF to track and save
            _context.JobProviderCompanies.Update(jobProvider);
            await _context.SaveChangesAsync();

            return "Profile picture updated successfully";
        }


        public async Task<string> DeleteProfilePictureAsync(Guid jobProviderId)
        {
            var jobProvider = await _context.JobProviderCompanies.FindAsync(jobProviderId);
            if (jobProvider == null)
                return "JobProvider not found";

            jobProvider.ProfilePictureData = null;
            await _context.SaveChangesAsync();

            return "Profile picture deleted successfully";
        }

        public async Task<FileContentResult?> GetProfilePictureAsync(Guid jobProviderId)
        {
            var jobProvider = await _context.JobProviderCompanies.FindAsync(jobProviderId);
            if (jobProvider == null || jobProvider.ProfilePictureData == null)
                return null;

            // Optionally detect image type dynamically
            string contentType = "image/jpeg"; // default
            return new FileContentResult(jobProvider.ProfilePictureData, contentType);
        }

        // ================== Company ==================
        public async Task<(Guid CompanyId, string Message)> AddCompanyAsync(
      Guid jobProviderId, string companyName, string location, string industry, string websiteUrl)
        {
            var company = new CompanyUser
            {
                Id = Guid.NewGuid(),
                FirstName = companyName,
                Email = string.Empty,
                Phone = string.Empty
            };

            var companyEntity = new JobProviderCompany
            {
                Id = Guid.NewGuid(),
                LegalName = companyName,
                Email = string.Empty,
                Address = "Default Address",              // ✅ Keep text address separate
                Summary = industry,
                Website = websiteUrl,
                Location = Guid.Parse(location),          // ✅ This fixes FK constraint
                CompanyUsers = { company }
            };

            _context.JobProviderCompanies.Add(companyEntity);
            await _context.SaveChangesAsync();

            return (companyEntity.Id, "Company added successfully");
        }


        public async Task<IEnumerable<JobProviderCompany>> GetAllCompaniesAsync()
        {
            return await _context.JobProviderCompanies
                .Include(c => c.CompanyUsers) // keep this if needed
                .ToListAsync(); // removed LocationNavigation
        }

        public async Task<JobProviderCompany> GetCompanyByIdAsync(Guid companyId)
        {
            return await _context.JobProviderCompanies
                .Include(c => c.CompanyUsers)
                .FirstOrDefaultAsync(c => c.Id == companyId) ?? throw new Exception("Company not found");
        }

        public async Task<string> UpdateCompanyByIdAsync(Guid companyId, string companyName, string location, string industry, string websiteUrl)
        {
            var company = await _context.JobProviderCompanies.FindAsync(companyId);
            if (company == null) return "Company not found";

            company.LegalName = companyName;
            company.Address = location;
            company.Summary = industry;
            company.Website = websiteUrl;

            await _context.SaveChangesAsync();
            return "Company updated successfully";
        }

        public async Task<string> PatchCompanyByIdAsync(Guid companyId, string industry)
        {
            var company = await _context.JobProviderCompanies.FindAsync(companyId);
            if (company == null) return "Company not found";

            company.Summary = industry;
            await _context.SaveChangesAsync();
            return "Company industry updated successfully";
        }

        public async Task<string> DeleteCompanyByIdAsync(Guid companyId)
        {
            var company = await _context.JobProviderCompanies
                .Include(c => c.CompanyUsers) // ✅ Load related users
                .FirstOrDefaultAsync(c => c.Id == companyId);

            if (company == null)
                return "Company not found";

            // ✅ Step 1: Remove all related CompanyUsers
            if (company.CompanyUsers != null && company.CompanyUsers.Any())
                _context.CompanyUsers.RemoveRange(company.CompanyUsers);

            // ✅ Step 2: Remove the company itself
            _context.JobProviderCompanies.Remove(company);

            await _context.SaveChangesAsync();
            return "Company deleted successfully";
        }

        // ================== Company Member ==================
        public async Task<(Guid MemberId, string Message)> AddCompanyMemberAsync(Guid companyId, string memberName, string designation, string email, string phone)
        {
            var company = await _context.JobProviderCompanies.FindAsync(companyId);
            if (company == null) return (Guid.Empty, "Company not found");

            var member = new CompanyUser
            {
                Id = Guid.NewGuid(),
                FirstName = memberName,
                Role = Enums.Role.MEMBER,
                Email = email,
                Phone = phone,
                CompanyNavigation = company
            };

            _context.CompanyUsers.Add(member);
            await _context.SaveChangesAsync();
            return (member.Id, "Company member added successfully");
        }

        public async Task<IEnumerable<CompanyUser>> GetAllCompanyMembersAsync()
        {
            return await _context.CompanyUsers
                .Include(cu => cu.CompanyNavigation) // optional, load parent company
                .ToListAsync();
        }

        public async Task<CompanyUser> GetCompanyMemberByIdAsync(Guid memberId)
        {
            return await _context.CompanyUsers
                .Include(c => c.CompanyNavigation)
                .FirstOrDefaultAsync(c => c.Id == memberId) ?? throw new Exception("Company member not found");
        }

        public async Task<string> UpdateCompanyMemberAsync(Guid memberId, string memberName, string designation, string email, string phone)
        {
            var member = await _context.CompanyUsers.FindAsync(memberId);
            if (member == null) return "Company member not found";

            member.FirstName = memberName;
            member.Role = Enums.Role.MEMBER; // or set based on designation
            member.Email = email;
            member.Phone = phone;

            await _context.SaveChangesAsync();
            return "Company member updated successfully";
        }

        public async Task<string> PatchCompanyMemberAsync(Guid memberId, string designation)
        {
            var member = await _context.CompanyUsers.FindAsync(memberId);
            if (member == null) return "Company member not found";

            member.Role = Enums.Role.MEMBER; // Or map from designation
            await _context.SaveChangesAsync();
            return "Company member designation updated successfully";
        }

        public async Task<string> DeleteCompanyMemberAsync(Guid memberId)
        {
            var member = await _context.CompanyUsers.FindAsync(memberId);
            if (member == null) return "Company member not found";

            _context.CompanyUsers.Remove(member);
            await _context.SaveChangesAsync();
            return "Company member deleted successfully";
        }

        public async Task<string> LogoutAsync(Guid jobProviderId)
        {
            var jobProvider = await _context.JobProviderCompanies.FindAsync(jobProviderId);
            if (jobProvider == null) return "Job provider not found";

            // In stateless JWT, there's nothing to remove server-side.
            // You can optionally log the logout action to a table for auditing.
            return "Logout successful. Please clear your token on the client side.";
        }


        // -------------------------
        // JOB APPLICATION METHODS
        // -------------------------


        public async Task<List<ApplicantDto>> GetApplicantsByJobIdAsync(Guid jobId)
        {
            var applications = await _repository.GetApplicantsByJobIdAsync(jobId);
            return _mapper.Map<List<ApplicantDto>>(applications);
        }

        public async Task<ApplicantDto?> GetApplicantByApplicationIdAsync(Guid applicationId)
        {
            var application = await _repository.GetApplicantByApplicationIdAsync(applicationId);
            return _mapper.Map<ApplicantDto?>(application);
        }

        public async Task<int> GetApplicationCountAsync()
        {
            return await _repository.GetApplicationCountAsync();
        }
    }
}

