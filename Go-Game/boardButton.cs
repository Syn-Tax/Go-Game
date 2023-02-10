
/*
    Alex Glen  
    Lewis Simmonds
    Oscar Morris
    AC22005
    Grid Game
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
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

        private Image point;
        private Image starPoint;
        private Image topPoint;
        private Image leftPoint;
        private Image rghtPoint;
        private Image btmPoint;
        private Image topLeftPoint;
        private Image btmLeftPoint;
        private Image topRghtPoint;
        private Image btmRghtPoint;

        private Image blackStone;
        private Image whiteStone;

        public boardButton(Board board, GameBoard gameboard)
        {
            this.board = board;
            this.gameBoard = gameboard;
            this.boardBtns = new Button[this.board.getSize(), this.board.getSize()];

            // load images
            this.point = new Bitmap("../../assets/point.png");
            this.starPoint = new Bitmap("../../assets/star point.png");
            // edge points
            this.topPoint = new Bitmap("../../assets/top.png");
            this.leftPoint = new Bitmap("../../assets/left.png");
            this.rghtPoint = new Bitmap("../../assets/rght.png");
            this.btmPoint = new Bitmap("../../assets/btm.png");
            // corner points
            this.topLeftPoint = new Bitmap("../../assets/topLeft.png");
            this.btmLeftPoint = new Bitmap("../../assets/btmLeft.png");
            this.topRghtPoint = new Bitmap("../../assets/topRght.png");
            this.btmRghtPoint = new Bitmap("../../assets/btmRght.png");

            this.blackStone = new Bitmap("../../assets/black.png");
            this.whiteStone = new Bitmap("../../assets/white.png");
        }

        public void setBoard(Board board)
        {
            this.board = board;
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
                    double h = (double)this.gameBoard.Height;
                    int initx = (this.gameBoard.Width - (this.gameBoard.Height - 75)) / 2;
                    this.boardBtns[row, col].SetBounds((int)(34.5 * col + (int)(initx + h*0.08)), (int)(34.5 * row + (int)(h*0.1225)), (int)(h*0.05), (int)(h*0.05));

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
            this.passBtn.SetBounds((int)(this.gameBoard.Width * 0.8), (int)(this.gameBoard.Height * 0.17), 100, 50);
            this.passBtn.Text = "Pass";
            this.passBtn.Click += new EventHandler(this.passBtn_Click);
            this.gameBoard.addObject(this.passBtn);

            this.resignBtn = new Button();
            this.resignBtn.SetBounds((int)(this.gameBoard.Width * 0.8), (int)(this.gameBoard.Height * 0.3), 100, 50);
            this.resignBtn.Text = "Resign";
            this.resignBtn.Click += new EventHandler(this.resignBtn_Click);
            this.gameBoard.addObject(this.resignBtn);

            // create current player label
            this.playerText = new Label();
            this.playerText.SetBounds((int)(this.gameBoard.Width * 0.8), (int)(this.gameBoard.Height * 0.1), 100, 50);
            this.playerText.Text = "Black to Play";
            this.gameBoard.addObject(this.playerText);

            // create prisoner labels
            this.blackPrisoners = new Label();
            this.blackPrisoners.SetBounds((int)(this.gameBoard.Width * 0.8), (int)(this.gameBoard.Height * 0.5), 100, 25);
            this.blackPrisoners.Text = "Black Prisoners: 0";
            this.gameBoard.addObject(this.blackPrisoners);

            this.whitePrisoners = new Label();
            this.whitePrisoners.SetBounds((int)(this.gameBoard.Width * 0.8), (int)(this.gameBoard.Height * 0.55), 100, 25);
            this.whitePrisoners.Text = "White Prisoners: 0";
            this.gameBoard.addObject(this.whitePrisoners);


            this.renderStones();
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
                    double h = (double)this.gameBoard.Height;
                    int initx = (this.gameBoard.Width - (this.gameBoard.Height - 75)) / 2;
                    int inity = 20;
                    //this.boardBtns[row, col].SetBounds((int)(34.5 * col + (int)(initx + h*0.08)), (int)(34.5 * row + (int)(h*0.1225)), (int)(h*0.05), (int)(h*0.05));
                    this.boardBtns[row, col].Size = new Size((int)(h * 0.059), (int)(h * 0.059));
                    this.boardBtns[row, col].Location = new Point((int)(34.5 * col + (int)(initx + h * 0.076)), (int)(34.5 * row + (int)(inity + h * 0.076)));
                    this.boardBtns[row, col].BackgroundImageLayout = ImageLayout.Stretch;
                    if (this.board.getBoard()[row, col] == 0)
                    {
                        // star points first
                        if ((row == 2 && col == 2) || (row == 6 && col == 6) || (row == 2 && col == 6) || (row == 6 && col == 2) || (row == 4 && col == 4))
                        {
                            this.boardBtns[row, col].BackgroundImage = this.starPoint;
                        }
                        // then corner points
                        else if (row == 0 && col == 0)
                        {
                            this.boardBtns[row, col].BackgroundImage = this.topLeftPoint;
                        } else if (row == 8 && col == 8)
                        {
                            this.boardBtns[row, col].BackgroundImage = this.btmRghtPoint;
                        } else if (row == 0 && col == 8)
                        {
                            this.boardBtns[row, col].BackgroundImage = this.topRghtPoint;
                        } else if (row == 8 && col == 0)
                        {
                            this.boardBtns[row, col].BackgroundImage = this.btmLeftPoint;
                        }
                        // then edge points
                        else if (row == 0)
                        {
                            this.boardBtns[row, col].BackgroundImage = this.topPoint;
                        } else if (row == 8)
                        {
                            this.boardBtns[row, col].BackgroundImage = this.btmPoint;
                        } else if (col == 0)
                        {
                            this.boardBtns[row, col].BackgroundImage = this.leftPoint;
                        } else if (col == 8)
                        {
                            this.boardBtns[row, col].BackgroundImage = this.rghtPoint;
                        }
                        // then everything else
                        else
                        {
                            this.boardBtns[row, col].BackgroundImage = this.point;
                        }
                    } else if (this.board.getBoard()[row, col] == 1)
                    {
                        this.boardBtns[row, col].BackgroundImage = this.blackStone;
                    } else if (this.board.getBoard()[row, col] == 2)
                    {
                        this.boardBtns[row, col].BackgroundImage = this.whiteStone;
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

            // set correct bounds
            this.passBtn.SetBounds((int)(this.gameBoard.Width * 0.8), (int)(this.gameBoard.Height * 0.17), 100, 50);
            this.resignBtn.SetBounds((int)(this.gameBoard.Width * 0.8), (int)(this.gameBoard.Height * 0.3), 100, 50);
            this.playerText.SetBounds((int)(this.gameBoard.Width * 0.8), (int)(this.gameBoard.Height * 0.1), 100, 50);
            this.blackPrisoners.SetBounds((int)(this.gameBoard.Width * 0.8), (int)(this.gameBoard.Height * 0.5), 100, 25);
            this.whitePrisoners.SetBounds((int)(this.gameBoard.Width * 0.8), (int)(this.gameBoard.Height * 0.55), 100, 25);
        }

        // https://stackoverflow.com/questions/2163829/how-do-i-rotate-a-picture-in-winforms
        private Image rotateImage(Image img, int degrees)
        {
            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(img);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)img.Width / 2, (float)img.Height / 2);

            //now rotate the image
            gfx.RotateTransform(degrees);

            gfx.TranslateTransform(-(float)img.Width / 2, -(float)img.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new System.Drawing.Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return img;
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
            renderStones();
        }

        private void resignBtn_Click(object sender, EventArgs e)
        {
            this.board.move(-2, -2, this.gameBoard);
            renderStones();
        }
    }
}