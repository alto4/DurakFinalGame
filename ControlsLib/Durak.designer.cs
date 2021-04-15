
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlActiveCards = new System.Windows.Forms.Panel();
            this.pnlPlayerCards = new System.Windows.Forms.Panel();
            this.pnlComputerCards = new System.Windows.Forms.Panel();
            this.txtComputerAttacker = new System.Windows.Forms.TextBox();
            this.btnRules = new System.Windows.Forms.Button();
            this.lblTrumpCard = new System.Windows.Forms.Label();
            this.lblOutOfCards = new System.Windows.Forms.Label();
            this.btnEndTurn = new System.Windows.Forms.Button();
            this.pnlDiscard = new System.Windows.Forms.Panel();
            this.pnlDefended = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxDeck = new CardBox.CardBox();
            this.cbxTrumpCard = new CardBox.CardBox();
            this.SuspendLayout();
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
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(889, 50);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "&Main Menu";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlActiveCards
            // 
            this.pnlActiveCards.AllowDrop = true;
            this.pnlActiveCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(217)))), ((int)(((byte)(120)))));
            this.pnlActiveCards.Location = new System.Drawing.Point(512, 175);
            this.pnlActiveCards.Name = "pnlActiveCards";
            this.pnlActiveCards.Size = new System.Drawing.Size(214, 205);
            this.pnlActiveCards.TabIndex = 13;
            this.pnlActiveCards.DragDrop += new System.Windows.Forms.DragEventHandler(this.Panel_DragDrop);
            this.pnlActiveCards.DragEnter += new System.Windows.Forms.DragEventHandler(this.Panel_DragEnter);
            // 
            // pnlPlayerCards
            // 
            this.pnlPlayerCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(164)))), ((int)(((byte)(15)))));
            this.pnlPlayerCards.Location = new System.Drawing.Point(243, 405);
            this.pnlPlayerCards.Name = "pnlPlayerCards";
            this.pnlPlayerCards.Size = new System.Drawing.Size(630, 146);
            this.pnlPlayerCards.TabIndex = 14;
            this.pnlPlayerCards.DragDrop += new System.Windows.Forms.DragEventHandler(this.Panel_DragDrop);
            this.pnlPlayerCards.DragEnter += new System.Windows.Forms.DragEventHandler(this.Panel_DragEnter);
            // 
            // pnlComputerCards
            // 
            this.pnlComputerCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(164)))), ((int)(((byte)(15)))));
            this.pnlComputerCards.Location = new System.Drawing.Point(243, 15);
            this.pnlComputerCards.Name = "pnlComputerCards";
            this.pnlComputerCards.Size = new System.Drawing.Size(630, 154);
            this.pnlComputerCards.TabIndex = 15;
            // 
            // txtComputerAttacker
            // 
            this.txtComputerAttacker.BackColor = System.Drawing.Color.Red;
            this.txtComputerAttacker.ForeColor = System.Drawing.Color.White;
            this.txtComputerAttacker.Location = new System.Drawing.Point(158, 79);
            this.txtComputerAttacker.Name = "txtComputerAttacker";
            this.txtComputerAttacker.Size = new System.Drawing.Size(66, 20);
            this.txtComputerAttacker.TabIndex = 0;
            this.txtComputerAttacker.Text = "Attacker";
            this.txtComputerAttacker.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtComputerAttacker.Visible = false;
            // 
            // btnRules
            // 
            this.btnRules.Location = new System.Drawing.Point(888, 11);
            this.btnRules.Name = "btnRules";
            this.btnRules.Size = new System.Drawing.Size(75, 23);
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
            this.lblTrumpCard.Location = new System.Drawing.Point(44, 131);
            this.lblTrumpCard.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTrumpCard.Name = "lblTrumpCard";
            this.lblTrumpCard.Size = new System.Drawing.Size(97, 18);
            this.lblTrumpCard.TabIndex = 20;
            this.lblTrumpCard.Text = "Trump Card";
            // 
            // lblOutOfCards
            // 
            this.lblOutOfCards.AutoSize = true;
            this.lblOutOfCards.BackColor = System.Drawing.Color.Crimson;
            this.lblOutOfCards.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutOfCards.Location = new System.Drawing.Point(47, 257);
            this.lblOutOfCards.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOutOfCards.Name = "lblOutOfCards";
            this.lblOutOfCards.Size = new System.Drawing.Size(94, 17);
            this.lblOutOfCards.TabIndex = 21;
            this.lblOutOfCards.Text = "Out Of Cards!";
            this.lblOutOfCards.Visible = false;
            this.lblOutOfCards.Click += new System.EventHandler(this.lblOutOfCards_Click);
            // 
            // btnEndTurn
            // 
            this.btnEndTurn.BackColor = System.Drawing.Color.Black;
            this.btnEndTurn.ForeColor = System.Drawing.Color.White;
            this.btnEndTurn.Location = new System.Drawing.Point(888, 457);
            this.btnEndTurn.Name = "btnEndTurn";
            this.btnEndTurn.Size = new System.Drawing.Size(75, 42);
            this.btnEndTurn.TabIndex = 24;
            this.btnEndTurn.Text = "End Turn";
            this.btnEndTurn.UseVisualStyleBackColor = false;
            this.btnEndTurn.Click += new System.EventHandler(this.btnEndTurn_Click);
            // 
            // pnlDiscard
            // 
            this.pnlDiscard.AllowDrop = true;
            this.pnlDiscard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(217)))), ((int)(((byte)(120)))));
            this.pnlDiscard.Location = new System.Drawing.Point(733, 175);
            this.pnlDiscard.Name = "pnlDiscard";
            this.pnlDiscard.Size = new System.Drawing.Size(139, 205);
            this.pnlDiscard.TabIndex = 25;
            // 
            // pnlDefended
            // 
            this.pnlDefended.AllowDrop = true;
            this.pnlDefended.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(217)))), ((int)(((byte)(120)))));
            this.pnlDefended.Location = new System.Drawing.Point(243, 175);
            this.pnlDefended.Name = "pnlDefended";
            this.pnlDefended.Size = new System.Drawing.Size(262, 205);
            this.pnlDefended.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(326, 383);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 17);
            this.label1.TabIndex = 27;
            this.label1.Text = "Defended Cards";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(571, 383);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 17);
            this.label2.TabIndex = 28;
            this.label2.Text = "Attacking Cards";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(751, 383);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 17);
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
            this.cbxDeck.Location = new System.Drawing.Point(33, 192);
            this.cbxDeck.Margin = new System.Windows.Forms.Padding(4);
            this.cbxDeck.Name = "cbxDeck";
            this.cbxDeck.Rank = CardLib.CardRank.Six;
            this.cbxDeck.Size = new System.Drawing.Size(113, 146);
            this.cbxDeck.Suit = CardLib.CardSuit.Diamonds;
            this.cbxDeck.TabIndex = 5;
            this.cbxDeck.TabStop = false;
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
            this.cbxTrumpCard.Location = new System.Drawing.Point(45, 154);
            this.cbxTrumpCard.Margin = new System.Windows.Forms.Padding(4);
            this.cbxTrumpCard.Name = "cbxTrumpCard";
            this.cbxTrumpCard.Rank = CardLib.CardRank.Ace;
            this.cbxTrumpCard.Size = new System.Drawing.Size(89, 128);
            this.cbxTrumpCard.Suit = CardLib.CardSuit.Diamonds;
            this.cbxTrumpCard.TabIndex = 19;
            this.cbxTrumpCard.TabStop = false;
            // 
            // frmGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(28)))), ((int)(((byte)(85)))));
            this.ClientSize = new System.Drawing.Size(976, 567);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlDefended);
            this.Controls.Add(this.pnlDiscard);
            this.Controls.Add(this.btnEndTurn);
            this.Controls.Add(this.txtComputerAttacker);
            this.Controls.Add(this.lblOutOfCards);
            this.Controls.Add(this.cbxDeck);
            this.Controls.Add(this.lblTrumpCard);
            this.Controls.Add(this.cbxTrumpCard);
            this.Controls.Add(this.btnRules);
            this.Controls.Add(this.pnlComputerCards);
            this.Controls.Add(this.pnlPlayerCards);
            this.Controls.Add(this.pnlActiveCards);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Durak!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGame_FormClosing);
            this.Load += new System.EventHandler(this.frmGame_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void BtnFlipCard_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox1;
        private CardBox.CardBox cbxDeck;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel pnlActiveCards;
        private System.Windows.Forms.Panel pnlPlayerCards;
        private System.Windows.Forms.Panel pnlComputerCards;
        private System.Windows.Forms.Button btnRules;
        private CardBox.CardBox cbxTrumpCard;
        private System.Windows.Forms.Label lblTrumpCard;
        private System.Windows.Forms.Label lblOutOfCards;
        private System.Windows.Forms.TextBox txtComputerAttacker;
        private System.Windows.Forms.Button btnEndTurn;
        private System.Windows.Forms.Panel pnlDiscard;
        private System.Windows.Forms.Panel pnlDefended;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

