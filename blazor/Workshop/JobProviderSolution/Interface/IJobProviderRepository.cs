using JobProviderSolution.Model;

namespace JobProviderSolution.Interface
{
    public interface  IJobProviderRepository
    {
        Task<JobProvider> GetByEmailAsync(string email);
        Task AddAsync(JobProvider jobProvider);
    }
}
