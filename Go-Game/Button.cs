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
    internal class Button
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

        public Button(int size) 
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
                    // Have transparancy.
                    makeTransparentBtn(boardBtns[x, y]);
                    // Have them scale and resize correctly
                    boardBtns[y, x].SetBounds(45 * x, 45 * y, 45, 45);
                    // Place buttons on the board.
                    // System.Windows.Form.Button(boardBtns[x, y]);
                }
            }
        }

        // Makes buttons transparaent.
        public Button makeTransparentBtn(Button btn)
        {
            btn.TabStop = false; // This means that the user can't select buttons by pressing tab
            btn.FlatStyle = FlatStyle.Flat; // removing 3D effects from the button with flat styling
            btn.FlatAppearance.BorderSize = 0; // removing border
            btn.Text = "b"; // Giving buttons text for testing purposes.
            btn.BackColor = Color.Transparent; // Making the button transparent with the form.
            return btn;
        }

        // Place stone is used to handle the stone being represented on the form and in the this.board 2D array.
        public void placeStone()
        {
            // Place coloured image into button, depending on whos turn it is.
            // Update the 2D array.
        }
