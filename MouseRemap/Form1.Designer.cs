namespace MouseRemap
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
            mousePressToClickLeft = new CheckBox();
            mousePressToClickRight = new CheckBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // mousePressToClickLeft
            // 
            mousePressToClickLeft.AutoSize = true;
            mousePressToClickLeft.Location = new Point(12, 42);
            mousePressToClickLeft.Name = "mousePressToClickLeft";
            mousePressToClickLeft.Size = new Size(172, 19);
            mousePressToClickLeft.TabIndex = 0;
            mousePressToClickLeft.Text = "Mouse Left Press does Click";
            mousePressToClickLeft.UseVisualStyleBackColor = true;
            mousePressToClickLeft.CheckedChanged += mousePressToClickLeft_CheckedChanged;
            // 
            // mousePressToClickRight
            // 
            mousePressToClickRight.AutoSize = true;
            mousePressToClickRight.Location = new Point(12, 67);
            mousePressToClickRight.Name = "mousePressToClickRight";
            mousePressToClickRight.Size = new Size(180, 19);
            mousePressToClickRight.TabIndex = 1;
            mousePressToClickRight.Text = "Mouse Right Press does Click";
            mousePressToClickRight.UseVisualStyleBackColor = true;
            mousePressToClickRight.CheckedChanged += mousePressToClickRight_CheckedChanged;
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(180, 23);
            button1.TabIndex = 2;
            button1.Text = "Disable/Enable prev (LCTRL+0)";
            button1.UseVisualStyleBackColor = true;
            button1.Click += onClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(256, 98);
            Controls.Add(button1);
            Controls.Add(mousePressToClickRight);
            Controls.Add(mousePressToClickLeft);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public CheckBox mousePressToClickLeft;
        public CheckBox mousePressToClickRight;
        public Button button1;
    }
}