using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    public class PathPlotter
    {
        public PathPlotter()
        {

        }

        private static int IsGoodPath(Path path, char destinationtype, char barrier)
        {
            int steps = 0;
            foreach(Node node in path.nodes)
            {
                if(node == null)
                {
                    return -1;
                }
                else if(node.value == barrier)
                {
                    return -1;
                }
                else
                {
                    if(node.value == destinationtype)
                    {
                        return steps;
                    }
                }
                steps++;
            }
            return -1;
        }

        public static Path ShortenPath(Path path, int maxsteps)
        {
            Path temp = new Path();
            int stepcount = 0;
            foreach(Node node in path.nodes)
            {
                if(stepcount == maxsteps + 1)
                {
                    return temp;
                }
                else
                {
                    temp.nodes.Add(node);
                }
                stepcount++;
            }
            return temp;
        }

        public static Path[] GetGoodPaths(char destination,char barrier,Path[] paths)
        {
            List<Path> toret = new List<Path>();
            foreach(Path path in paths)
            {
                int good = IsGoodPath(path, destination,barrier);
                if (good != -1)
                {
                    toret.Add(ShortenPath(path, good));
                }
            }
            return toret.ToArray();
        }

        public static Path FindShortestPath(Path[] paths)
        {
            if(paths.Length == 0)
            {
                throw new Exception("Cannot compute fastest paths out of no paths");
            }
            Path fastest = paths[0];
            foreach(Path path in paths)
            {
                if(fastest.length > path.length)
                {
                    fastest = path;
                }
            }
            return fastest;
        }

        public static Path FindFastestPathToDestination(Map map,char destination = 'd',char barrier = 'x')
        {
            Path[] allpaths = map.ComputeAllPaths();
            Path[] goodpaths = GetGoodPaths(destination, barrier, allpaths);
            return FindShortestPath(goodpaths);
        }
    }
}
