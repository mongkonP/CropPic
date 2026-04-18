namespace CropPic
{
    partial class Form1
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
            panel1 = new Panel();
            button2 = new Button();
            button4 = new Button();
            button3 = new Button();
            label1 = new Label();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            button5 = new Button();
            label2 = new Label();
            txtUp = new TextBox();
            txtDown = new TextBox();
            label3 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(txtDown);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtUp);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(949, 102);
            panel1.TabIndex = 0;
            // 
            // button2
            // 
            button2.Location = new Point(18, 39);
            button2.Name = "button2";
            button2.Size = new Size(158, 23);
            button2.TabIndex = 5;
            button2.Text = "Crop บน 0.05";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // button4
            // 
            button4.Location = new Point(182, 68);
            button4.Name = "button4";
            button4.Size = new Size(209, 23);
            button4.TabIndex = 4;
            button4.Text = "Crop บน 0.075ล่าง  0.125";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new Point(18, 68);
            button3.Name = "button3";
            button3.Size = new Size(158, 23);
            button3.TabIndex = 3;
            button3.Text = "Crop บน 0.11 ล่าง  0.118";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(209, 10);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 2;
            label1.Text = "Path:";
            // 
            // button1
            // 
            button1.Location = new Point(18, 10);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Open";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 102);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(949, 669);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // button5
            // 
            button5.Location = new Point(680, 68);
            button5.Name = "button5";
            button5.Size = new Size(89, 23);
            button5.TabIndex = 6;
            button5.Text = "Crop ";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(397, 72);
            label2.Name = "label2";
            label2.Size = new Size(24, 15);
            label2.TabIndex = 7;
            label2.Text = "บน:";
            // 
            // txtUp
            // 
            txtUp.Location = new Point(427, 68);
            txtUp.Name = "txtUp";
            txtUp.Size = new Size(100, 23);
            txtUp.TabIndex = 8;
            txtUp.Text = "0.11";
            // 
            // txtDown
            // 
            txtDown.Location = new Point(574, 68);
            txtDown.Name = "txtDown";
            txtDown.Size = new Size(100, 23);
            txtDown.TabIndex = 10;
            txtDown.Text = "0.125";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(544, 72);
            label3.Name = "label3";
            label3.Size = new Size(29, 15);
            label3.TabIndex = 9;
            label3.Text = "ล่าง :";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(949, 771);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Crop Pic";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private PictureBox pictureBox1;
        private Label label1;
        private Button button3;
        private Button button4;
        private Button button2;
        private TextBox txtDown;
        private Label label3;
        private TextBox txtUp;
        private Label label2;
        private Button button5;
    }
}
