using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Service.SignUp.DTO;

namespace Domain.Service.SignUp.Interface
{
  public interface ISignUpRequestService
    {
        

        void CreateSignupRequest(SignUpRequestDto data);

        Task<bool> VerifyEmailAsync(Guid jobProviderSignupRequestId);

        Task SetPasswordForLoginAsync(Guid signUpRequestId, SetPasswordRequest request);
    }
}
