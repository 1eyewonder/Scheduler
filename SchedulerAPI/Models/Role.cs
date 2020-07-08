using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerAPI.Models
{
    public class Role
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserRole { get; set; }

        [Required]
        public int AuthorizationLevel { get; set; }
    }
}
