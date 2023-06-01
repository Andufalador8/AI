using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ai_lab5
{
    public class Subject
    {
        public string Name { get; set; }
        public string Teacher { get; set; }
        public int classesInWeek { get; set; }
        public Subject(string name, string teacher, int classesInWeek) 
        { 
            this.Name = name;
            this.Teacher = teacher; 
            this.classesInWeek = classesInWeek;
        }
        
    }
}
