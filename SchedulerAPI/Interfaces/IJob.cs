using SchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerAPI.Interfaces
{
    public interface IJob
    {
        int Id { get; set; }

        string QuoteNumber { get; set; }

        string JobNumber { get; set; }

        int ProjectId { get; set; }
    }
}
