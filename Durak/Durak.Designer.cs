
using System;

namespace Durak
{
    partial class Durak
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
            CardLib.PlayingCard playingCard2 = new CardLib.PlayingCard();
            this.btnFlipCard = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cbxDeck = new CardBox.CardBox();
            this.lblClickedState = new System.Windows.Forms.Label();
            this.lblFlippedState = new System.Windows.Forms.Label();
            this.cardBox1 = new CardBox.CardBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.pnlActiveCards = new System.Windows.Forms.Panel();
            this.pnlPlayerCards = new System.Windows.Forms.Panel();
            this.pnlComputerCards = new System.Windows.Forms.Panel();
            this.txtPlayHistory = new System.Windows.Forms.TextBox();
            this.cbxSuit = new System.Windows.Forms.ComboBox();
            this.cbxRank = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnFlipCard
            // 
            this.btnFlipCard.Location = new System.Drawing.Point(159, 128);
            this.btnFlipCard.Name = "btnFlipCard";
            this.btnFlipCard.Size = new System.Drawing.Size(75, 23);
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
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
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
            this.cbxDeck.Location = new System.Drawing.Point(29, 23);
            this.cbxDeck.Name = "cbxDeck";
            this.cbxDeck.Rank = CardLib.CardRank.Six;
            this.cbxDeck.Size = new System.Drawing.Size(113, 146);
            this.cbxDeck.Suit = CardLib.CardSuit.Diamonds;
            this.cbxDeck.TabIndex = 5;
            this.cbxDeck.Click += new System.EventHandler(this.cbxDeck_Click);
            
            // 
            // lblClickedState
            // 
            this.lblClickedState.AutoSize = true;
            this.lblClickedState.Location = new System.Drawing.Point(26, 234);
            this.lblClickedState.Name = "lblClickedState";
            this.lblClickedState.Size = new System.Drawing.Size(172, 13);
            this.lblClickedState.TabIndex = 6;
            this.lblClickedState.Text = "The card has not yet been clicked.";
            // 
            // lblFlippedState
            // 
            this.lblFlippedState.AutoSize = true;
            this.lblFlippedState.Location = new System.Drawing.Point(27, 270);
            this.lblFlippedState.Name = "lblFlippedState";
            this.lblFlippedState.Size = new System.Drawing.Size(169, 13);
            this.lblFlippedState.TabIndex = 7;
            this.lblFlippedState.Text = "The card has not yet been flipped.";
            // 
            // cardBox1
            // 
            playingCard2.CardValue = 0;
            playingCard2.FaceUp = false;
            playingCard2.Rank = CardLib.CardRank.Six;
            playingCard2.Suit = CardLib.CardSuit.Diamonds;
            this.cardBox1.Card = playingCard2;
            this.cardBox1.CardOrientation = System.Windows.Forms.Orientation.Vertical;
            this.cardBox1.FaceUp = false;
            this.cardBox1.Location = new System.Drawing.Point(922, 175);
            this.cardBox1.Name = "cardBox1";
            this.cardBox1.Rank = CardLib.CardRank.Six;
            this.cardBox1.Size = new System.Drawing.Size(113, 146);
            this.cardBox1.Suit = CardLib.CardSuit.Diamonds;
            this.cardBox1.TabIndex = 10;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(158, 193);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "&Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(159, 162);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // pnlActiveCards
            // 
            this.pnlActiveCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.pnlActiveCards.Location = new System.Drawing.Point(341, 175);
            this.pnlActiveCards.Name = "pnlActiveCards";
            this.pnlActiveCards.Size = new System.Drawing.Size(532, 152);
            this.pnlActiveCards.TabIndex = 13;
            // 
            // pnlPlayerCards
            // 
            this.pnlPlayerCards.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.pnlPlayerCards.Location = new System.Drawing.Point(341, 333);
            this.pnlPlayerCards.Name = "pnlPlayerCards";
            this.pnlPlayerCards.Size = new System.Drawing.Size(532, 146);
            this.pnlPlayerCards.TabIndex = 14;
            // 
            // pnlComputerCards
            // 
            this.pnlComputerCards.BackColor = System.Drawing.Color.DarkRed;
            this.pnlComputerCards.Location = new System.Drawing.Point(341, 15);
            this.pnlComputerCards.Name = "pnlComputerCards";
            this.pnlComputerCards.Size = new System.Drawing.Size(532, 154);
            this.pnlComputerCards.TabIndex = 15;
            // 
            // txtPlayHistory
            // 
            this.txtPlayHistory.Location = new System.Drawing.Point(12, 286);
            this.txtPlayHistory.Multiline = true;
            this.txtPlayHistory.Name = "txtPlayHistory";
            this.txtPlayHistory.Size = new System.Drawing.Size(323, 193);
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
            this.cbxSuit.Location = new System.Drawing.Point(159, 86);
            this.cbxSuit.Name = "cbxSuit";
            this.cbxSuit.Size = new System.Drawing.Size(121, 21);
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
            this.cbxRank.Location = new System.Drawing.Point(159, 43);
            this.cbxRank.Name = "cbxRank";
            this.cbxRank.Size = new System.Drawing.Size(121, 21);
            this.cbxRank.TabIndex = 8;
            this.cbxRank.Text = "Ace";
            this.cbxRank.SelectedIndexChanged += new System.EventHandler(this.cbxRank_SelectedIndexChanged);
            // 
            // Durak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 491);
            this.Controls.Add(this.txtPlayHistory);
            this.Controls.Add(this.pnlComputerCards);
            this.Controls.Add(this.pnlPlayerCards);
            this.Controls.Add(this.pnlActiveCards);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.cardBox1);
            this.Controls.Add(this.cbxSuit);
            this.Controls.Add(this.cbxRank);
            this.Controls.Add(this.lblFlippedState);
            this.Controls.Add(this.lblClickedState);
            this.Controls.Add(this.cbxDeck);
            this.Controls.Add(this.btnFlipCard);
            this.Name = "Durak";
            this.Text = "frmMain";
            this.Load += new System.EventHandler(this.Durak_Load);
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
        private CardBox.CardBox cardBox1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel pnlActiveCards;
        private System.Windows.Forms.Panel pnlPlayerCards;
        private System.Windows.Forms.Panel pnlComputerCards;
        private System.Windows.Forms.TextBox txtPlayHistory;
        private System.Windows.Forms.ComboBox cbxSuit;
        private System.Windows.Forms.ComboBox cbxRank;
    }
}

