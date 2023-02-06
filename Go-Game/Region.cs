using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GoGame
{
    internal class Region
    {
        private List<Vector> points;
        private List<Group> surroundingGroups;

        private bool? isEnclosed;

        public Region()
        {
            this.surroundingGroups = new List<Group>();
            this.points = new List<Vector>();
        }

        public void addPoint(int row, int col)
        {
            this.points.Add(new Vector(row, col));
        }

        public List<Vector> getPoints()
        {
            return this.points;
        }

        public bool inRegion(int row, int col)
        {
            return this.points.Contains(new Vector(row, col));
        }

        public void addGroup(Group group)
        {
            this.surroundingGroups.Add(group);
        }

        public List<Group> getSurrounding()
        {
            return this.surroundingGroups;
        }

        public bool pointSurrounds(int row, int col)
        {
            bool surrounds = false;
            foreach (Group g in this.surroundingGroups)
            {
                if (g.inGroup(row, col))
                {
                    surrounds = true;
                    break;
                }
            }
            return surrounds;
        }

        public bool getIsEnclosed()
        {
            if (this.isEnclosed.HasValue)
            {
                return (bool)this.isEnclosed;
            } else
            {
                isEnclosed = true;
                int firstPlayer = this.surroundingGroups[0].getPlayer();
                foreach (Group g in this.surroundingGroups)
                {
                    if (g.getPlayer() != firstPlayer)
                    {
                        isEnclosed = false;
                        break;
                    }
                }
                return (bool)isEnclosed;
            }
        }

        public void printRegion()
        {
            Console.WriteLine(this.points.Count().ToString() + " " + this.getIsEnclosed().ToString());
            foreach (Vector point in this.points)
            {
                Console.Write(point.ToString() + " ");
            }
            Console.Write("\n\n");
        }
    }
}
