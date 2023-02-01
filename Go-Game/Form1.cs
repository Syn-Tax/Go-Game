using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Go_Game
{
    public partial class goGame : Form
    {
        public goGame()
        {
            InitializeComponent();
            createGrid();
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
            Button[,] boardBtns = new Button[boardSize, boardSize]; // Creating the 2D Array of Buttons
            renderGrid(); // Drawing the Go board image to the form.
            for (int x=0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    // Creating the buttons
                    boardBtns[x, y] = new Button();
                    // Buttons need to be aligned with the form, so that they are aligned with the board correctly.
                    boardBtns[x, y].SetBounds(250+(37*x), 70+(38*y), 25, 25);
                    makeTransparentBtn(boardBtns[x, y]);
                    boardBtns[x, y].Click += new EventHandler(this.btnEvent_Click); // Adding the click handler to the button
                    Controls.Add(boardBtns[x, y]); // Adding the button the form
                }
            }
            // Rendering the grid again, so that buttons are place on top.
            renderGrid();
        }

        public Button makeTransparentBtn(Button btn)
        {
            btn.TabStop = false; // This means that the user can't select buttons by pressing tab
            btn.FlatStyle = FlatStyle.Flat; // removing 3D effects from the button with flat styling
            btn.FlatAppearance.BorderSize = 0; // removing border
            btn.BackColor = Color.Transparent; // Making the button transparent with the form.
            return btn;
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

        void btnEvent_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Button has been clicked");


        }
    }
}
