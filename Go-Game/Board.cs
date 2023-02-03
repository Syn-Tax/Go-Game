using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GoGame
{
    internal class Board
    {
        private int size;

        // 2d integer array to store board position - 0 is empty, 1 is black, 2 is white
        private int[,] board;
        private int player;

        // list of grouped stones (required for capture and scoring logic)
        private List<Group> groups;

        public Board(int size) 
        {
            // initialise size
            this.size = size;

            // initialise board
            board = new int[this.size,this.size];
            for (int i=0; i<this.size; i++)
            {
                for (int j=0; j<this.size; j++)
                {
                    board[i,j] = 0;
                }
            }

            // initialise first player (to black)
            this.player = 1;
        }

        // returns current board position
        public int[,] getBoard()
        {
            return board;
        }

        // returns true if current player is white, false if current player is black
        public int getPlayer()
        {
            return player;
        }

        // returns true if move was legally made, false if move was illegal and could not be played
        public bool move(int row, int col, bool switchPlayer)
        {
            // check if already a stone there
            if (this.board[row,col] != 0)
            {
                return false; // already a stone at board pos x,y
            }
            
            // make move
            this.board[row,col] = this.player;

            // switch player
            if (switchPlayer)
            {
                this.player = (this.player % 2) + 1;
            }

            // update groups & calculate captures
            updateGroups();
            calcCaptures(row, col);

            // return success
            return true;
        }

        public void updateGroups()
        {
            // reset groups
            this.groups = new List<Group>();

            // loop through every position
            for (int row=0; row<this.size; row++)
            {
                for (int col=0; col<this.size; col++)
                {
                    // check if there's a stone here
                    if (this.board[row, col] == 0)
                    {
                        continue;
                    }

                    // check if the stone is already in a group
                    bool inGroup = false;
                    foreach (Group group in this.groups)
                    {
                        if (group.inGroup(row, col))
                        {
                            Console.WriteLine("already in group");
                            inGroup = true;
                            break;
                        }
                    }
                    if (inGroup) { continue; }

                    // create a new group from current location
                    groups.Add(createGroup(row, col));
                }
            }
        }

        private Group createGroup(int row, int col)
        {
            Group g = new Group(this.board[row, col]);

            g = traverseGroup(row, col, g);

            return g;
        }

        // BFS based flood-fill algorithm
        // https://www.wikiwand.com/en/Flood_fill
        private Group traverseGroup(int row, int col, Group group)
        {
            int[,] visited = new int[this.size, this.size];

            List<Vector> queue = new List<Vector>();

            queue.Add(new Vector(row, col));
            visited[row, col] = 1;

            while (queue.Count > 0)
            {
                Vector p = queue[0];
                queue.RemoveAt(0);

                group.addStone((int)p.X, (int)p.Y);

                // check north
                if (validCoord((int)p.X+1, (int)p.Y)
                    && visited[(int)p.X+1, (int)p.Y] != 1
                    && this.board[(int)p.X+1, (int)p.Y] == group.getPlayer())
                {
                    queue.Add(new Vector(p.X + 1, p.Y));
                    visited[(int)p.X + 1, (int)p.Y] = 1; 
                }

                // check south
                if (validCoord((int)p.X-1, (int)p.Y)
                    && visited[(int)p.X-1, (int)p.Y] != 1
                    && this.board[(int)p.X-1, (int)p.Y] == group.getPlayer())
                {
                    queue.Add(new Vector(p.X - 1, p.Y));
                    visited[(int)p.X - 1, (int)p.Y] = 1; 
                }

                // check east
                if (validCoord((int)p.X, (int)p.Y+1)
                    && visited[(int)p.X, (int)p.Y+1] != 1
                    && this.board[(int)p.X, (int)p.Y+1] == group.getPlayer())
                {
                    queue.Add(new Vector(p.X, p.Y + 1));
                    visited[(int)p.X, (int)p.Y + 1] = 1; 
                }

                // check west
                if (validCoord((int)p.X, (int)p.Y-1)
                    && visited[(int)p.X, (int)p.Y-1] != 1
                    && this.board[(int)p.X, (int)p.Y-1] == group.getPlayer())
                {
                    queue.Add(new Vector(p.X, p.Y - 1));
                    visited[(int)p.X, (int)p.Y - 1] = 1; 
                }
            }

            return group;
        }

        private bool validCoord(int row, int col)
        {
            if (row < 0 || col < 0) return false;
            if (row >= this.size || col >= this.size) return false;
            return true;
        }

        private void calcCaptures(int row, int col)
        {
            
        }

        public void printBoard()
        {
            for (int row=0; row<this.size; row++)
            {
                for (int col=0; col<this.size; col++)
                {
                    Console.Write(this.board[row, col].ToString() + " ");
                }
                Console.Write("\n");
            }

            Console.Write("\n\n");
            foreach (Group group in this.groups)
            {
                group.printGroup();
            }
        }
    }

    internal class Group
    {
        private int player;
        private List<Vector> stones;

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

        public void printGroup()
        {
            Console.WriteLine(stones.Count().ToString() + " " + this.player.ToString());
            foreach (Vector stone in this.stones)
            {
                Console.Write(stone.ToString() + " ");
            }
            Console.Write("\n\n");
        }
    }
}
