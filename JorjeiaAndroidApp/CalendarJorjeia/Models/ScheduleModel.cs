using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarJorjeia.Models
{
    public class ScheduleModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsPassed { get; set; }
        public bool IsPassed2 { get; set; }

    }
}
