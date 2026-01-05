using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Service.SignUp.Interface
{
  public interface ISignUpRequestRepository
    {
        Task AddJobProviderAsync(JobProviderCompany jobProvider);

        Guid AddSignupRequest(SignUpRequest signUpRequest);

        Task<SignUpRequest> GetSignupRequestByIdAsync(Guid jobProviderSignupRequestId);

        void UpdateSignupRequest(SignUpRequest signUpRequest);

        Task AddSystemUserAsync(SystemUser systemUser);
    }
}
