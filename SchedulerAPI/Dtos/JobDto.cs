using System.ComponentModel.DataAnnotations;

namespace SchedulerAPI.Dtos
{
    public class JobDto
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Quote number cannot be null or empty")]
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
    }
}