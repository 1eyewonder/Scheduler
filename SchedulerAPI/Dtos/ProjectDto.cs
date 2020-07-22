using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerAPI.Dtos
{
    public class ProjectDto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(5)]
        [RegularExpression("^[0-9-]*$", ErrorMessage = "Project number must be numeric or '-' characters")]
        public string Number { get; set; }

        [Required]
        public int CustomerId { get; set; }
    }
}
