using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerAPI.Models
{
    public class JobRevision
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int JobId { get; set; }

        /// <summary>
        /// Revision number of the job
        /// </summary>
        [Required(ErrorMessage ="RevisionNumber cannot be null")]
        public int RevisionNumber { get; set; }

        /// <summary>
        /// Brief summary explaining reason for changes
        /// </summary>
        [MaxLength(200, ErrorMessage = "The revision summary must be under 200 characters")]
        public string RevisionSummary { get; set; }
    }
}
