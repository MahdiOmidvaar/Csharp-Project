namespace calculatorWinForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnBackSpace = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.btnMultiple = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btnDivide = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btnPoint = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btnEqual = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.btnPower = new System.Windows.Forms.Button();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnBackSpace);
            this.panel1.Controls.Add(this.button13);
            this.panel1.Controls.Add(this.button14);
            this.panel1.Controls.Add(this.btnMultiple);
            this.panel1.Controls.Add(this.btn9);
            this.panel1.Controls.Add(this.btn7);
            this.panel1.Controls.Add(this.btn8);
            this.panel1.Controls.Add(this.btnDivide);
            this.panel1.Controls.Add(this.btn6);
            this.panel1.Controls.Add(this.btn4);
            this.panel1.Controls.Add(this.btn5);
            this.panel1.Controls.Add(this.btnPoint);
            this.panel1.Controls.Add(this.btn3);
            this.panel1.Controls.Add(this.btn1);
            this.panel1.Controls.Add(this.btn2);
            this.panel1.Controls.Add(this.btnEqual);
            this.panel1.Controls.Add(this.btn0);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(12, 155);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 381);
            this.panel1.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(153, 51);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(134, 59);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnBackSpace
            // 
            this.btnBackSpace.Location = new System.Drawing.Point(13, 51);
            this.btnBackSpace.Name = "btnBackSpace";
            this.btnBackSpace.Size = new System.Drawing.Size(134, 59);
            this.btnBackSpace.TabIndex = 16;
            this.btnBackSpace.Text = "<";
            this.btnBackSpace.UseVisualStyleBackColor = true;
            this.btnBackSpace.Click += new System.EventHandler(this.btnBackSpace_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(83, 116);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(64, 59);
            this.button13.TabIndex = 15;
            this.button13.Text = "+";
            this.button13.UseVisualStyleBackColor = true;
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(153, 116);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(64, 59);
            this.button14.TabIndex = 14;
            this.button14.Text = "-";
            this.button14.UseVisualStyleBackColor = true;
            // 
            // btnMultiple
            // 
            this.btnMultiple.Location = new System.Drawing.Point(223, 116);
            this.btnMultiple.Name = "btnMultiple";
            this.btnMultiple.Size = new System.Drawing.Size(64, 59);
            this.btnMultiple.TabIndex = 13;
            this.btnMultiple.Text = "*";
            this.btnMultiple.UseVisualStyleBackColor = true;
            // 
            // btn9
            // 
            this.btn9.Location = new System.Drawing.Point(13, 116);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(64, 59);
            this.btn9.TabIndex = 12;
            this.btn9.Text = "9";
            this.btn9.UseVisualStyleBackColor = true;
            this.btn9.MouseClick += new System.Windows.Forms.MouseEventHandler(this.numbers);
            // 
            // btn7
            // 
            this.btn7.Location = new System.Drawing.Point(83, 181);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(64, 59);
            this.btn7.TabIndex = 11;
            this.btn7.Text = "7";
            this.btn7.UseVisualStyleBackColor = true;
            this.btn7.MouseClick += new System.Windows.Forms.MouseEventHandler(this.numbers);
            // 
            // btn8
            // 
            this.btn8.Location = new System.Drawing.Point(153, 181);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(64, 59);
            this.btn8.TabIndex = 10;
            this.btn8.Text = "8";
            this.btn8.UseVisualStyleBackColor = true;
            this.btn8.MouseClick += new System.Windows.Forms.MouseEventHandler(this.numbers);
            // 
            // btnDivide
            // 
            this.btnDivide.Location = new System.Drawing.Point(223, 181);
            this.btnDivide.Name = "btnDivide";
            this.btnDivide.Size = new System.Drawing.Size(64, 59);
            this.btnDivide.TabIndex = 9;
            this.btnDivide.Text = "/";
            this.btnDivide.UseVisualStyleBackColor = true;
            // 
            // btn6
            // 
            this.btn6.Location = new System.Drawing.Point(13, 181);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(64, 59);
            this.btn6.TabIndex = 8;
            this.btn6.Text = "6";
            this.btn6.UseVisualStyleBackColor = true;
            this.btn6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.numbers);
            // 
            // btn4
            // 
            this.btn4.Location = new System.Drawing.Point(83, 246);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(64, 59);
            this.btn4.TabIndex = 7;
            this.btn4.Text = "4";
            this.btn4.UseVisualStyleBackColor = true;
            this.btn4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.numbers);
            // 
            // btn5
            // 
            this.btn5.Location = new System.Drawing.Point(153, 246);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(64, 59);
            this.btn5.TabIndex = 6;
            this.btn5.Text = "5";
            this.btn5.UseVisualStyleBackColor = true;
            this.btn5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.numbers);
            // 
            // btnPoint
            // 
            this.btnPoint.Location = new System.Drawing.Point(223, 246);
            this.btnPoint.Name = "btnPoint";
            this.btnPoint.Size = new System.Drawing.Size(64, 59);
            this.btnPoint.TabIndex = 5;
            this.btnPoint.Text = ".";
            this.btnPoint.UseVisualStyleBackColor = true;
            this.btnPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.numbers);
            // 
            // btn3
            // 
            this.btn3.Location = new System.Drawing.Point(13, 246);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(64, 59);
            this.btn3.TabIndex = 4;
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.numbers);
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(83, 311);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(64, 59);
            this.btn1.TabIndex = 3;
            this.btn1.Text = "1";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.numbers);
            // 
            // btn2
            // 
            this.btn2.Location = new System.Drawing.Point(153, 311);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(64, 59);
            this.btn2.TabIndex = 2;
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.numbers);
            // 
            // btnEqual
            // 
            this.btnEqual.Location = new System.Drawing.Point(223, 311);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(64, 59);
            this.btnEqual.TabIndex = 1;
            this.btnEqual.Text = "=";
            this.btnEqual.UseVisualStyleBackColor = true;
            this.btnEqual.Click += new System.EventHandler(this.btnEqual_Click);
            // 
            // btn0
            // 
            this.btn0.Location = new System.Drawing.Point(13, 311);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(64, 59);
            this.btn0.TabIndex = 0;
            this.btn0.Text = "0";
            this.btn0.UseVisualStyleBackColor = true;
            this.btn0.MouseClick += new System.Windows.Forms.MouseEventHandler(this.numbers);
            // 
            // btnPower
            // 
            this.btnPower.Location = new System.Drawing.Point(25, 115);
            this.btnPower.Name = "btnPower";
            this.btnPower.Size = new System.Drawing.Size(134, 34);
            this.btnPower.TabIndex = 18;
            this.btnPower.Text = "On";
            this.btnPower.UseVisualStyleBackColor = true;
            this.btnPower.Click += new System.EventHandler(this.btnPower_Click_1);
            // 
            // txtDisplay
            // 
            this.txtDisplay.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtDisplay.Location = new System.Drawing.Point(12, 56);
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.ReadOnly = true;
            this.txtDisplay.Size = new System.Drawing.Size(300, 27);
            this.txtDisplay.TabIndex = 19;
            this.txtDisplay.TextChanged += new System.EventHandler(this.txtDisplay_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 546);
            this.Controls.Add(this.txtDisplay);
            this.Controls.Add(this.btnPower);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Calculator";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Button btnClear;
        private Button btnBackSpace;
        private Button button13;
        private Button button14;
        private Button btnMultiple;
        private Button btn9;
        private Button btn7;
        private Button btn8;
        private Button btnDivide;
        private Button btn6;
        private Button btn4;
        private Button btn5;
        private Button btnPoint;
        private Button btn3;
        private Button btn1;
        private Button btn2;
        private Button btnEqual;
        private Button btn0;
        private Button btnPower;
        private TextBox txtDisplay;
    }
}