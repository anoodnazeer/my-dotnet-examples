using Domain_js.Service.User.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain_js.Service.User
{
	public class UserServices:IUserService
	{
		private readonly IHttpContextAccessor httpContextAccessor;

		public UserServices(IHttpContextAccessor _httpContextAccessor)
		{
			httpContextAccessor = _httpContextAccessor;
		}

		public string GetUserId()
		{
			var result = string.Empty;
			if (httpContextAccessor.HttpContext != null)
			{
				result = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
			}
			return result;
		}

	}
}
