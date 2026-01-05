using AutoMapper;
using Domain_js.Models;
using Domain_js.Service.Authuser.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain_js.Service.Authuser
{
    public class AuthUserRepository: IAuthUserRepository
    {
        protected readonly HireMeNowDbContext _context;
        IMapper mapper;
        private readonly IConfiguration _configuration;
        public AuthUserRepository(HireMeNowDbContext dbContext,IMapper _mapper, IConfiguration configuration)
        {
            _context = dbContext;
            mapper = _mapper;
            _configuration = configuration;
        }

        public async Task<AuthUser> AddAuthUser(AuthUser authUser)
        {
            authUser.Role = Enums.Role.JOB_SEEKER;
            await  _context.AuthUsers.AddAsync(authUser);
            Models.JobSeeker jobSeeker = mapper.Map<Models.JobSeeker>(authUser);
            await _context.JobSeekers.AddAsync(jobSeeker);
            JobSeekerProfile jp = new();
            jp.JobSeekerId = jobSeeker.Id;


           await  _context.JobSeekerProfiles.AddAsync(jp);
            _context.SaveChanges();
            return authUser;
        }

       

        public string? CreateToken(AuthUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User object cannot be null.");
            }
            string tokenSecret = _configuration.GetSection("AuthSettings:Token").Value;
            if (string.IsNullOrEmpty(tokenSecret))
            {
                throw new InvalidOperationException("Token secret is missing or empty in configuration.");
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AuthSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        


       
   
    }
}
