using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GoGame
{
    internal class Chain
    {
        private List<Group> groups;
        private List<Vector> connectionPoints;

        public Chain(List<Group> chain, List<Vector> connectingPoints)
        {
            this.groups = chain;
            this.connectionPoints = connectingPoints;
        }

        public void addGroup(Group g)
        {
            if (!this.inChain(g)) { this.groups.Add(g); }
        }

        public List<Group> getGroups()
        {
            return this.groups;
        }

        public void addPoints(List<Vector> points)
        {
            this.connectionPoints.AddRange(points);
        }

        public List<Vector> getPoints()
        {
            return this.connectionPoints;
        }

        public bool inChain(Group group)
        {
            return this.groups.Contains(group);
        }

        public int sureLibertyCount(Board board)
        {
            List<Region> regions = new List<Region>();
            foreach (Group g in this.groups)
            {
                regions.AddRange(g.getContacting());
            }

            int max = 0;
            foreach (Group g in this.groups)
            {
                int slc = g.sureLibertyCount(board, regions);
                if (slc > max) { max = slc; }
            }
            return max;
        }
    }
}
