using CardLib;
using ControlsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Durak
{
    public partial class frmGame : Form
    {

        #region FIELDS AND PROPERTIES

        // generate PlayingCard objects from a Deck
        Deck mainDeck = new Deck(true);

        // enlarge a card by this value
        private const int ENLARGE = 30;

        // The default size of a card
        static private Size normalCardSize = new Size(151,180);

        // makes card draggable
        private CardBox.CardBox dragCard;

        #endregion

        #region FORM AND STATIC CONTROL EVENT HANDLERS 

        /// <summary>
        /// Constructor for the main form
        /// </summary>
        public frmGame()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the main form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmGame_Load(object sender, EventArgs e)
        {
            // TEST SHUFFLE
            mainDeck.Shuffle();

            // seeing the order of the deck in debug console for debugging
            mainDeck.ShowDeck();
            System.Diagnostics.Debug.WriteLine(mainDeck.ToString());

            //initialDeal();
            InitialDeal();
            PlayingCard firstCard = mainDeck.DrawCard();
            this.cbxDeck.Card = firstCard;

            txtPlayHistory.Text += firstCard.Suit + " is the initial trump suit.";
            //txtPlayHistory.Text += "\nThere are now " + mainDeck.Count() + " cards left in the deck";

            //Wire out the out of cards event handler
            //mainDeck.OutOfCards MAKE A METHOD TO TRIGGER AN OUT OF CARDS EVENT
            //Show the number of cards left in the deck

        }

        /// <summary>
        /// Event for when the index changes on a combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// When the user clicks on the deck
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDeck_Click(object sender, EventArgs e)
        {
            
            lblClickedState.Text = cbxDeck.ToString() + " was last clicked." + Environment.NewLine;
            txtPlayHistory.Text += "Loaded!" + Environment.NewLine;

            /* NOTE: idk why the cards need to switch between horizontal and vertical - @Ed
            if (cbxDeck.CardOrientation == Orientation.Horizontal)
            {
                cbxDeck.CardOrientation = Orientation.Vertical;
            }
            else
            {
                cbxDeck.CardOrientation = Orientation.Horizontal;
            }
            */
            //If the deck is empty (no image)
            //if (stmt)
            //{
            //Reset the dealer
            //}
            //else //otherwise           
            
            txtPlayHistory.Text += cbxDeck.Card.ToString();
            
            //Create a new card
            PlayingCard card = cbxDeck.Card;
            card.FaceUp = true;

            txtPlayHistory.Text += card.ToString();
            if (card != null )
            {
                //Create new cardbox control based on card drawn
                CardBox.CardBox aCardBox = new CardBox.CardBox(card);
                txtPlayHistory.Text += "A cardbox equals " + aCardBox.ToString();

                //wire the event handlers
                aCardBox.Click += CardBox_Click; //When the player clicks a card in their hand

                //click or drag logic here at a later date

                aCardBox.MouseEnter += CardBox_MouseEnter; //wire cardbox mouse enter
                aCardBox.MouseLeave += CardBox_MouseLeave; //wire cardbox mouse leave

                //add new controls to the appropriate panel
                pnlPlayerCards.Controls.Add(aCardBox);


                txtPlayHistory.Text += Environment.NewLine + "Cards in players deck: " + (pnlPlayerCards.Controls.Count.ToString());
                //txtPlayHistory.Text += Environment.NewLine + "Cards in dealer deck: " + mainDeck.ToString();
                cbxDeck.Card =  mainDeck.DrawCard();

                foreach (CardBox.CardBox playerCard in pnlPlayerCards.Controls)
                {
                    if (card.GetType().ToString() == "CardLib.PlayingCard")
                    {

                        //txtPlayHistory.Text += "Player card observed";

                        //playerCard.Card.FaceUp = true
                        ;
                        playerCard.FaceUp = true;
                        txtPlayHistory.Text += Environment.NewLine + playerCard.Card.DebugString();
                    }
                    else
                    {
                        txtPlayHistory.Text += card.GetType().ToString();
                    }
                }

                //realign the controls
                RealignCards(pnlPlayerCards);
            }

            //display the number of cards left

            //}

        }

        /// <summary>
        /// When the Flip Card button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFlipCard_Click(object sender, EventArgs e)
        {
            cbxDeck.FaceUp = !cbxDeck.FaceUp;
        }
        /// <summary>
        /// When the selected index is changed for cbxSuit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxSuit_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxDeck.Suit = (CardLib.CardSuit)cbxSuit.SelectedIndex;
        }
        /// <summary>
        /// When the selected index is changed for cbxRank
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRank_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxDeck.Rank = (CardLib.CardRank)cbxRank.SelectedIndex + 1;
        }
        /// <summary>
        /// When the exit button is clicked by the user. In case user did not mean to press this button
        /// there is a cancel option.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            // to check if they player meant to go back to the main menu
            DialogResult result = MessageBox.Show("Are you sure you want to go back to the main menu?", "Back To Main Menu", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // hidding frmGame
                this.Hide();

                // new frmMainMenu instance
                frmMainMenu mainMenu = new frmMainMenu();

                // show the frmMainMenu form
                mainMenu.ShowDialog();

                // close frmMainMenu
                this.Close();
            }
        }
        /// <summary>
        /// Sets the card back image to null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDeck_OutOfCards(object sender, EventArgs e )
        {
            cbxDeck.BackgroundImage = null;
        }

        /// <summary>
        /// Button that shows the rules form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRules_Click(object sender, EventArgs e)
        {
            // new frmMainMenu instance
            frmRules rules = new frmRules();

            // show the frmRules form
            rules.ShowDialog();
        }


        #endregion

        #region CARDBOX EVENT HANDLERS
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CardBox_Click(object sender, EventArgs e)
        {
            //Convert sender to a cardbox
            CardBox.CardBox aCardBox = sender as CardBox.CardBox;

            //if the conversion worked
            if (aCardBox != null)
            {
                //if the card is in the home panel
                if (aCardBox.Parent == pnlPlayerCards)
                {
                    //Remove the card from player hand
                    aCardBox.Controls.Remove(aCardBox);
                    //Add card to the active play area
                    pnlActiveCards.Controls.Add(aCardBox);
                    txtPlayHistory.Text += "Card Clicked";
                }
                else //otherwise
                {
                    txtPlayHistory.Text += "Something is wrong";
                }

                RealignCards(pnlPlayerCards);
                RealignCards(pnlActiveCards);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CardBox_MouseEnter(object sender, EventArgs e)
        {
            //Convert sender to a cardbox
            CardBox.CardBox aCardBox = sender as CardBox.CardBox;
            txtPlayHistory.Text += "Mouse Entered";

            //if the conversion worked
            if (aCardBox != null)
            {
                //Enlarge
                aCardBox.Size = new Size(normalCardSize.Width + ENLARGE, normalCardSize.Height + ENLARGE);

                //Move the card to the top edge of the panel
                aCardBox.Top = 0;
            }
        }

        void CardBox_MouseLeave(object sender, EventArgs e)
        {
            //Convert sender to a cardbox
            CardBox.CardBox aCardBox = sender as CardBox.CardBox;
            txtPlayHistory.Text += "Mouse Left";

            //if the conversion worked
            if (aCardBox != null)
            {
                //Return to normal
                aCardBox.Size = normalCardSize;

                //Move the card to the top edge of the panel
                aCardBox.Top = 0;
            }
        }

        #endregion

        #region HELPER METHODS
         /// <summary>
        /// Repositions the cards in a panel so that they are evenly distributed in the area available.
        /// </summary>
        /// <param name="panelHand"></param>
        private void RealignCards(Panel panelHand)
        {
            // Determine the number of cards/controls in the panel.
            int myCount = panelHand.Controls.Count;

            // If there are any cards in the panel
            if (myCount > 0)
            {
                // Determine how wide one card/control is.
                int cardWidth = panelHand.Controls[0].Width;

                // Determine where the left-hand edge of a card/control placed 
                // in the middle of the panel should be  
                int startPoint = (panelHand.Width - cardWidth) / 2;

                // An offset for the remaining cards
                int offset = 0;

                // If there are more than one cards/controls in the panel
                if (myCount > 1)
                {
                    // Determine what the offset should be for each card based on the 
                    // space available and the number of card/controls
                    offset = (panelHand.Width - cardWidth - 2 * ENLARGE) / (myCount - 1);

                    // If the offset is bigger than the card/control width, i.e. there is lots of room, 
                    // set the offset to the card width. The cards/controls will not overlap at all.
                    if (offset > cardWidth)
                        offset = cardWidth;

                    // Determine width of all the cards/controls 
                    int allCardsWidth = (myCount - 1) * offset + cardWidth;
                    // Set the start point to where the left-hand edge of the "first" card should be.
                    startPoint = (panelHand.Width - allCardsWidth) / 2;
                }

                // Aligning the cards: Note that I align them in reserve order from how they
                // are stored in the controls collection. This is so that cards on the left 
                // appear underneath cards to the right. This allows the user to see the rank
                // and suit more easily.

                // Align the "first" card (which is the last control in the collection)
                panelHand.Controls[myCount - 1].Top = ENLARGE;
                System.Diagnostics.Debug.Write(panelHand.Controls[myCount - 1].Top.ToString() + "\n");
                panelHand.Controls[myCount - 1].Left = startPoint;

                // for each of the remaining controls, in reverse order.
                for (int index = myCount - 2; index >= 0; index--)
                {
                    // Align the current card
                    panelHand.Controls[index].Top = ENLARGE;
                    panelHand.Controls[index].Left = panelHand.Controls[index + 1].Left + offset;
                }
            }
        }

        /// <summary>
        /// initialDeal - deals players 6 cards to start
        /// </summary>
        private void InitialDeal()
        {
            for (int i = 0; i <= 6; i++)
            {
                PlayingCard card = cbxDeck.Card;

                if (card != null) //if card isn't null
                {
                    card.FaceUp = true;

                    //Make it a cardbox for the player
                    CardBox.CardBox playerCardBox = new CardBox.CardBox(card);
                    //Wire events
                    playerCardBox.Click += CardBox_Click; //When the player clicks a card in their hand
                    //click or drag logic here at a later date
                    playerCardBox.MouseEnter += CardBox_MouseEnter; //wire cardbox mouse enter
                    playerCardBox.MouseLeave += CardBox_MouseLeave; //wire cardbox mouse leave

                    //Add cardbox to panel
                    pnlPlayerCards.Controls.Add(playerCardBox);
                    cbxDeck.Card = mainDeck.DrawCard();

                    //Make a cardbox for the computer
                    card = cbxDeck.Card;
                    card.FaceUp = true;

                    pnlComputerCards.Controls.Add(new CardBox.CardBox(card));
                    cbxDeck.Card = mainDeck.DrawCard();
                }
            }
            RealignCards(pnlPlayerCards);
            RealignCards(pnlComputerCards);
        }

        /// <summary>
        /// RoundDeal - deals both the computer and players card until they have 6 cards in their hand to proceed to the round
        /// </summary>
        private void RoundDeal()
        {
            for (int i = pnlPlayerCards.Controls.Count; i <= 6; i++)
            {
                PlayingCard card = cbxDeck.Card;
                card.FaceUp = true;

                pnlPlayerCards.Controls.Add(new CardBox.CardBox(card));
                cbxDeck.Card = mainDeck.DrawCard();
            }
            RealignCards(pnlPlayerCards);

            for (int i = pnlComputerCards.Controls.Count; i <= 6; i++)
            {
                PlayingCard card = cbxDeck.Card;
                card.FaceUp = true;
                pnlComputerCards.Controls.Add(new CardBox.CardBox(card));
                cbxDeck.Card = mainDeck.DrawCard();
            }
            RealignCards(pnlComputerCards);
        }

        #endregion

        #region EMPTY EVENT HANDLERS
        private void btnReset_Click(object sender, EventArgs e)
        {

        }

        private void cbxDeck_Load(object sender, EventArgs e)
        {

        }

        private void lblClickedState_Click(object sender, EventArgs e)
        {

        }

        #endregion
        
    }
}
