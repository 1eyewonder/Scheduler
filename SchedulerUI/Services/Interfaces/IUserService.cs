﻿using SchedulerAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SchedulerUI.Services.Interfaces
{
    interface IUserService
    {
        bool IsLoggedIn { get; set; }
        Task<HttpResponseMessage> Login(UserLoginDto userLogin);
        Task<HttpResponseMessage> Register(UserDto user);
        Task LogOut();
        Task SetAuthorizationHeader();
        Task<bool> MinimumAuthRequired(int authLevel);
    }
}
