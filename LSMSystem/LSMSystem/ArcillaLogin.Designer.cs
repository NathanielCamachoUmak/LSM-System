namespace LSMSystem
{
    partial class ArcillaLogin
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArcillaLogin));
            button1 = new Button();
            unLabel = new Label();
            pwLabel = new Label();
            unBox = new TextBox();
            pwBox = new TextBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(335, 256);
            button1.Name = "button1";
            button1.Size = new Size(136, 36);
            button1.TabIndex = 0;
            button1.Text = "LOG IN";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // unLabel
            // 
            unLabel.AutoSize = true;
            unLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            unLabel.Location = new Point(191, 162);
            unLabel.Name = "unLabel";
            unLabel.Size = new Size(95, 21);
            unLabel.TabIndex = 1;
            unLabel.Text = "USERNAME:";
            // 
            // pwLabel
            // 
            pwLabel.AutoSize = true;
            pwLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pwLabel.Location = new Point(189, 205);
            pwLabel.Name = "pwLabel";
            pwLabel.Size = new Size(97, 21);
            pwLabel.TabIndex = 2;
            pwLabel.Text = "PASSWORD:";
            // 
            // unBox
            // 
            unBox.Location = new Point(294, 162);
            unBox.Name = "unBox";
            unBox.Size = new Size(298, 23);
            unBox.TabIndex = 3;
            // 
            // pwBox
            // 
            pwBox.Location = new Point(294, 205);
            pwBox.Name = "pwBox";
            pwBox.Size = new Size(298, 23);
            pwBox.TabIndex = 4;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Center;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 450);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pwBox);
            Controls.Add(unBox);
            Controls.Add(pwLabel);
            Controls.Add(unLabel);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label unLabel;
        private Label pwLabel;
        private TextBox unBox;
        private TextBox pwBox;
        private PictureBox pictureBox1;
    }
}