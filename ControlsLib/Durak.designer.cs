
using System;

namespace Durak
{
    partial class frmGame
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
            CardLib.PlayingCard playingCard1 = new CardLib.PlayingCard();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGame));
            this.btnFlipCard = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblClickedState = new System.Windows.Forms.Label();
            this.lblFlippedState = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.pnlActiveCards = new System.Windows.Forms.Panel();
            this.pnlPlayerCards = new System.Windows.Forms.Panel();
            this.pnlComputerCards = new System.Windows.Forms.Panel();
            this.txtPlayHistory = new System.Windows.Forms.TextBox();
            this.cbxSuit = new System.Windows.Forms.ComboBox();
            this.cbxRank = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRules = new System.Windows.Forms.Button();
            this.cbxDeck = new CardBox.CardBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFlipCard
            // 
            this.btnFlipCard.Location = new System.Drawing.Point(212, 158);
            this.btnFlipCard.Margin = new System.Windows.Forms.Padding(4);
            this.btnFlipCard.Name = "btnFlipCard";
            this.btnFlipCard.Size = new System.Drawing.Size(100, 28);
            this.btnFlipCard.TabIndex = 0;
            this.btnFlipCard.Text = "&Flip Card";
            this.btnFlipCard.UseVisualStyleBackColor = true;
            this.btnFlipCard.Click += new System.EventHandler(this.btnFlipCard_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Diamonds",
            "Hearts",
            "Spades",
            "Clubs"});
            this.comboBox1.Location = new System.Drawing.Point(195, 176);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 25);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lblClickedState
            // 
            this.lblClickedState.AutoSize = true;
            this.lblClickedState.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClickedState.Location = new System.Drawing.Point(4, 17);
            this.lblClickedState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClickedState.Name = "lblClickedState";
            this.lblClickedState.Size = new System.Drawing.Size(264, 20);
            this.lblClickedState.TabIndex = 6;
            this.lblClickedState.Text = "The card has not yet been clicked.";
            this.lblClickedState.Click += new System.EventHandler(this.lblClickedState_Click);
            // 
            // lblFlippedState
            // 
            this.lblFlippedState.AutoSize = true;
            this.lblFlippedState.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFlippedState.Location = new System.Drawing.Point(3, 55);
            this.lblFlippedState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFlippedState.Name = "lblFlippedState";
            this.lblFlippedState.Size = new System.Drawing.Size(261, 20);
            this.lblFlippedState.TabIndex = 7;
            this.lblFlippedState.Text = "The card has not yet been flipped.";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(211, 238);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 28);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "&Main Menu";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(212, 199);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 28);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pnlActiveCards
            // 
            this.pnlActiveCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(217)))), ((int)(((byte)(120)))));
            this.pnlActiveCards.Location = new System.Drawing.Point(455, 215);
            this.pnlActiveCards.Margin = new System.Windows.Forms.Padding(4);
            this.pnlActiveCards.Name = "pnlActiveCards";
            this.pnlActiveCards.Size = new System.Drawing.Size(709, 276);
            this.pnlActiveCards.TabIndex = 13;
            // 
            // pnlPlayerCards
            // 
            this.pnlPlayerCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(164)))), ((int)(((byte)(15)))));
            this.pnlPlayerCards.Location = new System.Drawing.Point(455, 499);
            this.pnlPlayerCards.Margin = new System.Windows.Forms.Padding(4);
            this.pnlPlayerCards.Name = "pnlPlayerCards";
            this.pnlPlayerCards.Size = new System.Drawing.Size(709, 180);
            this.pnlPlayerCards.TabIndex = 14;
            // 
            // pnlComputerCards
            // 
            this.pnlComputerCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(164)))), ((int)(((byte)(15)))));
            this.pnlComputerCards.Location = new System.Drawing.Point(455, 18);
            this.pnlComputerCards.Margin = new System.Windows.Forms.Padding(4);
            this.pnlComputerCards.Name = "pnlComputerCards";
            this.pnlComputerCards.Size = new System.Drawing.Size(709, 190);
            this.pnlComputerCards.TabIndex = 15;
            // 
            // txtPlayHistory
            // 
            this.txtPlayHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(89)))), ((int)(((byte)(208)))));
            this.txtPlayHistory.Location = new System.Drawing.Point(17, 375);
            this.txtPlayHistory.Margin = new System.Windows.Forms.Padding(4);
            this.txtPlayHistory.Multiline = true;
            this.txtPlayHistory.Name = "txtPlayHistory";
            this.txtPlayHistory.Size = new System.Drawing.Size(429, 304);
            this.txtPlayHistory.TabIndex = 16;
            // 
            // cbxSuit
            // 
            this.cbxSuit.FormattingEnabled = true;
            this.cbxSuit.Items.AddRange(new object[] {
            "Diamonds",
            "Hearts",
            "Spades",
            "Clubs"});
            this.cbxSuit.Location = new System.Drawing.Point(212, 106);
            this.cbxSuit.Margin = new System.Windows.Forms.Padding(4);
            this.cbxSuit.Name = "cbxSuit";
            this.cbxSuit.Size = new System.Drawing.Size(160, 24);
            this.cbxSuit.TabIndex = 9;
            this.cbxSuit.Text = "Hearts";
            this.cbxSuit.SelectedIndexChanged += new System.EventHandler(this.cbxSuit_SelectedIndexChanged);
            // 
            // cbxRank
            // 
            this.cbxRank.FormattingEnabled = true;
            this.cbxRank.Items.AddRange(new object[] {
            "Ace",
            "Two",
            "Three",
            "Four",
            "Five",
            "Six",
            "Seven",
            "Eight",
            "Nine",
            "Ten",
            "Jack",
            "Queen",
            "King"});
            this.cbxRank.Location = new System.Drawing.Point(212, 53);
            this.cbxRank.Margin = new System.Windows.Forms.Padding(4);
            this.cbxRank.Name = "cbxRank";
            this.cbxRank.Size = new System.Drawing.Size(160, 24);
            this.cbxRank.TabIndex = 8;
            this.cbxRank.Text = "Ace";
            this.cbxRank.SelectedIndexChanged += new System.EventHandler(this.cbxRank_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(89)))), ((int)(((byte)(208)))));
            this.panel1.Controls.Add(this.lblClickedState);
            this.panel1.Controls.Add(this.lblFlippedState);
            this.panel1.Location = new System.Drawing.Point(94, 295);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 100);
            this.panel1.TabIndex = 17;
            // 
            // btnRules
            // 
            this.btnRules.Location = new System.Drawing.Point(1184, 13);
            this.btnRules.Margin = new System.Windows.Forms.Padding(4);
            this.btnRules.Name = "btnRules";
            this.btnRules.Size = new System.Drawing.Size(100, 28);
            this.btnRules.TabIndex = 18;
            this.btnRules.Text = "The R&ules";
            this.btnRules.UseVisualStyleBackColor = true;
            this.btnRules.Click += new System.EventHandler(this.btnRules_Click);
            // 
            // cbxDeck
            // 
            playingCard1.CardValue = 0;
            playingCard1.FaceUp = false;
            playingCard1.Rank = CardLib.CardRank.Six;
            playingCard1.Suit = CardLib.CardSuit.Diamonds;
            this.cbxDeck.Card = playingCard1;
            this.cbxDeck.CardOrientation = System.Windows.Forms.Orientation.Vertical;
            this.cbxDeck.FaceUp = false;
            this.cbxDeck.Location = new System.Drawing.Point(38, 18);
            this.cbxDeck.Margin = new System.Windows.Forms.Padding(5);
            this.cbxDeck.Name = "cbxDeck";
            this.cbxDeck.Rank = CardLib.CardRank.Six;
            this.cbxDeck.Size = new System.Drawing.Size(151, 180);
            this.cbxDeck.Suit = CardLib.CardSuit.Diamonds;
            this.cbxDeck.TabIndex = 5;
            this.cbxDeck.Click += new System.EventHandler(this.cbxDeck_Click);
            this.cbxDeck.Load += new System.EventHandler(this.cbxDeck_Load);
            // 
            // frmGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(28)))), ((int)(((byte)(85)))));
            this.ClientSize = new System.Drawing.Size(1301, 698);
            this.Controls.Add(this.btnRules);
            this.Controls.Add(this.txtPlayHistory);
            this.Controls.Add(this.pnlComputerCards);
            this.Controls.Add(this.pnlPlayerCards);
            this.Controls.Add(this.pnlActiveCards);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.cbxSuit);
            this.Controls.Add(this.cbxRank);
            this.Controls.Add(this.cbxDeck);
            this.Controls.Add(this.btnFlipCard);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Durak!";
            this.Load += new System.EventHandler(this.frmGame_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void BtnFlipCard_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Button btnFlipCard;
        private System.Windows.Forms.ComboBox comboBox1;
        private CardBox.CardBox cbxDeck;
        private System.Windows.Forms.Label lblClickedState;
        private System.Windows.Forms.Label lblFlippedState;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel pnlActiveCards;
        private System.Windows.Forms.Panel pnlPlayerCards;
        private System.Windows.Forms.Panel pnlComputerCards;
        private System.Windows.Forms.TextBox txtPlayHistory;
        private System.Windows.Forms.ComboBox cbxSuit;
        private System.Windows.Forms.ComboBox cbxRank;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRules;
    }
}

