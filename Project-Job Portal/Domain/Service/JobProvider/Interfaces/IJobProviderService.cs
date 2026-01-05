using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Service.JobProvider.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobSeekerModel = Domain.Models.JobSeeker;

namespace Domain.Service.JobProvider.Interfaces
{
    public interface IJobProviderService
    {

        // ================== Profile Picture ==================
       
            Task<string> AddProfilePictureAsync(Guid jobProviderId, IFormFile file);
            Task<string> UpdateProfilePictureAsync(Guid jobProviderId, IFormFile file);
            Task<string> DeleteProfilePictureAsync(Guid jobProviderId);
            Task<FileContentResult?> GetProfilePictureAsync(Guid jobProviderId);
        


        // ================== Company ==================
        Task<(Guid CompanyId, string Message)> AddCompanyAsync(Guid jobProviderId, string companyName, string location, string industry, string websiteUrl);
        Task<JobProviderCompany> GetCompanyByIdAsync(Guid companyId);

        Task<IEnumerable<JobProviderCompany>> GetAllCompaniesAsync();
        Task<string> UpdateCompanyByIdAsync(Guid companyId, string companyName, string location, string industry, string websiteUrl);
        Task<string> PatchCompanyByIdAsync(Guid companyId, string industry);
        Task<string> DeleteCompanyByIdAsync(Guid companyId);

        // ================== Company Member ==================
        Task<(Guid MemberId, string Message)> AddCompanyMemberAsync(Guid companyId, string memberName, string designation, string email, string phone);

        Task<IEnumerable<CompanyUser>> GetAllCompanyMembersAsync();

        Task<CompanyUser> GetCompanyMemberByIdAsync(Guid memberId);
        Task<string> UpdateCompanyMemberAsync(Guid memberId, string memberName, string designation, string email, string phone);
        Task<string> PatchCompanyMemberAsync(Guid memberId, string designation);
        Task<string> DeleteCompanyMemberAsync(Guid memberId);

        Task<string> LogoutAsync(Guid jobProviderId);


        //// -------------------------
        //// JOB POST METHODS
        //// -------------------------
        //Task<Guid> CreateJobPostAsync(JobPostDto jobPost);
        //Task<JobPostDto?> GetJobByIdAsync(Guid id);
        //Task<bool> UpdateJobByIdAsync(Guid id, JobPostDto updatedJob);
        //Task<bool> PatchJobByIdAsync(Guid id, decimal? salary);
        //Task<bool> DeleteJobByIdAsync(Guid id);
        //Task<List<JobPostDto>> GetAllJobsAsync();

        // -------------------------
        // JOB APPLICATION METHODS
        // -------------------------

        Task<List<ApplicantDto>> GetApplicantsByJobIdAsync(Guid jobId);
        Task<ApplicantDto?> GetApplicantByApplicationIdAsync(Guid applicationId);

        Task<int> GetApplicationCountAsync();
    }
}
