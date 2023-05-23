namespace Tetris
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GameButton = new System.Windows.Forms.Button();
            this.Scores = new System.Windows.Forms.Label();
            this.ScoresCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GameButton
            // 
            this.GameButton.Location = new System.Drawing.Point(619, 63);
            this.GameButton.Name = "GameButton";
            this.GameButton.Size = new System.Drawing.Size(75, 23);
            this.GameButton.TabIndex = 0;
            this.GameButton.Text = "Играть";
            this.GameButton.UseVisualStyleBackColor = true;
            this.GameButton.Click += new System.EventHandler(this.StartGameButton);
            // 
            // Scores
            // 
            this.Scores.AutoSize = true;
            this.Scores.Enabled = false;
            this.Scores.Location = new System.Drawing.Point(637, 104);
            this.Scores.Name = "Scores";
            this.Scores.Size = new System.Drawing.Size(33, 15);
            this.Scores.TabIndex = 1;
            this.Scores.Text = "Счет";
            // 
            // ScoresCount
            // 
            this.ScoresCount.AutoSize = true;
            this.ScoresCount.Location = new System.Drawing.Point(648, 131);
            this.ScoresCount.Name = "ScoresCount";
            this.ScoresCount.Size = new System.Drawing.Size(13, 15);
            this.ScoresCount.TabIndex = 2;
            this.ScoresCount.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ScoresCount);
            this.Controls.Add(this.Scores);
            this.Controls.Add(this.GameButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button GameButton;
        private Label Scores;
        private Label ScoresCount;
    }
}