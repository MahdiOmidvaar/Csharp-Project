namespace MyCoffeeProject
{
    partial class TShirtForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TShirtForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBoxTShirts = new System.Windows.Forms.ComboBox();
            this.lblSelected = new System.Windows.Forms.Label();
            this.listBoxSelected = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(307, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(360, 277);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // comboBoxTShirts
            // 
            this.comboBoxTShirts.FormattingEnabled = true;
            this.comboBoxTShirts.Location = new System.Drawing.Point(57, 409);
            this.comboBoxTShirts.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxTShirts.Name = "comboBoxTShirts";
            this.comboBoxTShirts.Size = new System.Drawing.Size(208, 24);
            this.comboBoxTShirts.TabIndex = 1;
            this.comboBoxTShirts.SelectedIndexChanged += new System.EventHandler(this.comboBoxTShirts_SelectedIndexChanged);
            // 
            // lblSelected
            // 
            this.lblSelected.AutoSize = true;
            this.lblSelected.Location = new System.Drawing.Point(359, 359);
            this.lblSelected.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(64, 16);
            this.lblSelected.TabIndex = 2;
            this.lblSelected.Text = "Selected:";
            this.lblSelected.Click += new System.EventHandler(this.lblSelected_Click);
            // 
            // listBoxSelected
            // 
            this.listBoxSelected.FormattingEnabled = true;
            this.listBoxSelected.ItemHeight = 16;
            this.listBoxSelected.Location = new System.Drawing.Point(363, 379);
            this.listBoxSelected.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxSelected.Name = "listBoxSelected";
            this.listBoxSelected.Size = new System.Drawing.Size(192, 148);
            this.listBoxSelected.TabIndex = 3;
            this.listBoxSelected.SelectedIndexChanged += new System.EventHandler(this.listBoxSelected_SelectedIndexChanged);
            // 
            // TShirtForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 620);
            this.Controls.Add(this.listBoxSelected);
            this.Controls.Add(this.lblSelected);
            this.Controls.Add(this.comboBoxTShirts);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TShirtForm";
            this.Text = "TShirtForm";
            this.Load += new System.EventHandler(this.TShirtForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBoxTShirts;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.ListBox listBoxSelected;
    }
}