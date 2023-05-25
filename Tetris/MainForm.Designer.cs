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
            this.label1 = new System.Windows.Forms.Label();
            this.TimeField = new System.Windows.Forms.TextBox();
            this.WidthField = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.HeightField = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MaxScore = new System.Windows.Forms.Label();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(482, 386);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(268, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Время передвижения фигуры в миллисекундах";
            // 
            // TimeField
            // 
            this.TimeField.Location = new System.Drawing.Point(570, 404);
            this.TimeField.Name = "TimeField";
            this.TimeField.Size = new System.Drawing.Size(100, 23);
            this.TimeField.TabIndex = 4;
            this.TimeField.Text = "1500";
            this.TimeField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // WidthField
            // 
            this.WidthField.Location = new System.Drawing.Point(570, 354);
            this.WidthField.Name = "WidthField";
            this.WidthField.Size = new System.Drawing.Size(100, 23);
            this.WidthField.TabIndex = 6;
            this.WidthField.Text = "10";
            this.WidthField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(518, 336);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ширина игрового поля в квадратах";
            // 
            // HeightField
            // 
            this.HeightField.Location = new System.Drawing.Point(570, 304);
            this.HeightField.Name = "HeightField";
            this.HeightField.Size = new System.Drawing.Size(100, 23);
            this.HeightField.TabIndex = 8;
            this.HeightField.Text = "20";
            this.HeightField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(518, 286);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Высота игрового поля в квадратах";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(442, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Лучший результат";
            // 
            // MaxScore
            // 
            this.MaxScore.AutoSize = true;
            this.MaxScore.Location = new System.Drawing.Point(474, 31);
            this.MaxScore.Name = "MaxScore";
            this.MaxScore.Size = new System.Drawing.Size(13, 15);
            this.MaxScore.TabIndex = 10;
            this.MaxScore.Text = "0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 637);
            this.Controls.Add(this.MaxScore);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.HeightField);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.WidthField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TimeField);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ScoresCount);
            this.Controls.Add(this.Scores);
            this.Controls.Add(this.GameButton);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button GameButton;
        private Label Scores;
        private Label ScoresCount;
        private Label label1;
        private TextBox TimeField;
        private TextBox WidthField;
        private Label label2;
        private TextBox HeightField;
        private Label label3;
        private Label label4;
        private Label MaxScore;
    }
}