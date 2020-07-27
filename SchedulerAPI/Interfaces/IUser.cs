using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerAPI.Interfaces
{
    public interface IUser
    {
        int Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        string Password { get; set; }
    }
}
