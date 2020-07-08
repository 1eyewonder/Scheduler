using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerAPI.Models
{
    public class QuoteRevision
    {
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Foreign id referencing Job model
        /// </summary>
        [ForeignKey("Job")]
        [Required]
        public int JobId { get; set; }
        public Job Job { get; set; }

        /// <summary>
        /// Revision number of the quote
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
