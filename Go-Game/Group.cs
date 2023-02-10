/*
    Alex Glen  
    Lewis Simmonds
    Oscar Morris
    AC22005
    Grid Game
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GoGame
{
    internal class Group
    {
        private int player;
        private List<Vector> stones;
        private List<Vector> liberties;
        private bool? isSafe;
        private List<Region> healthyRegions;
        private List<Region> contactingRegions;
        private Chain chain;

        public Group(int player) 
        { 
            this.stones = new List<Vector>();
            this.liberties = new List<Vector>();
            this.healthyRegions = new List<Region>();
            this.contactingRegions = new List<Region>();
            this.player = player;
            this.chain = new Chain(new List<Group> { this }, new List<Vector>());
        }

        public void addStone(int row, int col)
        {
            this.stones.Add(new Vector(row,col));
        }

        public List<Vector> getStones()
        {
            return this.stones;
        }

        public bool inGroup(int row, int col) 
        { 
            return this.stones.Contains(new Vector(row, col)); 
        }

        public int getPlayer()
        {
            return this.player;
        }

        public void setChain(Chain c)
        {
            this.chain = c;
        }

        public List<Region> getHealthy()
        {
            return this.healthyRegions;
        }

        public List<Region> getContacting()
        {
            return this.contactingRegions;
        }

        public void addRegion(Region region)
        {
            this.contactingRegions.Add(region);
        }

        public void calcLiberties(Board board)
        {
            // reset liberties list
            this.liberties = new List<Vector>();

            foreach (Vector stone in this.stones)
            {
                int x = (int)stone.X;
                int y = (int)stone.Y;
                // find liberties
                if (board.validCoord(x + 1, y) && !liberties.Contains(new Vector(x + 1, y)) && board.getBoard()[x + 1, y] == 0) { this.liberties.Add(new Vector(x + 1, y)); }
                if (board.validCoord(x - 1, y) && !liberties.Contains(new Vector(x - 1, y)) && board.getBoard()[x - 1, y] == 0) { this.liberties.Add(new Vector(x - 1, y)); }
                if (board.validCoord(x, y + 1) && !liberties.Contains(new Vector(x, y + 1)) && board.getBoard()[x, y + 1] == 0) { this.liberties.Add(new Vector(x, y + 1)); }
                if (board.validCoord(x, y - 1) && !liberties.Contains(new Vector(x, y - 1)) && board.getBoard()[x, y - 1] == 0) { this.liberties.Add(new Vector(x, y - 1)); }
            }
        }

        public List<Vector> getLiberties()
        {
            return this.liberties;
        }

        public bool getSafety(Board board, bool recalculate=false)
        {
            if (this.isSafe.HasValue && !recalculate)
            {
                return (bool)this.isSafe;
            } else
            {
                this.isSafe = (bool?)this.calculateSafety(board);
                return (bool)this.isSafe;
            }
        }

        private bool calculateSafety(Board board)
        {
            int totalSLC;
            if (this.chain.getGroups().Count == 1)
            {
                totalSLC = this.sureLibertyCount(board, this.contactingRegions);
            } else
            {
                totalSLC = this.chain.sureLibertyCount(board);
            }

            if (totalSLC >= 2)
            {
                // group is provably alive!!! (algorithm from: http://webdocs.cs.ualberta.ca/~mmueller/ps/gpw97.pdf)
                return true;
            }

            foreach (Region r in this.contactingRegions)
            {
                if (r.getPlayer() == this.player && r.getPoints().Count > 5)
                {
                    return true;
                }
            }

            return false;
        }

        public void calculateHealthy(Board board)
        {
            foreach (Region region in board.getRegions())
            {
                bool isHealthy = true;
                foreach (Vector point in region.getPoints())
                {
                    if (!this.liberties.Contains(point))
                    {
                        isHealthy = false;
                    }
                }
                if (isHealthy)
                {
                    this.healthyRegions.Add(region);
                }
            }
        }

        public int sureLiberty(Board board, Region region)
        {
            List<Group> G = new List<Group>();
            foreach (Group g in board.getGroups())
            {
                if (g.getPlayer() == this.player) { G.Add(g); }
            }

            if (!this.contactingRegions.Contains(region)) { return 0; }

            foreach (Group g in region.getSurrounding())
            {
                if (!G.Contains(g)) { return 0; }
            }

            return 1;
        }

        public int sureLibertyCount(Board board, List<Region> regions)
        {
            int count = 0;
            foreach (Region r in regions)
            {
                count += sureLiberty(board, r);
            }
            return count;
        }
    }
}
