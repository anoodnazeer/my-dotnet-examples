using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain_js.Service.Authuser;
using Domain_js.Service.Authuser.Interfaces;
using Domain_js.Service.Login.DTOs;
using Domain_js.Service.Login.Interfaces;
using Domain_js.Service.SignUp.Interfaces;

namespace Domain_js.Service.Login
{
    public class LoginRequestService : ILoginRequestService
    {
        ILoginRequestRepository jobSeekerRepository;
        IAuthUserRepository authUserRepository;
        IMapper mapper;
        public LoginRequestService(ILoginRequestRepository _jobSeekerRepository, IMapper _mapper, IAuthUserRepository _authUserRepository)
        {
            jobSeekerRepository = _jobSeekerRepository;
            mapper = _mapper;
            
            authUserRepository = _authUserRepository;
        }

        public JobSeekerLoginDto login(string email, string password)
        {
            var user = jobSeekerRepository.GetUserByEmailpassword(email,password);
            if (user == null)
            {
                return null;
            }
            else
            {
                if ((password == user.Password))
                {
                    var userReturn = mapper.Map<JobSeekerLoginDto>(user);
                    userReturn.Token = authUserRepository.CreateToken(user);
                    return userReturn;
                }
                return null;
            }
           
        }

     
    }
       
    }

