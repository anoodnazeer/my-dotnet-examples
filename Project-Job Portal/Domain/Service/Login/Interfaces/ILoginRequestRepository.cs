
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Login.Interfaces
{
    public interface ILoginRequestRepository
    {

        AuthUser? GetUserByEmail(string email);


        Task<AuthUser?> GetUserByEmailAsync(string email);
        Task<AuthUser?> GetUserByEmailAndPasswordAsync(string email, string password);

    }
}

