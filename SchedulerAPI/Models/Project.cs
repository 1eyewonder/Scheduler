using SchedulerAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerAPI.Models
{
    /// <summary>
    /// Project overview details such as name, location, customer, etc.
    /// </summary>
    public class Project : IProject
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
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [Required]
        public Customer Customer { get; set; } 
    }
}
