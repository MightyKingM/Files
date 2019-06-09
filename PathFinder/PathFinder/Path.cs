using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    public class Path
    {
        public List<Node> nodes;
        public Node lastnode
        {
            get
            {
                return nodes[nodes.Count - 1];
            }
            set
            {
                nodes[nodes.Count - 1] = value;
            }
        }

        public int length
        {
            get
            {
                return nodes.Count;
            }
        }

        public Path()
        {
            nodes = new List<Node>();
        }

        public void Draw(Graphics gfx)
        {
            foreach(Node node in nodes)
            {
                gfx.DrawEllipse(new Pen(Brushes.Black,2), new RectangleF(node.x * 100,node.y*100,99,99));
            }
        }
    }
}
