using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerAPI.Dtos
{
    public class UserLoginDto
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(18)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
