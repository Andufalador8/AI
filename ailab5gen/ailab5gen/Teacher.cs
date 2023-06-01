using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ailab5gen
{
    class Teacher
    {
        public string Name { get; set; }
        public List<string> AvailableSubjects { get; set; }

        public Teacher(string name, List<string> subjects)
        {
            Name = name;
            AvailableSubjects = subjects;
        }
    }
}
