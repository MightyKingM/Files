using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    public class Node
    {
        public Node n = null;
        public Node e = null;
        public Node w = null;
        public Node s = null;
        public char value = ' ';
        public int x;
        public int y;

        public Node()
        {

        }

        public Node(char value)
        {
            this.value = value;
        }

        public Point GetCenter()
        {
            return new Point(x * 100 - 45, y * 100 - 45);
        }

        public string GetNodeType()
        {
            if(value == 'd')
            {
                return "Destination";
            }
            else if(value == 'r')
            {
                return "Root";
            }
            else if(value == 'x')
            {
                return "Barrier";
            }
            else if(value == 'o')
            {
                return "Traversable Node";
            }
            else
            {
                return "Unkown";
            }
        }
    }
}
