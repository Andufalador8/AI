using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ailab5gen
{
    class Schedule
    {
        public Dictionary<int, string> ClassSchedule { get; set; }

        public Schedule()
        {
            ClassSchedule = new Dictionary<int, string>();
        }
    }
}
