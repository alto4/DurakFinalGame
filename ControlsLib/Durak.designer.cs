﻿
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
            CardLib.PlayingCard playingCard2 = new CardLib.PlayingCard();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGame));
            this.btnFlipCard = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblClickedState = new System.Windows.Forms.Label();
            this.lblFlippedState = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlActiveCards = new System.Windows.Forms.Panel();
            this.pnlPlayerCards = new System.Windows.Forms.Panel();
            this.pnlComputerCards = new System.Windows.Forms.Panel();
            this.txtComputerAttacker = new System.Windows.Forms.TextBox();
            this.txtPlayHistory = new System.Windows.Forms.TextBox();
            this.cbxSuit = new System.Windows.Forms.ComboBox();
            this.cbxRank = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRules = new System.Windows.Forms.Button();
            this.lblTrumpCard = new System.Windows.Forms.Label();
            this.lblOutOfCards = new System.Windows.Forms.Label();
            this.btnStopAttacking = new System.Windows.Forms.Button();
            this.pnlDiscard = new System.Windows.Forms.Panel();
            this.pnlDefended = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxDeck = new CardBox.CardBox();
            this.cbxTrumpCard = new CardBox.CardBox();
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
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
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
            this.btnExit.Location = new System.Drawing.Point(212, 206);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 28);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "&Main Menu";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlActiveCards
            // 
            this.pnlActiveCards.AllowDrop = true;
            this.pnlActiveCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(217)))), ((int)(((byte)(120)))));
            this.pnlActiveCards.Location = new System.Drawing.Point(767, 215);
            this.pnlActiveCards.Margin = new System.Windows.Forms.Padding(4);
            this.pnlActiveCards.Name = "pnlActiveCards";
            this.pnlActiveCards.Size = new System.Drawing.Size(332, 252);
            this.pnlActiveCards.TabIndex = 13;
            this.pnlActiveCards.DragDrop += new System.Windows.Forms.DragEventHandler(this.Panel_DragDrop);
            this.pnlActiveCards.DragEnter += new System.Windows.Forms.DragEventHandler(this.Panel_DragEnter);
            // 
            // pnlPlayerCards
            // 
            this.pnlPlayerCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(164)))), ((int)(((byte)(15)))));
            this.pnlPlayerCards.Location = new System.Drawing.Point(455, 498);
            this.pnlPlayerCards.Margin = new System.Windows.Forms.Padding(4);
            this.pnlPlayerCards.Name = "pnlPlayerCards";
            this.pnlPlayerCards.Size = new System.Drawing.Size(709, 180);
            this.pnlPlayerCards.TabIndex = 14;
            this.pnlPlayerCards.DragDrop += new System.Windows.Forms.DragEventHandler(this.Panel_DragDrop);
            this.pnlPlayerCards.DragEnter += new System.Windows.Forms.DragEventHandler(this.Panel_DragEnter);
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
            // txtComputerAttacker
            // 
            this.txtComputerAttacker.BackColor = System.Drawing.Color.Red;
            this.txtComputerAttacker.ForeColor = System.Drawing.Color.White;
            this.txtComputerAttacker.Location = new System.Drawing.Point(360, 161);
            this.txtComputerAttacker.Margin = new System.Windows.Forms.Padding(4);
            this.txtComputerAttacker.Name = "txtComputerAttacker";
            this.txtComputerAttacker.Size = new System.Drawing.Size(87, 22);
            this.txtComputerAttacker.TabIndex = 0;
            this.txtComputerAttacker.Text = "Attacker";
            this.txtComputerAttacker.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtComputerAttacker.Visible = false;
            // 
            // txtPlayHistory
            // 
            this.txtPlayHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(89)))), ((int)(((byte)(208)))));
            this.txtPlayHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.txtPlayHistory.Location = new System.Drawing.Point(17, 375);
            this.txtPlayHistory.Margin = new System.Windows.Forms.Padding(4);
            this.txtPlayHistory.Multiline = true;
            this.txtPlayHistory.Name = "txtPlayHistory";
            this.txtPlayHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
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
            this.panel1.Location = new System.Drawing.Point(93, 295);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 100);
            this.panel1.TabIndex = 17;
            // 
            // btnRules
            // 
            this.btnRules.Location = new System.Drawing.Point(1184, 14);
            this.btnRules.Margin = new System.Windows.Forms.Padding(4);
            this.btnRules.Name = "btnRules";
            this.btnRules.Size = new System.Drawing.Size(100, 28);
            this.btnRules.TabIndex = 18;
            this.btnRules.Text = "The R&ules";
            this.btnRules.UseVisualStyleBackColor = true;
            this.btnRules.Click += new System.EventHandler(this.btnRules_Click);
            // 
            // lblTrumpCard
            // 
            this.lblTrumpCard.AutoSize = true;
            this.lblTrumpCard.BackColor = System.Drawing.Color.White;
            this.lblTrumpCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrumpCard.Location = new System.Drawing.Point(53, 14);
            this.lblTrumpCard.Name = "lblTrumpCard";
            this.lblTrumpCard.Size = new System.Drawing.Size(121, 24);
            this.lblTrumpCard.TabIndex = 20;
            this.lblTrumpCard.Text = "Trump Card";
            // 
            // lblOutOfCards
            // 
            this.lblOutOfCards.AutoSize = true;
            this.lblOutOfCards.BackColor = System.Drawing.Color.Crimson;
            this.lblOutOfCards.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutOfCards.Location = new System.Drawing.Point(56, 166);
            this.lblOutOfCards.Name = "lblOutOfCards";
            this.lblOutOfCards.Size = new System.Drawing.Size(114, 20);
            this.lblOutOfCards.TabIndex = 21;
            this.lblOutOfCards.Text = "Out Of Cards!";
            this.lblOutOfCards.Visible = false;
            this.lblOutOfCards.Click += new System.EventHandler(this.lblOutOfCards_Click);
            // 
            // btnStopAttacking
            // 
            this.btnStopAttacking.BackColor = System.Drawing.Color.Black;
            this.btnStopAttacking.ForeColor = System.Drawing.Color.White;
            this.btnStopAttacking.Location = new System.Drawing.Point(1184, 562);
            this.btnStopAttacking.Margin = new System.Windows.Forms.Padding(4);
            this.btnStopAttacking.Name = "btnStopAttacking";
            this.btnStopAttacking.Size = new System.Drawing.Size(100, 52);
            this.btnStopAttacking.TabIndex = 24;
            this.btnStopAttacking.Text = "Stop Attacking";
            this.btnStopAttacking.UseVisualStyleBackColor = false;
            this.btnStopAttacking.Click += new System.EventHandler(this.btnStopAttacking_Click);
            // 
            // pnlDiscard
            // 
            this.pnlDiscard.AllowDrop = true;
            this.pnlDiscard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(217)))), ((int)(((byte)(120)))));
            this.pnlDiscard.Location = new System.Drawing.Point(1108, 215);
            this.pnlDiscard.Margin = new System.Windows.Forms.Padding(4);
            this.pnlDiscard.Name = "pnlDiscard";
            this.pnlDiscard.Size = new System.Drawing.Size(185, 252);
            this.pnlDiscard.TabIndex = 25;
            // 
            // pnlDefended
            // 
            this.pnlDefended.AllowDrop = true;
            this.pnlDefended.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(217)))), ((int)(((byte)(120)))));
            this.pnlDefended.Location = new System.Drawing.Point(455, 215);
            this.pnlDefended.Margin = new System.Windows.Forms.Padding(4);
            this.pnlDefended.Name = "pnlDefended";
            this.pnlDefended.Size = new System.Drawing.Size(304, 252);
            this.pnlDefended.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(542, 471);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 20);
            this.label1.TabIndex = 27;
            this.label1.Text = "Defended Cards";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(869, 471);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 20);
            this.label2.TabIndex = 28;
            this.label2.Text = "Attacking Cards";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1132, 471);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 20);
            this.label3.TabIndex = 29;
            this.label3.Text = "Discarded Cards";
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
            this.cbxDeck.Location = new System.Drawing.Point(37, 86);
            this.cbxDeck.Margin = new System.Windows.Forms.Padding(5);
            this.cbxDeck.Name = "cbxDeck";
            this.cbxDeck.Rank = CardLib.CardRank.Six;
            this.cbxDeck.Size = new System.Drawing.Size(151, 180);
            this.cbxDeck.Suit = CardLib.CardSuit.Diamonds;
            this.cbxDeck.TabIndex = 5;
            this.cbxDeck.TabStop = false;
            this.cbxDeck.Click += new System.EventHandler(this.cbxDeck_Click);
            // 
            // cbxTrumpCard
            // 
            playingCard2.CardValue = 0;
            playingCard2.FaceUp = true;
            playingCard2.Rank = CardLib.CardRank.Ace;
            playingCard2.Suit = CardLib.CardSuit.Diamonds;
            this.cbxTrumpCard.Card = playingCard2;
            this.cbxTrumpCard.CardOrientation = System.Windows.Forms.Orientation.Vertical;
            this.cbxTrumpCard.FaceUp = true;
            this.cbxTrumpCard.Location = new System.Drawing.Point(53, 39);
            this.cbxTrumpCard.Margin = new System.Windows.Forms.Padding(5);
            this.cbxTrumpCard.Name = "cbxTrumpCard";
            this.cbxTrumpCard.Rank = CardLib.CardRank.Ace;
            this.cbxTrumpCard.Size = new System.Drawing.Size(119, 158);
            this.cbxTrumpCard.Suit = CardLib.CardSuit.Diamonds;
            this.cbxTrumpCard.TabIndex = 19;
            this.cbxTrumpCard.TabStop = false;
            // 
            // frmGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(28)))), ((int)(((byte)(85)))));
            this.ClientSize = new System.Drawing.Size(1301, 698);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlDefended);
            this.Controls.Add(this.pnlDiscard);
            this.Controls.Add(this.btnStopAttacking);
            this.Controls.Add(this.txtComputerAttacker);
            this.Controls.Add(this.lblOutOfCards);
            this.Controls.Add(this.cbxDeck);
            this.Controls.Add(this.lblTrumpCard);
            this.Controls.Add(this.cbxTrumpCard);
            this.Controls.Add(this.btnRules);
            this.Controls.Add(this.txtPlayHistory);
            this.Controls.Add(this.pnlComputerCards);
            this.Controls.Add(this.pnlPlayerCards);
            this.Controls.Add(this.pnlActiveCards);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.cbxSuit);
            this.Controls.Add(this.cbxRank);
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
        private System.Windows.Forms.Panel pnlActiveCards;
        private System.Windows.Forms.Panel pnlPlayerCards;
        private System.Windows.Forms.Panel pnlComputerCards;
        private System.Windows.Forms.TextBox txtPlayHistory;
        private System.Windows.Forms.ComboBox cbxSuit;
        private System.Windows.Forms.ComboBox cbxRank;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRules;
        private CardBox.CardBox cbxTrumpCard;
        private System.Windows.Forms.Label lblTrumpCard;
        private System.Windows.Forms.Label lblOutOfCards;
        private System.Windows.Forms.TextBox txtComputerAttacker;
        private System.Windows.Forms.Button btnStopAttacking;
        private System.Windows.Forms.Panel pnlDiscard;
        private System.Windows.Forms.Panel pnlDefended;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

