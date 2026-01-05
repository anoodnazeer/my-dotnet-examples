
//using AutoMapper;
//using Domain.Models;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Domain.Service.Authuser.Interfaces;


//namespace Domain.Service.Authuser
//{
//    internal class AuthUserRepository
//    {

//        private readonly HireMeNowDbContext _context;
//        IMapper _mapper;
//        private readonly IConfiguration _configuration;
//        public AuthUserRepository(HireMeNowDbContext context, IMapper mapper, IConfiguration configuration)
//        {
//            _context = context;
//            _mapper = mapper;
//            _configuration = configuration;

//        }

//        public string? CreateToken(AuthUser user)
//        {
//            if (user == null)
//                throw new ArgumentNullException(nameof(user), "User object cannot be null.");

//            string tokenSecret = _configuration.GetSection("AuthSettings:Token").Value;
//            if (string.IsNullOrEmpty(tokenSecret))
//                throw new InvalidOperationException("Token secret is missing or empty in configuration.");

//            // Update connection info
//            user.ConnectionId = Guid.NewGuid().ToString();
//            user.OnlineStatus = true;
//            _context.SaveChanges();

//            List<Claim> claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.Name, user.FirstName ?? string.Empty),
//                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
//                new Claim(ClaimTypes.Sid, user.Id.ToString()),
//                new Claim(ClaimTypes.Role, user.Role.ToString()),
//                new Claim("ConnectionId", user.ConnectionId ?? string.Empty)
//            };

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecret));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

//            var token = new JwtSecurityToken(
//                claims: claims,
//                expires: DateTime.Now.AddDays(1),
//                signingCredentials: creds
//            );

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }



//        public async Task<AuthUser> AddAuthUserJS(AuthUser authUser)
//        {
//            authUser.Role = Enums.Role.JOB_SEEKER;
//            await _context.AuthUsers.AddAsync(authUser);
//            Models.JobSeeker jobSeeker = _mapper.Map<Models.JobSeeker>(authUser);
//            await _context.JobSeekers.AddAsync(jobSeeker);
//            JobSeekerProfile jp = new();
//            jp.Id = Guid.NewGuid();
//            jp.JobSeekerId = jobSeeker.Id;
//            await _context.JobSeekerProfiles.AddAsync(jp);
//            _context.SaveChanges();
//            return authUser;
//        }


//        // Add Auth User for Job Provider
//        public async Task<AuthUser> AddAuthUserJP(AuthUser authUser)

//        {
//            authUser.Role = Enums.Role.JOB_PROVIDER;

//            await _context.AuthUsers.AddAsync(authUser);

//            Models.JobSeeker jobSeeker = _mapper.Map<Models.JobSeeker>(authUser);
//            await _context.JobSeekers.AddAsync(jobSeeker);
//            JobSeekerProfile jp = new();
//            jp.Id = Guid.NewGuid();
//            jp.JobSeekerId = jobSeeker.Id;
//            await _context.JobSeekerProfiles.AddAsync(jp);
//            _context.SaveChanges();
//            return authUser;
//        }

//        public async Task AddUserAsync(AuthUser user)
//        {
//            await _context.AuthUsers.AddAsync(user);
//            await _context.SaveChangesAsync();
//        }



//    }
//}

using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Service.Authuser.Interfaces;


namespace Domain.Service.Authuser
{
    public class AuthUserRepository : IAuthUserRepository
    {

        private readonly HireMeNowDbContext _context;
        IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AuthUserRepository(HireMeNowDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;

        }

        public string? CreateToken(AuthUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User object cannot be null.");

            string tokenSecret = _configuration.GetSection("AuthSettings:Token").Value;
            if (string.IsNullOrEmpty(tokenSecret))
                throw new InvalidOperationException("Token secret is missing or empty in configuration.");

            // Update connection info
            user.ConnectionId = Guid.NewGuid().ToString();
            user.OnlineStatus = true;
            _context.SaveChanges();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("ConnectionId", user.ConnectionId ?? string.Empty)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public async Task<AuthUser> AddAuthUserJS(AuthUser authUser)
        {
            authUser.Role = Enums.Role.JOB_SEEKER;
            await _context.AuthUsers.AddAsync(authUser);
            Models.JobSeeker jobSeeker = _mapper.Map<Models.JobSeeker>(authUser);
            await _context.JobSeekers.AddAsync(jobSeeker);
            JobSeekerProfile jp = new();
            jp.Id = Guid.NewGuid();
            jp.JobSeekerId = jobSeeker.Id;
            await _context.JobSeekerProfiles.AddAsync(jp);
            _context.SaveChanges();
            return authUser;
        }


        // Add Auth User for Job Provider
        public async Task<AuthUser> AddAuthUserJP(AuthUser authUser)

        {
            authUser.Role = Enums.Role.JOB_PROVIDER;

            await _context.AuthUsers.AddAsync(authUser);

            Models.JobSeeker jobSeeker = _mapper.Map<Models.JobSeeker>(authUser);
            await _context.JobSeekers.AddAsync(jobSeeker);
            JobSeekerProfile jp = new();
            jp.Id = Guid.NewGuid();
            jp.JobSeekerId = jobSeeker.Id;
            await _context.JobSeekerProfiles.AddAsync(jp);
            _context.SaveChanges();
            return authUser;
        }

        public async Task AddUserAsync(AuthUser user)
        {
            await _context.AuthUsers.AddAsync(user);
            await _context.SaveChangesAsync();
        }



    }
}


