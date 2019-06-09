using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    public class Map
    {
        public Node root;
        public Path RootPath
        {
            get
            {
                Path temp = new Path();
                temp.nodes.Add(root);
                return temp;
            }
        }
        private List<Node> drawnnodes;

        public Map(Node root)
        {
            this.root = root;
        }

        public Map()
        {
            root = new Node();
        }

        public void Draw(Graphics gfx)
        {
            drawnnodes = new List<Node>();
            Draw(gfx, new Node[] { root },0);
        }

        public Path[] ComputeAllPaths()
        {
            List<Path> ret = new List<Path>();
            ComputeAllPaths(RootPath, ref ret);
            return ret.ToArray();
        }

        public void ComputeAllPaths(Path path,ref List<Path> list)
        {
            if(path.lastnode == null)
            {
                list.Add(path);
            }
            else
            {
                Path npath = new Path();
                Path spath = new Path();
                Path epath = new Path();
                Path wpath = new Path();
                npath.nodes = path.nodes.ToList();
                spath.nodes = path.nodes.ToList();
                epath.nodes = path.nodes.ToList();
                wpath.nodes = path.nodes.ToList();
                if(!npath.nodes.Contains(npath.lastnode.n))
                {
                    npath.nodes.Add(path.lastnode.n);
                    ComputeAllPaths(npath, ref list);
                }
                if (!spath.nodes.Contains(spath.lastnode.s))
                {
                    spath.nodes.Add(path.lastnode.s);
                    ComputeAllPaths(spath, ref list);
                }
                if (!epath.nodes.Contains(epath.lastnode.e))
                {
                    epath.nodes.Add(path.lastnode.e);
                    ComputeAllPaths(epath, ref list);
                }
                if (!wpath.nodes.Contains(wpath.lastnode.w))
                {
                    wpath.nodes.Add(path.lastnode.w);
                    ComputeAllPaths(wpath, ref list);
                }
            }
        }

        public Node[] ComputeAllNodes()
        {
            List<Node> list = new List<Node>();
            ComputeAllNodes(root, ref list);
            return list.ToArray();
        }

        public void ComputeAllNodes(Node node, ref List<Node> masterlist)
        {
            if(masterlist.Contains(node) || node == null)
            {
                return;
            }
            else
            {
                masterlist.Add(node);
            }
            ComputeAllNodes(node.n, ref masterlist);
            ComputeAllNodes(node.s, ref masterlist);
            ComputeAllNodes(node.e, ref masterlist);
            ComputeAllNodes(node.w, ref masterlist);
        }

        public void Draw(Graphics gfx, Node[] todraw, int depth)
        {
            foreach(Node node in todraw)
            {
                if(drawnnodes.Contains(node))
                {
                    
                }
                else
                {
                    drawnnodes.Add(node);
                    if (node.value == 'o')
                    {
                        gfx.FillEllipse(Brushes.Green, new Rectangle(node.x * 100, node.y * 100, 99, 99));
                    }
                    else if(node.value == 'r')
                    {
                        gfx.FillEllipse(Brushes.Yellow, new Rectangle(node.x * 100, node.y * 100, 99, 99));
                    }
                    else if(node.value == 'x')
                    {
                        gfx.FillEllipse(Brushes.Red, new Rectangle(node.x * 100, node.y * 100, 99, 99));
                    }
                    else if(node.value == 'd')
                    {
                        gfx.FillEllipse(Brushes.Blue, new Rectangle(node.x * 100, node.y * 100, 99, 99));
                    }
                    List<Node> nodestodraw = new List<Node>();
                    if (node.w != null)
                    {
                        nodestodraw.Add(node.w);
                    }
                    if (node.e != null)
                    {
                        nodestodraw.Add(node.e);
                    }
                    if (node.n != null)
                    {
                        nodestodraw.Add(node.n);
                    }
                    if (node.s != null)
                    {
                        nodestodraw.Add(node.s);
                    }
                    Draw(gfx, nodestodraw.ToArray(), depth + 1);
                }
            }
        }

        public static Map FromFile(string filepath)
        {
            string[] rows = File.ReadAllLines(filepath);
            Node[][] table = new Node[rows.Length][];
            for (int i = 0; i < table.Length; i++)
            {
                 table[i] = new Node[rows[0].Length];
            }
            for (int y = 0; y < rows.Length; y++)
            {
                char[] cols = rows[y].ToCharArray();
                for (int x = 0; x < cols.Length; x++)
                {
                    table[y][x] = new Node(cols[x]);
                    table[y][x].x = x;
                    table[y][x].y = y;
                }
            }
            int rootx = 0;
            int rooty = 0;
            for(int y = 0; y < table.Length; y++)
            {
                for (int x = 0; x < table[y].Length; x++)
                {
                    if(table[y][x].value == 'r')
                    {
                        rootx = x;
                        rooty = y;
                    }
                    if(y != table.Length-1)
                    {
                        table[y][x].s = table[y + 1][x];
                        table[y + 1][x].n = table[y][x];
                    }
                    if (x != table[y].Length -1)
                    {
                        table[y][x].e = table[y][x + 1];
                        table[y][x + 1].w = table[y][x];
                    }
                }
            }
            return new Map(table[rooty][rootx]);
        }
    }
}
