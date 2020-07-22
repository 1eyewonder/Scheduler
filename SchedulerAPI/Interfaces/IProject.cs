using SchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerAPI.Interfaces
{
    public interface IProject
    {
        int Id { get; set; }

        string Name { get; set; }

         string Number { get; set; }

        int CustomerId { get; set; }
    }
}
