using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoGame
{
    public partial class GameBoard : Form
    {
        public GameBoard()
        {
            InitializeComponent();
            createGrid();
            Board b = new Board(9);
            b.move(1, 1, false);
            b.move(1, 2, true);

            b.move(1, 0, false);
            b.move(0, 1, false);
            b.move(0, 2, false);
            b.move(1, 3, false);
            b.move(2, 2, false);
            b.printBoard();

            b.move(2, 1, true);
            b.printBoard();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            renderGrid();
            //Console.WriteLine(this.Height);
            //MessageBox.Show("you resized the window!!!");
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
            // remove all picture boxes - NOTE: All things with tag "grid" will be re-rendered
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && control.Tag.ToString() == "grid")
                {
                    this.Controls.Remove(control);
                }
            }
            // Writing the image to the form. Note that the render occurs from the exe (i.e. bin\Debug)
            renderImage("../../assets/goBoard.png", (this.Width-(this.Height-75))/2, 30, this.Height-75, this.Height-75, "grid");

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
            this.Controls.Add(pb);
            return pb;
        }
    }
}
