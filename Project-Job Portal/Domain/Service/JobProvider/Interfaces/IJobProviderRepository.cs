using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Service.JobProvider.Dto;
using JobSeekerModel = Domain.Models.JobSeeker; // alias to fix naming conflict

namespace Domain.Service.JobProvider.Interfaces
{
    public interface IJobProviderRepository
    {

      
        // ================== Profile Picture ==================
        Task AddProfilePictureAsync(Guid jobProviderId, string filePath);
        Task<string?> GetProfilePicturePathAsync(Guid jobProviderId);
        Task DeleteProfilePictureAsync(Guid jobProviderId);

        // ================== Company ==================
        Task<CompanyUser?> GetCompanyByIdAsync(Guid companyId);
        Task<IEnumerable<JobProviderCompany>> GetAllCompaniesAsync();
        Task<CompanyUser> AddCompanyAsync(CompanyUser company);
        Task UpdateCompanyAsync(CompanyUser company);
        Task DeleteCompanyAsync(CompanyUser company);

        // ================== Company Member ==================
        Task<CompanyUser?> GetCompanyMemberByIdAsync(Guid memberId);
        Task<IEnumerable<CompanyUser>> GetAllCompanyMembersAsync();
        Task<CompanyUser> AddCompanyMemberAsync(CompanyUser member);
        Task UpdateCompanyMemberAsync(CompanyUser member);
        Task DeleteCompanyMemberAsync(CompanyUser member);

        Task<JobProviderCompany?> GetByIdAsync(Guid jobProviderId);
        Task<string> LogoutAsync(Guid jobProviderId);

        // ================== Save Changes ==================
        Task<int> SaveChangesAsync();

        //// -------------------------
        //// JOB POST METHODS
        //// -------------------------

        //// 21. Create Job Post
        //Task<Guid> CreateJobPostAsync(JobPost jobPost);

        //// 22. Get Job By ID
        //Task<JobPost?> GetJobByIdAsync(Guid id);

        //// 23. Update Job By ID
        //Task<bool> UpdateJobByIdAsync(Guid id, JobPost updatedJob);

        //// 24. Patch Job By ID (partial update — e.g., salary only)
        //Task<bool> PatchJobByIdAsync(Guid id, decimal? salary);

        //// 25. Delete Job By ID
        //Task<bool> DeleteJobByIdAsync(Guid id);

        //// 26. Get All Jobs
        //Task<List<JobPost>> GetAllJobsAsync();

        // 27. Get JobApplicants by Id
        Task<List<JobApplication>> GetApplicantsByJobIdAsync(Guid jobId);
        Task<JobApplication?> GetApplicantByApplicationIdAsync(Guid applicationId);

        // 27. Get Application Count
        Task<int> GetApplicationCountAsync();
    }
}