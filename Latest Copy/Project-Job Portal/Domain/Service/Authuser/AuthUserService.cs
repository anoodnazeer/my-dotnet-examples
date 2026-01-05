
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Service.Authuser.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Domain.Service.Authuser
{
    public class AuthUserService : IAuthUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthUserRepository _userRepository;
        private readonly HireMeNowDbContext _context;

        public AuthUserService(IHttpContextAccessor httpContextAccessor, IAuthUserRepository userRepository, HireMeNowDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _context = context;
        }

        public string GetUserId()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value.ToString();
            }
            return result;
        }

        public async Task LogoutAsync(Guid userId)
        {
            var user = await _context.AuthUsers.FindAsync(userId);
            if (user != null)
            {
                user.ConnectionId = null;
                user.OnlineStatus = false;
                await _context.SaveChangesAsync();
            }
        }

    }
}

