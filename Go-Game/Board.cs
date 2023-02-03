using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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


            // update groups & calculate captures
            updateGroups();
            bool validCapture = calcCaptures();

            if (!validCapture) { this.board[row, col] = 0; return false; }

            // switch player
            if (switchPlayer)
            {
                this.player = (this.player % 2) + 1;
            }
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
            List<Vector> liberties = new List<Vector>();

            queue.Add(new Vector(row, col));
            visited[row, col] = 1;

            while (queue.Count > 0)
            {
                Vector p = queue[0];
                int x = (int)p.X;
                int y = (int)p.Y;
                queue.RemoveAt(0);

                group.addStone((int)p.X, (int)p.Y);

                // find liberties
                if (validCoord(x+1,y) && !liberties.Contains(new Vector(x+1,y)) && board[x+1,y] == 0) { liberties.Add(new Vector(x + 1, y)); }
                if (validCoord(x-1,y) && !liberties.Contains(new Vector(x-1,y)) && board[x-1,y] == 0) { liberties.Add(new Vector(x - 1, y)); }
                if (validCoord(x,y+1) && !liberties.Contains(new Vector(x,y+1)) && board[x,y+1] == 0) { liberties.Add(new Vector(x, y + 1)); }
                if (validCoord(x,y-1) && !liberties.Contains(new Vector(x,y-1)) && board[x,y-1] == 0) { liberties.Add(new Vector(x, y - 1)); }

                // check north
                if (validCoord(x+1, y) && visited[x+1, y] != 1 && this.board[x +1, y] == group.getPlayer())
                {
                    queue.Add(new Vector(x + 1, y));
                    visited[x + 1, y] = 1; 
                }

                // check south
                if (validCoord(x-1, y) && visited[x-1, y] != 1 && this.board[x-1, y] == group.getPlayer())
                {
                    queue.Add(new Vector(x - 1, y));
                    visited[x - 1, y] = 1; 
                }

                // check east
                if (validCoord(x, y+1) && visited[x, y+1] != 1 && this.board[x, y+1] == group.getPlayer())
                {
                    queue.Add(new Vector(x, y + 1));
                    visited[x, y + 1] = 1; 
                }

                // check west
                if (validCoord(x, y-1) && visited[x, y-1] != 1 && this.board[x, y-1] == group.getPlayer())
                {
                    queue.Add(new Vector(x, y - 1));
                    visited[x, y - 1] = 1; 
                }
            }

            group.setLiberties(liberties.Count);

            return group;
        }

        private bool validCoord(int row, int col)
        {
            if (row < 0 || col < 0) return false;
            if (row >= this.size || col >= this.size) return false;
            return true;
        }

        // returns true if capture is valid, false otherwise
        private bool calcCaptures()
        {
            bool enemyCapture = false;
            for (int i=0; i<this.groups.Count; i++)
            {
                if (this.groups[i].getLiberties() == 0 && this.groups[i].getPlayer() != this.player)
                {
                    enemyCapture = true;
                    // remove all stones in the group
                    foreach (Vector p in this.groups[i].getStones())
                    {
                        board[(int)p.X, (int)p.Y] = 0;
                    }
                    groups.RemoveAt(i);
                } 
            }

            foreach(Group g in this.groups)
            {
                if (g.getPlayer() == this.player && !enemyCapture && g.getLiberties() == 0)
                {
                    // self capture, without capturing an enemy group
                    return false;
                }

            }
            return true;
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
}
