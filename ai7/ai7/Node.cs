using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai7
{
    class Node
    {
        public char[,] Board { get; set; }
        public int Score { get; set; }
        public List<Node> Children { get; set; }
        public bool IsMax { get; set; }
    }

}
