﻿using Microsoft.AspNetCore.Http;

namespace ExampleMicroservice.Shared.Service
{
    public class SharedIdentityService:ISharedIdentityService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
    }
}