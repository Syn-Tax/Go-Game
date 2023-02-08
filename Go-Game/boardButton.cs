﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace GoGame
{
    internal class boardButton
    {
        // We may need other board details in order to handle the placement of stones.
        Board board;
        GameBoard gameBoard;

        public boardButton(Board board, GameBoard gameboard)
        {
            this.board = board;
            this.gameBoard = gameboard;
        }

        // createButtons is used for the construction and mangement of buttons
        public void createButtons()
        {
            // Creating 2D array of buttons
            Button[,] boardBtns = new Button[this.board.getSize(), this.board.getSize()];
            // Loop using the board 2D array. i.e this.board 
            for (int row = 0; row < this.board.getSize(); row++)
            {
                for (int col = 0; col < this.board.getSize(); col++)
                {
                    // Placing the buttons onto the form
                    boardBtns[row, col] = new Button();
                    // Have transparancy.
                    makeTransparentBtn(boardBtns[row, col]);
                    // Have them scale and resize correctly
                    boardBtns[row, col].SetBounds(33 * col + 250, 33 * row + 70, 15, 15);

                    // create event handler for button
                    boardBtns[row, col].Click += new EventHandler(this.btnEvent_Click);
                    boardBtns[row, col].Tag = row.ToString() + " " + col.ToString();
                    // Place buttons on the board.
                    // System.Windows.Form.Button(boardBtns[x, y]);
                    this.gameBoard.addButton(boardBtns[row, col]);
                }
            }
        }

        // Makes buttons transparaent.
        public Button makeTransparentBtn(Button btn)
        {
            btn.TabStop = false; // This means that the user can't select buttons by pressing tab
            btn.FlatStyle = FlatStyle.Flat; // removing 3D effects from the button with flat styling
            btn.FlatAppearance.BorderSize = 0; // removing border
            btn.BackColor = Color.FromArgb(219, 176, 107); // Making the button transparent with the form.
            return btn;
        }

        // Place stone is used to handle the stone being represented on the form and in the this.board 2D array.
        public void placeStone()
        {
            // Place coloured image into button, depending on whos turn it is.
            // Update the 2D array.
        }

        // on click event handler, mainly used for placeStone()
        void btnEvent_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int row = Int32.Parse(btn.Tag.ToString().Split()[0]);
            int col = Int32.Parse(btn.Tag.ToString().Split()[1]);
            //Console.WriteLine("Button " + row.ToString() + "," + col.ToString() + " was pressed");
            bool isLegal = this.board.move(row, col, this.gameBoard);
            if (!isLegal)
            {

                // Creating a message box indicate illegal move
                DialogResult illegalMove;
                illegalMove = MessageBox.Show("Last move was Illegal!!!", "Illegal Move!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}