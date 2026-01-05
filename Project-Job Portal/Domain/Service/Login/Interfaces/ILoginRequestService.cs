
using Domain.Service.Login.DTO;
using Domain.Service.Login.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Service.Login.Interfaces
{
    public interface ILoginRequestService
    {

        AdminLoginDTO Adminlogin(string email, string password);
        Task<bool> LogoutAsync(Guid adminId);

        JobProviderLoginDto Login(string email, string password);
        Task<JobSeekerLoginDto?> LoginJS(string email, string password);

    }
}
