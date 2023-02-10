using System;
using System.Drawing;
using System.Windows.Forms;

namespace GoGame
{
    public partial class GameBoard : Form
    {
        private PictureBox gridPictureBox;

        private Board board;

        private boardButton bb;

        public GameBoard()
        {
            InitializeComponent();
            mainMenuPanel.Visible = true;
            optionMenuPanel.Visible = false;
            gameBoardPanel.Visible = false;
            this.board = new Board(9, 5.5f);
            this.bb = new boardButton(this.board, this);
        }

        private void resetGame()
        {
            mainMenuPanel.Visible = true;
            optionMenuPanel.Visible = false;
            gameBoardPanel.Visible = false;
            initialiseBoard();
        }

        private void initialiseBoard()
        {
            gameBoardPanel.Controls.Clear();
            this.board = new Board(9, 5.5f);
            this.bb.setBoard(this.board);
            placeBtns();
            createGrid();
            initialiseTimer();
        }

        private void initialiseTimer()
        {

            Timer timer = new Timer();
            timer.Interval = 1000; // Timer will tick every second
            timer.Start();
            timer.Tick += new EventHandler(Timer_Tick);
            gameBoardPanel.Controls.Add(timerLabel);

        }

        private int elapsedTime = 0;

        private void Timer_Tick(object sender, EventArgs e)
        {
            elapsedTime++;
            timerLabel.Text = TimeSpan.FromSeconds(elapsedTime).ToString();
        }

        // Creating function to add objects to the form
        public void addObject(object btn)
        {
            gameBoardPanel.Controls.Add(btn as Control);

        }

        public void finishGame(int player, float score, bool resign=false)
        {
            string endGame;
            if (player == 1)
            {
                if (resign)
                {
                    endGame = "Black Won by Resignation";
                } else
                {
                    endGame = "Black Won by " + score.ToString() + " Points";
                }
            } else
            {
                if (resign)
                {
                    endGame = "White Won by Resignation";
                } else
                {
                    endGame = "White Won by " + score.ToString() + " Points";
                }
            }

            DialogResult result = MessageBox.Show(endGame + "\n\nIf this is the correct result, press Yes to end the game, if this is incorrect, press No to continue playing", "Game Over", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.resetGame();
            }
        }

        private void placeBtns()
        {
            // Defining an instance of board buttons, so that buttons can be added to the form.
            this.bb.createButtons();
            this.bb.renderStones();
        }

        private void playButton_Click(object sender, EventArgs e) // handler for when the playButton is clicked
        {
            // bring the gameBoardPanel to the front and make it visible
            gameBoardPanel.BringToFront();
            gameBoardPanel.Visible = true;
            this.initialiseBoard();
        }

        private void optionsButton_Click(object sender, EventArgs e) // handler for when the optionsButton is clicked
        {
            // bring the optionMenuPanel to the front and make it visible
            optionMenuPanel.BringToFront();
            optionMenuPanel.Visible = true;
        }

        private void quitButton_Click(object sender, EventArgs e) // handler for when the quitButton is clicked
        {
            // close the application
            this.Close();
        }

        private void gameBoardBackButton_Click(object sender, EventArgs e) // handler for when the backButton on the gameBoard is clicked
        {
            // bring the mainMenuPanel to the front and make the gameBoardPanel invisible
            mainMenuPanel.BringToFront();
            gameBoardPanel.Visible = false;
        }
        private void optionsBackButton_Click(object sender, EventArgs e) // handler for when the backButton on the optionMenuPanel is clicked
        {
            // bring the mainMenuPanel to the front and make the optionMenuPanel invisible
            mainMenuPanel.BringToFront();
            optionMenuPanel.Visible = false;
        }

        protected override void OnResize(EventArgs e) // event handler for whenever the form is resized
        {
            base.OnResize(e);
            renderGrid();
        }

        public void createGrid() // The create grid function is responsible for building the board.
        {
            int boardSize = 9; // For now, this is 9, eventually we can give the user options to pick 9x9, 13x13, 19x19
            Array[,] boardArray = new Array[boardSize, boardSize]; // Creating the 2D Array
            renderGrid(); // Drawing the Go board image to the form.
            // Now we need to link the 2D array to the image
            // Creating buttons

        }

        public void renderGrid() // The render grid function is responsible for loading the image
        {
            // if the gridPictireBox object isn't null, i.e, there is an image
            if (gridPictureBox != null)
            {
                // remove the current image from the panel
                gameBoardPanel.Controls.Remove(gridPictureBox);

                // set the picture box to null for the next render of the image
                gridPictureBox = null;
            }

            // render the image again using the new dimensions of the window
            gridPictureBox = renderImage("../../assets/goBoard.png", (this.Width - (this.Height - 75)) / 2, 20, this.Height - 110, this.Height - 110, "grid");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) // Click event handler for the strip menu.
        {
            // Creating a message box to display about information.
            DialogResult result;
            result = MessageBox.Show("Go. Alexander Glen, Lewis Simmonds, Oscar Morris. (c) 2023", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public PictureBox renderImage(string path, int x, int y, int w, int h, string tag) // The render image function handles writing any images.
        {
            PictureBox pb = new PictureBox();
            pb.ImageLocation = path;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Location = new Point(x, y);
            pb.Size = new Size(w, h);
            pb.Tag = tag;
            gameBoardPanel.Controls.Add(pb);
            return pb;
        }

        private void rulesToolStripMenuItem_Click(object sender, EventArgs e) // Click event handler for the rules in the strip menu.
        {
            DialogResult result;
            String ruleText;
            // Adding a description of the reles.
            ruleText = "The Basic rules of Go:\n\n1. Black makes the first move.\n2. A move consists of placing a single stone on an empty intersection.\n3. If an opposing coloured stone is surrounded in all adjacent intersections, then it is captured at taken off the board.\n4. Each player can pass their turn at any point, if both players pass, the game is over and the score is counted.\n5. Score is calculated from the territory (empty intersections) that each player has surrounded.\n6. Note that white has 5.5 bonus captures (komi), to offset the fact that they move second.";
            result = MessageBox.Show(ruleText, "Basic Go Rules", MessageBoxButtons.OK);
        }
    }
}
