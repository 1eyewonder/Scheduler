using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerUI.Dtos
{
    public class PaginationResponseDto<T>
    {
        public int TotalPagesQuantity { get; set; }
        public List<T> Items { get; set; }
    }
}
