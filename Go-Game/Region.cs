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

        private int? player;

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

        public int getPlayer()
        {
            if (this.player.HasValue)
            {
                return (int)this.player;
            } else
            {
                int blackCount = 0;
                int whiteCount = 0;

                foreach (Group g in this.surroundingGroups)
                {
                    if (g.getPlayer() == 1)
                    {
                        blackCount += g.getStones().Count;
                    } else if (g.getPlayer() == 2)
                    {
                        whiteCount += g.getStones().Count;
                    }
                }
                if (blackCount > whiteCount)
                {
                    this.player = 1;
                } else if (whiteCount > blackCount)
                {
                    this.player = 2;
                } else
                {
                    this.player = 0;
                }

                return (int)this.player;
            }
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
            Console.WriteLine(this.points.Count().ToString() + " " + this.getPlayer().ToString());
            foreach (Vector point in this.points)
            {
                Console.Write(point.ToString() + " ");
            }
            Console.Write("\n\n");
        }
    }
}
