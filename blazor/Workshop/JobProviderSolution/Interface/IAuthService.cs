using JobProviderSolution.Dto;

namespace JobProviderSolution.Interface
{
    public interface  IAuthService
    {
        Task<bool> Register(JobProviderDto jobProviderDto, string password);
        Task<bool> Login(string email, string password);
        Task Logout();
    }
}
