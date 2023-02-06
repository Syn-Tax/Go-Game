using System.Windows.Forms;

namespace GoGame
{
    partial class GameBoard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Creating function to add board buttons to the form
        public void addButton(Button btn)
        {
            this.Controls.Add(btn);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameBoard));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuPanel = new System.Windows.Forms.Panel();
            this.gameBoardPanel = new System.Windows.Forms.Panel();
            this.gameBoardBackButton = new System.Windows.Forms.Button();
            this.optionMenuPanel = new System.Windows.Forms.Panel();
            this.optionsBackButton = new System.Windows.Forms.Button();
            this.nothingToSeeHere = new System.Windows.Forms.Label();
            this.quitButton = new System.Windows.Forms.Button();
            this.optionsButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.mainMenuPanel.SuspendLayout();
            this.gameBoardPanel.SuspendLayout();
            this.optionMenuPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // mainMenuPanel
            // 
            this.mainMenuPanel.Controls.Add(this.gameBoardPanel);
            this.mainMenuPanel.Controls.Add(this.optionMenuPanel);
            this.mainMenuPanel.Controls.Add(this.quitButton);
            this.mainMenuPanel.Controls.Add(this.optionsButton);
            this.mainMenuPanel.Controls.Add(this.playButton);
            this.mainMenuPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainMenuPanel.Location = new System.Drawing.Point(0, 24);
            this.mainMenuPanel.Name = "mainMenuPanel";
            this.mainMenuPanel.Size = new System.Drawing.Size(800, 426);
            this.mainMenuPanel.TabIndex = 1;
            // 
            // gameBoardPanel
            // 
            this.gameBoardPanel.Controls.Add(this.gameBoardBackButton);
            this.gameBoardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameBoardPanel.Location = new System.Drawing.Point(0, 0);
            this.gameBoardPanel.Name = "gameBoardPanel";
            this.gameBoardPanel.Size = new System.Drawing.Size(800, 426);
            this.gameBoardPanel.TabIndex = 3;
            // 
            // gameBoardBackButton
            // 
            this.gameBoardBackButton.Font = new System.Drawing.Font("Fugaz One", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameBoardBackButton.Location = new System.Drawing.Point(22, 22);
            this.gameBoardBackButton.Name = "gameBoardBackButton";
            this.gameBoardBackButton.Size = new System.Drawing.Size(99, 51);
            this.gameBoardBackButton.TabIndex = 0;
            this.gameBoardBackButton.Text = "Back";
            this.gameBoardBackButton.UseVisualStyleBackColor = true;
            this.gameBoardBackButton.Click += new System.EventHandler(this.gameBoardBackButton_Click);
            // 
            // optionMenuPanel
            // 
            this.optionMenuPanel.Controls.Add(this.optionsBackButton);
            this.optionMenuPanel.Controls.Add(this.nothingToSeeHere);
            this.optionMenuPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionMenuPanel.Location = new System.Drawing.Point(0, 0);
            this.optionMenuPanel.Name = "optionMenuPanel";
            this.optionMenuPanel.Size = new System.Drawing.Size(800, 426);
            this.optionMenuPanel.TabIndex = 2;
            // 
            // optionsBackButton
            // 
            this.optionsBackButton.Font = new System.Drawing.Font("Fugaz One", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionsBackButton.Location = new System.Drawing.Point(22, 22);
            this.optionsBackButton.Name = "optionsBackButton";
            this.optionsBackButton.Size = new System.Drawing.Size(99, 51);
            this.optionsBackButton.TabIndex = 1;
            this.optionsBackButton.Text = "Back";
            this.optionsBackButton.UseVisualStyleBackColor = true;
            this.optionsBackButton.Click += new System.EventHandler(this.optionsBackButton_Click);
            // 
            // nothingToSeeHere
            // 
            this.nothingToSeeHere.AutoSize = true;
            this.nothingToSeeHere.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nothingToSeeHere.Location = new System.Drawing.Point(266, 172);
            this.nothingToSeeHere.Name = "nothingToSeeHere";
            this.nothingToSeeHere.Size = new System.Drawing.Size(244, 25);
            this.nothingToSeeHere.TabIndex = 0;
            this.nothingToSeeHere.Text = "there is nothing here yet";
            // 
            // quitButton
            // 
            this.quitButton.Font = new System.Drawing.Font("Fugaz One", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quitButton.Location = new System.Drawing.Point(250, 296);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(279, 80);
            this.quitButton.TabIndex = 2;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // optionsButton
            // 
            this.optionsButton.Font = new System.Drawing.Font("Fugaz One", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionsButton.Location = new System.Drawing.Point(250, 172);
            this.optionsButton.Name = "optionsButton";
            this.optionsButton.Size = new System.Drawing.Size(279, 80);
            this.optionsButton.TabIndex = 1;
            this.optionsButton.Text = "Options";
            this.optionsButton.UseVisualStyleBackColor = true;
            this.optionsButton.Click += new System.EventHandler(this.optionsButton_Click);
            // 
            // playButton
            // 
            this.playButton.Font = new System.Drawing.Font("Fugaz One", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playButton.Location = new System.Drawing.Point(250, 45);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(279, 80);
            this.playButton.TabIndex = 0;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // GameBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainMenuPanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Go";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainMenuPanel.ResumeLayout(false);
            this.gameBoardPanel.ResumeLayout(false);
            this.optionMenuPanel.ResumeLayout(false);
            this.optionMenuPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel mainMenuPanel;
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.Button optionsButton;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Panel gameBoardPanel;
        private System.Windows.Forms.Button gameBoardBackButton;
        private System.Windows.Forms.Panel optionMenuPanel;
        private System.Windows.Forms.Button optionsBackButton;
        private System.Windows.Forms.Label nothingToSeeHere;
    }
}

