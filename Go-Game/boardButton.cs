using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
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
        private Board board;
        private GameBoard gameBoard;
        private Button[,] boardBtns;

        private Button passBtn;
        private Button resignBtn;
        private Label playerText;

        private Label blackPrisoners;
        private Label whitePrisoners;

        private Color boardColor = Color.FromArgb(219, 176, 107);
        private Color blackStone = Color.FromArgb(0, 0, 0);
        private Color whiteStone = Color.FromArgb(255, 255, 255);

        public boardButton(Board board, GameBoard gameboard)
        {
            this.board = board;
            this.gameBoard = gameboard;
            this.boardBtns = new Button[this.board.getSize(), this.board.getSize()];
        }

        // createButtons is used for the construction and mangement of buttons
        public void createButtons()
        {
            // Loop using the board 2D array. i.e this.board 
            for (int row = 0; row < this.board.getSize(); row++)
            {
                for (int col = 0; col < this.board.getSize(); col++)
                {
                    // Placing the buttons onto the form
                    this.boardBtns[row, col] = new Button();
                    // Have transparancy.
                    makeTransparentBtn(this.boardBtns[row, col]);
                    // Have them scale and resize correctly
                    this.boardBtns[row, col].SetBounds(33 * col + 250, 33 * row + 70, 15, 15);

                    // create event handler for button
                    this.boardBtns[row, col].Click += new EventHandler(this.btnEvent_Click);
                    this.boardBtns[row, col].Tag = row.ToString() + " " + col.ToString();
                    // Place buttons on the board.
                    // System.Windows.Form.Button(boardBtns[x, y]);
                    this.gameBoard.addObject(this.boardBtns[row, col]);
                }
            }

            // create pass & resign button
            this.passBtn = new Button();
            this.passBtn.SetBounds(650, 100, 100, 50);
            this.passBtn.Text = "Pass";
            this.passBtn.Click += new EventHandler(this.passBtn_Click);
            this.gameBoard.addObject(this.passBtn);

            this.resignBtn = new Button();
            this.resignBtn.SetBounds(650, 200, 100, 50);
            this.resignBtn.Text = "Resign";
            this.resignBtn.Click += new EventHandler(this.resignBtn_Click);
            this.gameBoard.addObject(this.resignBtn);

            // create current player label
            this.playerText = new Label();
            this.playerText.SetBounds(650, 50, 100, 50);
            this.playerText.Text = "Black to Play";
            this.gameBoard.addObject(this.playerText);

            // create prisoner labels
            this.blackPrisoners = new Label();
            this.blackPrisoners.SetBounds(650, 275, 100, 25);
            this.blackPrisoners.Text = "Black Prisoners: 0";
            this.gameBoard.addObject(this.blackPrisoners);

            this.whitePrisoners = new Label();
            this.whitePrisoners.SetBounds(650, 300, 100, 25);
            this.whitePrisoners.Text = "White Prisoners: 0";
            this.gameBoard.addObject(this.whitePrisoners);
        }

        // Makes buttons transparaent.
        public Button makeTransparentBtn(Button btn)
        {
            btn.TabStop = false; // This means that the user can't select buttons by pressing tab
            btn.FlatStyle = FlatStyle.Flat; // removing 3D effects from the button with flat styling
            btn.FlatAppearance.BorderSize = 0; // removing border
            btn.BackColor = this.boardColor; // Making the button transparent with the form.
            return btn;
        }

        // render all stones from the 2d array in the Board class
        public void renderStones()
        {
            for (int row = 0; row < this.board.getSize(); row ++)
            {
                for (int col = 0; col < this.board.getSize(); col++)
                {
                    if (this.board.getBoard()[row, col] == 0)
                    {
                        this.boardBtns[row, col].BackColor = this.boardColor;
                    } else if (this.board.getBoard()[row, col] == 1)
                    {
                        this.boardBtns[row, col].BackColor = this.blackStone;
                    } else if (this.board.getBoard()[row, col] == 2)
                    {
                        this.boardBtns[row, col].BackColor = this.whiteStone;
                    }
                }
            }

            if (this.board.getPlayer() == 1)
            {
                this.playerText.Text = "Black to Play";
            } else
            {
                this.playerText.Text = "White to Play";
            }

            this.blackPrisoners.Text = "Black Prisoners: " + this.board.getBlackPrisoners().Count.ToString();
            this.whitePrisoners.Text = "White Prisoners: " + this.board.getWhitePrisoners().Count.ToString();
        }

        private void illegalMove()
        {
            // Creating a message box indicate illegal move
            DialogResult illegalMove;
            illegalMove = MessageBox.Show("Last move was Illegal!!!", "Illegal Move!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        // Function to play a sound effect on button press.
        private void clickSound()
        {
            SoundPlayer stoneSound = new SoundPlayer("../../assets/stone-dropping.wav");
            stoneSound.Play();
        }

        // on click event handler, deals with placing stones on the board
        private void btnEvent_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int row = Int32.Parse(btn.Tag.ToString().Split()[0]);
            int col = Int32.Parse(btn.Tag.ToString().Split()[1]);
            //Console.WriteLine("Button " + row.ToString() + "," + col.ToString() + " was pressed");
            bool isLegal = this.board.move(row, col, this.gameBoard);
            if (!isLegal)
            {
                illegalMove();
            }
            else
            {
                clickSound();
            }
            renderStones();
            // Playing a sound effect on click.
        }

        private void passBtn_Click(object sender, EventArgs e)
        {
            this.board.move(-1, -1, this.gameBoard);
        }

        private void resignBtn_Click(object sender, EventArgs e)
        {
            this.board.move(-2, -2, this.gameBoard);
        }
    }
}