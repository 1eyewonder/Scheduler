using SchedulerAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerAPI.Models
{
    public class Customer : ICustomer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(5)]
        public string Name { get; set; }
    }
}
