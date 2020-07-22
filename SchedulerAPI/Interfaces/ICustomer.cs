using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerAPI.Interfaces
{
    public interface ICustomer
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
