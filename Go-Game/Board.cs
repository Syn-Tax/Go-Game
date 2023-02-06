using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace GoGame
{
    internal class Board
    {
        private int size;

        // 2d integer array to store board position - 0 is empty, 1 is black, 2 is white
        private int[,] board;
        private int player;
        private int numMoves;

        // list of grouped stones (required for capture and scoring logic)
        private List<Group> groups;

        // list of previous board positions (required for ko rule logic)
        private LinkedList<int[,]> prevBoards;

        public Board(int size) 
        {
            // initialise size
            this.size = size;

            // initialise board
            this.board = new int[this.size,this.size];
            for (int i=0; i<this.size; i++)
            {
                for (int j=0; j<this.size; j++)
                {
                    this.board[i,j] = 0;
                }
            }

            // initialise prevBoards variable
           this. prevBoards = new LinkedList<int[,]>();

            // initialise first player (to black)
            this.player = 1;
            this.numMoves = 0;
        }

        // returns current board position
        public int[,] getBoard()
        {
            return this.board;
        }

        // returns true if current player is white, false if current player is black
        public int getPlayer()
        {
            return this.player;
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

            // save board
            this.prevBoards.AddFirst((int[,])this.board.Clone());
            if (this.numMoves > 2)
            {
                this.prevBoards.RemoveLast();
            }

            // switch player
            if (switchPlayer)
            {
                this.player = (this.player % 2) + 1;
            }
            this.numMoves++;
            // return success
            return true;
        }

        // createButtons is used for the construction and mangement of buttons
        public void createButtons()
        {
            // Creating 2D array of buttons
            Button[,] boardBtns = new Button[this.size, this.size];
            // Loop using the board 2D array. i.e this.board 
            for (int x = 0; x < this.size; x++)
            {
                for (int y = 0; y< this.size; y++)
                {
                    // Placing the buttons onto the form
                    boardBtns[x, y] = new Button();
                    // Have them scale and resize correctly
                    boardBtns[y, x].SetBounds(45 * x, 45 * y, 45, 45);
                    // Have transparancy.
                }
            }
            // Place the buttons onto the form
            // Have them scale and resize correctly
            // Have transparancy.
        }

        // Place stone is used to handle the stone being represented on the form and in the this.board 2D array.
        public void placeStone()
        {
            // Place coloured image into button, depending on whos turn it is.
            // Update the 2D array.
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
            int[,] tempBoard = (int[,])this.board.Clone();
            bool enemyCapture = false;
            for (int i=0; i<this.groups.Count; i++)
            {
                if (this.groups[i].getLiberties() == 0 && this.groups[i].getPlayer() != this.player)
                {
                    enemyCapture = true;
                    // remove all stones in the group
                    foreach (Vector p in this.groups[i].getStones())
                    {
                        tempBoard[(int)p.X, (int)p.Y] = 0;
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
            if (listInList(tempBoard, this.prevBoards.ToList()))
            {
                // Ko rule activated!
                return false;
            }

            this.board = tempBoard;
            return true;
        }

        // for some unknown reason, .Contains didn't work so had to implement it myself :(
        private bool listInList(int[,] board, List<int[,]> list)
        {
            for (int i=0; i<list.Count; i++)
            {
                if (listEqual(list[i], board))
                {
                    return true;
                }
            }
            return false;
        }

        private bool listEqual(int[,] board1, int[,] board2)
        {
            for (int row=0; row<this.size; row++)
            {
                for (int col=0; col<this.size; col++)
                {
                    if (board1[row,col] != board2[row,col])
                    {
                        return false;
                    }
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

                for (LinkedListNode<int[,]> node = this.prevBoards.First; node != null; node = node.Next)
                {
                    Console.Write("\t");
                    for (int col=0; col<this.size; col++)
                    {
                        Console.Write(node.Value[row,col].ToString() + " ");
                    }
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
