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
        private int liberties;

        public Group(int player) 
        { 
            stones = new List<Vector>();
            this.player = player;
        }

        public void addStone(int row, int col)
        {
            stones.Add(new Vector(row,col));
        }

        public List<Vector> getStones()
        {
            return stones;
        }

        public bool inGroup(int row, int col) 
        { 
            return stones.Contains(new Vector(row, col)); 
        }

        public int getPlayer()
        {
            return this.player;
        }

        public void setLiberties(int liberties)
        {
            this.liberties = liberties;
        }

        public int getLiberties()
        {
            return this.liberties;
        }

        public void printGroup()
        {
            Console.WriteLine(stones.Count().ToString() + " " + this.player.ToString() + " " + this.liberties.ToString());
            foreach (Vector stone in this.stones)
            {
                Console.Write(stone.ToString() + " ");
            }
            Console.Write("\n\n");
        }
    }
}
