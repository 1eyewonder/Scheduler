using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerAPI.Models
{
    public class Job
    {
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Number users use to correlate a job when it is in the quote stage
        /// </summary>
        [Required(ErrorMessage ="Quote number cannot be null or empty")]
        [MaxLength(15)]
        [MinLength(5)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Quote number must be numeric")]
        public string QuoteNumber { get; set; }

        /// <summary>
        /// Number users use to correlate a job when it is sold
        /// </summary>
        [MaxLength(15)]
        [MinLength(5)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Job number must be numeric")]
        public string JobNumber { get; set; }

        /// <summary>
        /// Number users use to groups jobs that are categorized under the same project
        /// </summary>
        [MaxLength(15)]
        [MinLength(5)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Project number must be numeric")]
        public string ProjectNumber { get; set; }

        /// <summary>
        /// Child table to keep track of quote revisions
        /// </summary>
        [Required]
        public ICollection<QuoteRevision> QuoteRevisions { get; set; } = new List<QuoteRevision>();

        /// <summary>
        /// Child table to keep track of job revisions
        /// </summary>
        [Required]
        public ICollection<JobRevision> JobRevisions { get; set; } = new List<JobRevision>();

        /// <summary>
        /// Indicates this job is sold or not
        /// </summary>
        [Required(ErrorMessage ="IsAJob cannot be null")]
        public bool IsAJob { get; set; }
    }
}
