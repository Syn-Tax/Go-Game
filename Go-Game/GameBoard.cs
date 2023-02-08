using System;
using System.Drawing;
using System.Windows.Forms;

namespace GoGame
{
    public partial class GameBoard : Form
    {
        private PictureBox gridPictureBox;

        public GameBoard()
        {
            InitializeComponent();
            mainMenuPanel.Visible = true;
            optionMenuPanel.Visible = false;
            gameBoardPanel.Visible = false;
            testMoves();
            createGrid();
        }

        private void testMoves()
        {
            Board b = new Board(9, 3.5f);
            b.move(4, 4, this);
            b.move(2, 4, this);
            b.move(3, 5, this);
            b.move(6, 4, this);
            b.move(6, 3, this);
            b.move(5, 3, this);
            b.move(6, 2, this);
            b.move(4, 3, this);
            b.move(5, 4, this);
            b.move(6, 5, this);
            b.move(3, 3, this);
            b.move(3, 4, this);
            b.move(4, 2, this);
            b.move(4, 5, this);
            b.move(5, 2, this);
            b.move(3, 6, this);
            b.move(7, 4, this);
            b.move(7, 5, this);
            b.move(8, 4, this);
            b.move(8, 5, this);
            b.move(7, 3, this);
            b.move(2, 3, this);
            b.move(5, 5, this);
            b.move(5, 6, this);
            b.move(4, 6, this);
            b.move(4, 7, this);
            b.move(4, 5, this);
            b.move(6, 7, this);
            b.move(3, 7, this);
            b.move(2, 6, this);
            b.move(2, 5, this);
            b.move(1, 5, this);
            b.move(2, 7, this);
            b.move(1, 6, this);
            b.move(1, 7, this);
            b.move(3, 2, this);
            b.move(4, 3, this);
            b.move(3, 1, this);
            b.move(4, 1, this);
            b.move(4, 0, this);
            b.move(5, 0, this);
            b.move(3, 0, this);
            b.move(1, 2, this);
            b.move(2, 2, this);
            b.move(1, 3, this);
            b.move(1, 4, this);
            b.move(1, 1, this);
            b.move(3, 8, this);
            b.move(0, 6, this);
            b.move(2, 8, this);
            b.move(0, 7, this);
            b.move(1, 8, this);
            b.move(2, 1, this);
            b.move(0, 5, this);
            b.move(2, 0, this);
            b.move(0, 8, this);
            b.move(0, 3, this);
            b.move(0, 1, this);
            b.move(-1, -1, this);
            b.move(-1, -1, this);
        }

        private void playButton_Click(object sender, EventArgs e) // handler for when the playButton is clicked
        {
            // bring the gameBoardPanel to the front and make it visible
            gameBoardPanel.BringToFront();
            gameBoardPanel.Visible = true;
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
            adjustButtons();
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

        private void adjustButtons()
        {
            // calculate the height of each button
            int buttonHeight = mainMenuPanel.Height / 7;

            // calculate the font size based on the button height
            int fontSize = buttonHeight / 3;

            // calculate the total height of all buttons
            int totalButtonHeight = buttonHeight * 3;

            // calculate the starting y position of the first button
            int y = mainMenuPanel.Height / 2 - totalButtonHeight / 2;

            // resize and position the playButton
            playButton.Size = new Size(mainMenuPanel.Width / 3, buttonHeight);
            playButton.Location = new Point(mainMenuPanel.Width / 2 - playButton.Width / 2, y - 10);
            playButton.Font = new Font(playButton.Font.FontFamily, fontSize);

            // update the y position for the next button
            y += buttonHeight;

            // resize and position the optionsButton
            optionsButton.Size = new Size(mainMenuPanel.Width / 3, buttonHeight);
            optionsButton.Location = new Point(mainMenuPanel.Width / 2 - optionsButton.Width / 2, y);
            optionsButton.Font = new Font(optionsButton.Font.FontFamily, fontSize);

            // update the y position for the next button
            y += buttonHeight;

            // resize and position the quitButton
            quitButton.Size = new Size(mainMenuPanel.Width / 3, buttonHeight);
            quitButton.Location = new Point(mainMenuPanel.Width / 2 - quitButton.Width / 2, y + 10);
            quitButton.Font = new Font(quitButton.Font.FontFamily, fontSize);
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
    }
}
