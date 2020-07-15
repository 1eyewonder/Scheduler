using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerAPI.Dtos
{
    public class AuthenticationDto
    {
        public string AccessToken { get; set; }

        public int? ExpiresIn { get; set; }

        public string TokenType { get; set; }

        public int? CreationTime { get; set; }

        public int? ExpirationTime { get; set; }

        public int AuthorizationLevel { get; set; }

        public int UserId { get; set; }

    }
}
