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
    internal class boardButton
    {
        // Need to get/inherit size of the board for Board.cs
        public int size;

        // We may need other board details in order to handle the placement of stones.

        public boardButton()
        {
            // Getting the size from board.
            // initialise size
            // this.size = Board.getSize();
        }

        // createButtons is used for the construction and mangement of buttons
        public void createButtons()
        {
            // Creating 2D array of buttons
            Button[,] boardBtns = new Button[this.size, this.size];
            // Loop using the board 2D array. i.e this.board 
            for (int x = 0; x < this.size; x++)
            {
                for (int y = 0; y < this.size; y++)
                {
                    // Placing the buttons onto the form
                    boardBtns[x, y] = new Button();
                    // Have transparancy.
                    makeTransparentBtn(boardBtns[x, y]);
                    // Have them scale and resize correctly
                    boardBtns[y, x].SetBounds(45 * x, 45 * y, 45, 45);
                    // Place buttons on the board.
                    // System.Windows.Form.Button(boardBtns[x, y]);
                    GameBoard.addButton(boardBtns[x, y]);
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

        // on click event handler, mainly used for placeStone()
        void btnEvent_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Button has been clicked");
        }
    }
}