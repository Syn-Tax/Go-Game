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

        public Group(int player) 
        { 
            this.stones = new List<Vector>();
            this.liberties = new List<Vector>();
            this.healthyRegions = new List<Region>();
            this.player = player;
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

        

        public void printGroup(Board board)
        {
            Console.WriteLine(this.stones.Count().ToString() + " " + this.player.ToString() + " " + this.liberties.Count().ToString() + " " + this.getSafety(board).ToString());
            foreach (Vector stone in this.stones)
            {
                Console.Write(stone.ToString() + " ");
            }
            Console.Write("\n");
            foreach (Vector liberty in this.liberties)
            {
                Console.Write(liberty.ToString() + " ");
            }
            Console.Write("\n\n");
        }

        public bool getSafety(Board board)
        {
            if (this.isSafe.HasValue)
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
            int totalSLC = 0;
            foreach (Region r in this.healthyRegions)
            {
                totalSLC += sureLibertyCount(board, r);
            }

            if (totalSLC >= 2)
            {
                return true;
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

        private int sureLibertyCount(Board board, Region region)
        {
            List<Group> G = new List<Group>();
            foreach (Group g in board.getGroups())
            {
                if (g.getPlayer() == this.player) { G.Add(g); }
            }

            if (!this.healthyRegions.Contains(region)) { return 0; }

            foreach (Group g in region.getSurrounding())
            {
                if (!G.Contains(g)) { return 0; }
            }

            return 1;
        }
    }
}
