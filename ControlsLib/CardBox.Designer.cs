﻿namespace CardBox
{
    partial class CardBox
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbMyPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMyPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMyPictureBox
            // 
            this.pbMyPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMyPictureBox.Location = new System.Drawing.Point(0, 0);
            this.pbMyPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.pbMyPictureBox.Name = "pbMyPictureBox";
            this.pbMyPictureBox.Size = new System.Drawing.Size(100, 132);
            this.pbMyPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMyPictureBox.TabIndex = 0;
            this.pbMyPictureBox.TabStop = false;
            this.pbMyPictureBox.Click += new System.EventHandler(this.pbMyPictureBox_Click);
            this.pbMyPictureBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.pbMyPictureBox_DragDrop);
            this.pbMyPictureBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.pbMyPictureBox_DragEnter);
            this.pbMyPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMyPictureBox_MouseDown);
            this.pbMyPictureBox.MouseEnter += new System.EventHandler(this.pbMyPictureBox_MouseEnter);
            this.pbMyPictureBox.MouseLeave += new System.EventHandler(this.pbMyPictureBox_MouseLeave);
            // 
            // CardBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbMyPictureBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CardBox";
            this.Size = new System.Drawing.Size(100, 132);
            this.Load += new System.EventHandler(this.CardBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbMyPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMyPictureBox;
    }
}
