using System;
using System.Collections.Generic;
using System.Drawing;
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

        // list of regions (required for scoring logic)
        private List<Region> regions;

        // list of chains (also required for scoring logic)
        private List<Chain> chains;

        // list of previous board positions (required for ko rule logic)
        private LinkedList<int[,]> prevBoards;

        // integers for tracking prisoners per player
        private List<Vector> blackPrisoners;
        private List<Vector> whitePrisoners;

        private bool lastMovePass;
        private float komi;

        public Board(int size, float komi)
        {
            // initialise size
            this.size = size;

            // initialise board
            this.board = new int[this.size, this.size];
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    this.board[i, j] = 0;
                }
            }

            // initialise prevBoards variable
            this.prevBoards = new LinkedList<int[,]>();

            // initialise first player (to black)
            this.player = 1;
            this.numMoves = 0;

            // initialise prisoner vars
            this.blackPrisoners = new List<Vector>();
            this.whitePrisoners = new List<Vector>();

            this.lastMovePass = false;
            this.komi = komi;
        }

        // returns the size of the board.
        public int getSize()
        {
            return size;    
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

        public List<Region> getRegions()
        {
            return this.regions;
        }

        public List<Group> getGroups()
        {
            return this.groups;
        }

        public List<Chain> getChains()
        {
            return this.chains;
        }

        // returns true if move was legally made, false if move was illegal and could not be played
        public bool move(int row, int col, GameBoard gb)
        {
            Console.WriteLine("Move at: " + row.ToString() + "," + col.ToString());
            // check for pass
            if (row == -1 || col == -1)
            {
                this.player = (this.player % 2) + 1;
                this.numMoves++;

                if (this.lastMovePass)
                {
                    float score = this.calculateScore(this.komi);
                    //this.printBoard();
                    Console.WriteLine("\n\n" + score.ToString());
                    Console.WriteLine(string.Join(" ", this.blackPrisoners));
                    Console.WriteLine(string.Join(" ", this.whitePrisoners));

                }
                else
                {
                    this.lastMovePass = true;
                }

                return true;
            }

            // check if already a stone there
            if (this.board[row, col] != 0)
            {
                return false; // already a stone at board pos x,y
            }

            // make move
            this.board[row, col] = this.player;


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

            // recalculate liberties
            foreach (Group g in this.groups)
            {
                g.calcLiberties(this);
            }

            // switch player
            this.player = (this.player % 2) + 1;
            this.numMoves++;

            // return success
            return true;
        }

        public void updateGroups()
        {
            // reset groups
            this.groups = new List<Group>();

            // loop through every position
            for (int row = 0; row < this.size; row++)
            {
                for (int col = 0; col < this.size; col++)
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
                    Group g = new Group(this.board[row, col]);

                    g = traverseGroup(row, col, g);
                    this.groups.Add(g);
                }
            }
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

                group.addStone(x, y);


                // check north
                if (validCoord(x + 1, y) && visited[x + 1, y] != 1 && this.board[x + 1, y] == group.getPlayer())
                {
                    queue.Add(new Vector(x + 1, y));
                    visited[x + 1, y] = 1;
                }

                // check south
                if (validCoord(x - 1, y) && visited[x - 1, y] != 1 && this.board[x - 1, y] == group.getPlayer())
                {
                    queue.Add(new Vector(x - 1, y));
                    visited[x - 1, y] = 1;
                }

                // check east
                if (validCoord(x, y + 1) && visited[x, y + 1] != 1 && this.board[x, y + 1] == group.getPlayer())
                {
                    queue.Add(new Vector(x, y + 1));
                    visited[x, y + 1] = 1;
                }

                // check west
                if (validCoord(x, y - 1) && visited[x, y - 1] != 1 && this.board[x, y - 1] == group.getPlayer())
                {
                    queue.Add(new Vector(x, y - 1));
                    visited[x, y - 1] = 1;
                }
            }

            group.calcLiberties(this);

            return group;
        }

        public void updateRegions()
        {
            // reset regions
            this.regions = new List<Region>();

            // loop through every position
            for (int row = 0; row < this.size; row++)
            {
                for (int col = 0; col < this.size; col++)
                {
                    // check if there's a stone here (if there is, continue)
                    if (this.board[row, col] != 0)
                    {
                        continue;
                    }

                    // check if the intersection is already in a region
                    bool inRegion = false;
                    foreach (Region region in this.regions)
                    {
                        if (region.inRegion(row, col))
                        {
                            inRegion = true;
                            break;
                        }
                    }
                    if (inRegion) { continue; }

                    // create a new region from current location
                    Region r = new Region();

                    r = traverseRegion(row, col, r);
                    this.regions.Add(r);
                }
            }
        }

        // same algorithm as `traverseGroup` with minor tweaks
        private Region traverseRegion(int row, int col, Region region)
        {
            int[,] visited = new int[this.size, this.size];

            List<Vector> queue = new List<Vector>();

            queue.Add(new Vector(row, col));
            visited[row, col] = 1;

            while (queue.Count > 0)
            {
                Vector p = queue[0];
                int x = (int)p.X;
                int y = (int)p.Y;
                queue.RemoveAt(0);

                region.addPoint(x, y);

                // find surrounding groups
                if (validCoord(x + 1, y) && !region.pointSurrounds(x + 1, y) && board[x + 1, y] != 0) { region.addGroup(this.groupAt(x + 1, y)); this.groupAt(x + 1, y).addRegion(region); }
                if (validCoord(x - 1, y) && !region.pointSurrounds(x - 1, y) && board[x - 1, y] != 0) { region.addGroup(this.groupAt(x - 1, y)); this.groupAt(x - 1, y).addRegion(region); }
                if (validCoord(x, y + 1) && !region.pointSurrounds(x, y + 1) && board[x, y + 1] != 0) { region.addGroup(this.groupAt(x, y + 1)); this.groupAt(x, y + 1).addRegion(region); }
                if (validCoord(x, y - 1) && !region.pointSurrounds(x, y - 1) && board[x, y - 1] != 0) { region.addGroup(this.groupAt(x, y - 1)); this.groupAt(x, y - 1).addRegion(region); }

                // check north
                if (validCoord(x + 1, y) && visited[x + 1, y] != 1 && this.board[x + 1, y] == 0)
                {
                    queue.Add(new Vector(x + 1, y));
                    visited[x + 1, y] = 1;
                }

                // check south
                if (validCoord(x - 1, y) && visited[x - 1, y] != 1 && this.board[x - 1, y] == 0)
                {
                    queue.Add(new Vector(x - 1, y));
                    visited[x - 1, y] = 1;
                }

                // check east
                if (validCoord(x, y + 1) && visited[x, y + 1] != 1 && this.board[x, y + 1] == 0)
                {
                    queue.Add(new Vector(x, y + 1));
                    visited[x, y + 1] = 1;
                }

                // check west
                if (validCoord(x, y - 1) && visited[x, y - 1] != 1 && this.board[x, y - 1] == 0)
                {
                    queue.Add(new Vector(x, y - 1));
                    visited[x, y - 1] = 1;
                }
            }

            return region;
        }

        public void calculateChains()
        {
            this.chains = new List<Chain>();

            foreach (Group g in this.groups)
            {
                foreach (Group G in this.groups)
                {
                    if (g == G) { continue; }
                    if (g.getPlayer() != G.getPlayer()) { continue; }

                    List<Vector> sharedLiberties = (List<Vector>)g.getLiberties().Intersect(G.getLiberties()).ToList();

                    if (sharedLiberties.Count >= 2)
                    {
                        this.addToChain(g, G, sharedLiberties);
                    }
                    else
                    {
                        if (sharedLiberties.Count == 1)
                        {
                            int x = (int)sharedLiberties[0].X;
                            int y = (int)sharedLiberties[0].Y;

                            List<Vector> pointLiberties = new List<Vector>();

                            if (this.validCoord(x + 1, y) && !pointLiberties.Contains(new Vector(x + 1, y)) && this.board[x + 1, y] == 0) { pointLiberties.Add(new Vector(x + 1, y)); }
                            if (this.validCoord(x - 1, y) && !pointLiberties.Contains(new Vector(x - 1, y)) && this.board[x - 1, y] == 0) { pointLiberties.Add(new Vector(x - 1, y)); }
                            if (this.validCoord(x, y + 1) && !pointLiberties.Contains(new Vector(x, y + 1)) && this.board[x, y + 1] == 0) { pointLiberties.Add(new Vector(x, y + 1)); }
                            if (this.validCoord(x, y - 1) && !pointLiberties.Contains(new Vector(x, y - 1)) && this.board[x, y - 1] == 0) { pointLiberties.Add(new Vector(x, y - 1)); }

                            if (pointLiberties.Count <= 1)
                            {
                                this.addToChain(g, G, sharedLiberties);
                            }
                        }
                    }
                }
            }
        }

        private void addToChain(Group g, Group G, List<Vector> sharedLiberties)
        {

            bool inChain = false;
            foreach (Chain chain in this.chains)
            {
                if (chain.inChain(g) || chain.inChain(G))
                {
                    inChain = true;
                    chain.addGroup(G);
                    chain.addGroup(g);
                    chain.addPoints(sharedLiberties);

                    G.setChain(chain);
                    g.setChain(chain);
                    break;
                }
            }
            if (!inChain)
            {
                Chain c = new Chain(new List<Group> { g, G }, sharedLiberties);
                this.chains.Add(c);
                G.setChain(c);
                g.setChain(c);
            }
        }

        private Group groupAt(int row, int col)
        {
            foreach (Group group in this.groups)
            {
                if (group.inGroup(row, col))
                {
                    return group;
                }
            }
            throw new InvalidOperationException("invalid point");
        }

        public bool validCoord(int row, int col)
        {
            if (row < 0 || col < 0) return false;
            if (row >= this.size || col >= this.size) return false;
            return true;
        }

        private void removeDead()
        {
            for (int i = 0; i < this.groups.Count; i++)
            {
                if (!(bool)this.groups[i].getSafety(this))
                {
                    Console.WriteLine("Found dead group with stone at: " + this.groups[i].getStones()[0].ToString());
                    foreach (Vector p in this.groups[i].getStones())
                    {
                        this.board[(int)p.X, (int)p.Y] = 0;
                        if (this.groups[i].getPlayer() == 2)
                        {
                            this.blackPrisoners.Add(p);
                        }
                        else
                        {
                            this.whitePrisoners.Add(p);
                        }
                    }
                    this.groups.RemoveAt(i);
                }
            }
        }

        public float calculateScore(float komi)
        {
            this.updateGroups();
            this.updateRegions();
            this.calculateChains();
            foreach (Group group in this.groups)
            {
                group.getSafety(this, true);
            }

            this.removeDead();
            this.updateGroups();
            this.updateRegions();

            float score = 0;

            foreach (Region r in this.regions)
            {
                if (r.getPlayer() == 1)
                {
                    score += r.getPoints().Count;
                }
                else if (r.getPlayer() == 2)
                {
                    score -= r.getPoints().Count;
                }
            }
            score += blackPrisoners.Count;
            score -= whitePrisoners.Count;

            score -= komi;

            return score;
        }

        // returns true if capture is valid, false otherwise
        private bool calcCaptures()
        {
            int[,] tempBoard = (int[,])this.board.Clone();
            bool enemyCapture = false;
            for (int i = 0; i < this.groups.Count; i++)
            {
                if (this.groups[i].getLiberties().Count == 0 && this.groups[i].getPlayer() != this.player)
                {
                    enemyCapture = true;
                    // remove all stones in the group
                    foreach (Vector p in this.groups[i].getStones())
                    {
                        tempBoard[(int)p.X, (int)p.Y] = 0;
                        if (this.player == 1)
                        {
                            this.blackPrisoners.Add(p);
                        }
                        else
                        {
                            this.whitePrisoners.Add(p);
                        }
                    }
                    this.groups.RemoveAt(i);
                }
            }

            foreach (Group g in this.groups)
            {
                if (g.getPlayer() == this.player && !enemyCapture && g.getLiberties().Count == 0)
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
            for (int i = 0; i < list.Count; i++)
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
            for (int row = 0; row < this.size; row++)
            {
                for (int col = 0; col < this.size; col++)
                {
                    if (board1[row, col] != board2[row, col])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
