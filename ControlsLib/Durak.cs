using CardLib;
using ControlsLib;
using System;
using System.Collections;
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

        //Testing Player Class Methods
        //StatsPlayer nick = new StatsPlayer();
        //String used to reference the player in the logs and stats
        string playerName;
        
        // generate PlayingCard objects from a Deck
        Deck mainDeck = new Deck(SizeOfDecks.Normal);

        // enlarge a card by this value
        private const int ENLARGE = 35;

        // The default size of a card
        static private Size normalCardSize = new Size(100,135);

        // makes card draggable
        private CardBox.CardBox dragCard;

        // to collect all of the card panels
        List<Panel> cardPanels = new List<Panel>();

        CardRank rankOfLastDefense;
        Boolean playerAttacking = true;
        Boolean initialAttackDefended = false;
        #endregion

        #region FORM AND STATIC CONTROL EVENT HANDLERS 

        /// <summary>
        /// Constructor for the main form
        /// </summary>
        public frmGame()
        {
            InitializeComponent();

            playerName = "Player 1";
        }

        public frmGame(string name)
        {
            InitializeComponent();

            playerName = name;
        }

        /// <summary>
        /// When the main form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmGame_Load(object sender, EventArgs e)
        {
            // adding all of the card panels to a list
            cardPanels.Add(pnlActiveCards);
            cardPanels.Add(pnlComputerCards);
            cardPanels.Add(pnlPlayerCards);

            StartGame();

            //When the StreamReader and StreamWriters are incorporated, we will pull a Vector
            //of players from a csv, or other text file. 
            //This is where we will pull all the previous stats and put them into a Vector, 
            //Then we are going to search the vector for the username to see if it exists,
            //if it does, then our current player equals that player, otherwise, it will be a
            //new player to add to the stats.

            //Testing StatsPlayer Methods
            txtPlayHistory.Text += Environment.NewLine + "! Welcome to the game, " + playerName + "!";


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
                       
            if (mainDeck.Size >= 0)
            {
                PlayingCard card = new PlayingCard();

                SettingsWithCards();     

                txtPlayHistory.Text += cbxDeck.Card.ToString();

                //Create a new card
                card = (mainDeck.Size == 0) ? cbxTrumpCard.Card : cbxDeck.Card;

                card.FaceUp = true;
                txtPlayHistory.Text += card.ToString();

                if (card != null )
                {

                    //Create new cardbox control based on card drawn
                    CardBox.CardBox aCardBox = new CardBox.CardBox(card);

                    aCardBox.Size = normalCardSize;

                    //wire the event handlers (NEEDS TO BE A CARDBOX)
                    //aCardBox.Click += CardBox_Click; //When the player clicks a card in their hand

                    // wire drag drop
                    WireCardBoxEventHandlers(aCardBox);

                    //add new controls to the appropriate panel
                    pnlPlayerCards.Controls.Add(aCardBox);

                    txtPlayHistory.Text += Environment.NewLine + "Cards in players deck: " + (pnlPlayerCards.Controls.Count.ToString());
                    //txtPlayHistory.Text += Environment.NewLine + "Cards in dealer deck: " + mainDeck.ToString();
                
                    try
                    {
                        if (mainDeck.Size == 0)
                        {
                            SettingsOutOfCards();
                        }
                        else
                        {
                            cbxDeck.Card = mainDeck.DrawCard();
                        }
                
                        foreach (CardBox.CardBox playerCard in pnlPlayerCards.Controls)
                        {
                            if (card.GetType().ToString() == "CardLib.PlayingCard")
                            {
                                //txtPlayHistory.Text += "Player card observed";
                                //playerCard.FaceUp = true;
                                txtPlayHistory.Text += Environment.NewLine + playerCard.Card.DebugString();
                            }
                            else
                            {
                                txtPlayHistory.Text += card.GetType().ToString();
                            }
                        }

                        //realign the controls
                        RealignAllCards();
                    }
                    catch (IndexOutOfRangeException)
                    {
                        System.Diagnostics.Debug.WriteLine("Exception catched when trying to draw card out of index.");
                    }
                }
                //display the number of cards left
            } 
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

        /// <summary>
        /// Make the mouse pointer a "move" pointer when a drag enters a Panel.
        /// </summary>
        private void Panel_DragEnter(object sender, DragEventArgs e)
        {
            // Make the mouse pointer a "move" pointer
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// Move a card/control when it is dropped from one Panel to another.
        /// </summary>
        private void Panel_DragDrop(object sender, DragEventArgs e)
        {
            if (dragCard != null)
            {
                Panel thisPanel = sender as Panel;
                Panel fromPanel = dragCard.Parent as Panel;
                CardBox.CardBox aCardBox = new CardBox.CardBox();

                if (thisPanel != null && fromPanel != null)
                {
                    if (thisPanel != fromPanel)
                    {
                        fromPanel.Controls.Remove(dragCard);
                        thisPanel.Controls.Add(dragCard);

                        RealignCards(thisPanel);
                        RealignCards(fromPanel);


                        // WHERE GAME LOGIN KICKS OFF 
                        // This could also be a separate event handler triggering AI function to consider best choice - currently just choosing the first card
                        // NOTE index 0 is on right side of computer panel of cards

                        // AI Function to determine best card
                        int computerChoiceIndex = determineBestPlay(pnlComputerCards);

                        if (computerChoiceIndex >= 0)
                        {
                            CardBox.CardBox computerCardBox = pnlComputerCards.Controls[computerChoiceIndex] as CardBox.CardBox;
                            aCardBox.Controls.Remove(computerCardBox);
                            pnlActiveCards.Controls.Add(computerCardBox);
                            txtPlayHistory.Text += "Computer responds with card immediately." + Environment.NewLine;


                            // ***TODO: Fix cards for comparison based on round circumstances
                            CompareCards((CardBox.CardBox)pnlActiveCards.Controls[0], computerCardBox, this.initialAttackDefended);
                                                
                        }
                        else
                        {
                            txtPlayHistory.Text += Environment.NewLine + "COMPUTER HAS NO GOOD CHOICES. Human wins this attack/defense. Things will happen here to proceed with gameplay." + Environment.NewLine;
                            MessageBox.Show("Attack successful");

                            // Wire in events to discard pnlActiveCards to pnlComputerCards


                        }


                    }
                }

                RealignAllCards();
            }
        }

        /// <summary>
        /// Clicking the restart button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region CARDBOX EVENT HANDLERS



        /// <summary>
        /// When a CardBox is clicked
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

                    // WHERE GAME LOGIN KICKS OFF 
                    // This could also be a separate event handler triggering AI function to consider best choice - currently just choosing the first card
                    // NOTE index 0 is on right side of computer panel of cards

                    // AI Function to determine best card
                    int computerChoiceIndex = determineBestPlay(pnlComputerCards);

                    if (computerChoiceIndex >= 0)
                    {
                        CardBox.CardBox computerCardBox = pnlComputerCards.Controls[computerChoiceIndex] as CardBox.CardBox;
                        aCardBox.Controls.Remove(computerCardBox);
                        pnlActiveCards.Controls.Add(computerCardBox);
                        txtPlayHistory.Text += "Computer responds with card immediately." + Environment.NewLine;
                    }
                    else
                    {
                        txtPlayHistory.Text += Environment.NewLine + "COMPUTER HAS NO GOOD CHOICES. Human wins this attack/defense. Things will happen here to proceed with gameplay." + Environment.NewLine;
                        MessageBox.Show("Computer has no choices.");
                    }

                    // Determine winner
                }
                else //otherwise
                {
                    txtPlayHistory.Text += "Something is wrong";
                }

                RealignAllCards();
            }
            
        }

        /// <summary>
        /// When a drag enters a card, enter the parent panel instead.
        /// </summary>
        private void CardBox_DragEnter(object sender, DragEventArgs e)
        {
            //// Convert sender to a CardBox
            CardBox.CardBox aCardBox = sender as CardBox.CardBox;

            // If the conversion worked
            if (aCardBox != null)
            {
                // Do the operation on the parent panel instead
                Panel_DragEnter(aCardBox.Parent, e);
            }
        }

        /// <summary>
        /// When a drag is dropped on a card, drop on the parent panel instead.
        /// </summary>
        private void CardBox_DragDrop(object sender, DragEventArgs e)
        {
            // Convert sender to a CardBox
            CardBox.CardBox aCardBox = sender as CardBox.CardBox;

            // If the conversion worked
            if (aCardBox != null)
            {
                // Do the operation on the parent panel instead
                Panel_DragDrop(aCardBox.Parent, e);

            }
        }

        /// <summary>
        /// Make a card bigger when entering its box
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                aCardBox.Top = ENLARGE;
            }

        }

        /// <summary>
        /// Start a card move on drag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardBox_MouseDown(object sender, MouseEventArgs e)
        {
            // Set dragCard 
            dragCard = sender as CardBox.CardBox;
                       
            if (dragCard != null)
            {
                // Set the data to be dragged and the allowed effect dragging will have.
                DoDragDrop(dragCard, DragDropEffects.Move);
            }
        }

        /// <summary>
        /// Button for when the player decides they are done attacking (whether due to a lack of enabled cards, or strategic move to save valuable cards)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ***TODO: Add in draw counter for player stats***
        private void btnStopAttacking_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Computer's turn to attack!");

            // Put previous cards in discard pile
            
            // Loop through all cards in the players hand and disable any cards outside of those with valid ranks
            foreach (CardBox.CardBox playedCard in pnlActiveCards.Controls)
            {
                
                //Make it a cardbox for the player
                CardBox.CardBox playedCardBox = new CardBox.CardBox(playedCard.Card);

                playedCardBox.Size = normalCardSize;
                               
                //Add cardbox to panel
                pnlDiscard.Controls.Add(playedCardBox);
                //playedCardBox.Card.FaceUp = false;
                txtPlayHistory.Text += Environment.NewLine + playedCardBox.ToString();
               
            }  
                        
            while(pnlActiveCards.Controls.Count > 0)
            {
                pnlActiveCards.Controls.RemoveAt(0);
            }

            // ***TODO: Flip discarded pile to facedown *** 
            RoundDeal();

            reenableAllCards();

            txtComputerAttacker.Visible = true;
            btnStopAttacking.Visible = false;

            playerAttacking = false;
            ComputerAttacks();

        }

        /// <summary>
        /// 
        /// </summary>
        private void ComputerAttacks()
        {
            Random rand = new Random();

            int computerChoiceIndex = rand.Next(pnlComputerCards.Controls.Count);

            txtPlayHistory.Text += Environment.NewLine + "Computers chosen attack index: " + computerChoiceIndex;

            if (computerChoiceIndex >= 0)
            {
                CardBox.CardBox computerCard = pnlComputerCards.Controls[computerChoiceIndex] as CardBox.CardBox;
                pnlComputerCards.Controls.Remove(computerCard);
                pnlActiveCards.Controls.Add(computerCard);

                txtPlayHistory.Text += "Computer attacks with card immediately." + Environment.NewLine;

                CompareCards((CardBox.CardBox)pnlActiveCards.Controls[0], computerCard, this.initialAttackDefended);

            }
            else
            {
                // Turn attack back to human player
                playerAttacking = true;
            }

            RealignAllCards();
        }

        #endregion

        #region HELPER METHODS

        /// <summary>
        /// Initiates the gameplay by establishing the first drawn card as the designated trump suit, dealing 6 cards to each player, and declaring the first player to attack
        /// based on which player possesses the lowest ranking trump card
        /// </summary>
        /// ***TODO: Determine first attacker based on lowest trump card in initial hand (rather than assume the player is always the attacked off the bat)*** 
        private void StartGame()
        {
            // shuffle
            mainDeck.Shuffle();

            // seeing the order of the deck in debug console for debugging
            mainDeck.ShowDeck(); // This shows all cards, turn this off when done development
            System.Diagnostics.Debug.WriteLine(mainDeck.ToString());
            try
            {
                InitialDeal();

                PlayingCard firstCard = mainDeck.DrawCard();
                PlayingCard firstPlayableCard = mainDeck.DrawCard();

                this.cbxTrumpCard.Card = firstCard;
                this.cbxDeck.Card = firstPlayableCard;

                // add the trump card back but at the last place in the deck
                mainDeck.AddCardAtBottom(firstCard);

                txtPlayHistory.Text += firstCard.Suit + " is the initial trump suit.";
            }
            catch (IndexOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("Exception catched when trying to draw card out of index.");
            }
        }

        /// <summary>
        /// This called RealignCards() to realign all the cards in the form.
        /// </summary>
        private void RealignAllCards()
        {
            foreach (var control in cardPanels)
            {
                RealignCards(control);
                
            }
        }

        /// <summary>
        /// Switching settings of several objects to show the deck is not out of cards
        /// </summary>
        private void SettingsWithCards()
        {
            lblOutOfCards.Visible = false;
            cbxDeck.Enabled = true;
            cbxTrumpCard.Visible = true;
            lblTrumpCard.Visible = true;
        }

        /// <summary>
        /// Switching settings of cbxDeck to show the deck is out of cards
        /// </summary>
        private void SettingsOutOfCards()
        {
            lblOutOfCards.Visible = true;
            cbxDeck.Enabled = false;
            cbxTrumpCard.Visible = false;
            lblTrumpCard.Visible = false;
        }


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

                for (int i = 0; i < panelHand.Controls.Count; i++)
                {
                    panelHand.Controls[i].Size = normalCardSize;

                }
            }
        }

        /// <summary>
        /// initialDeal - deals players 6 cards to start
        /// </summary>
        /// ***TODO: followup with a function to establish the first attacker based on lowest trump card -> perhaps within this function, or as a separate function***
        private void InitialDeal()
        {
            for (int i = 0; i < 6; i++)
            {
                PlayingCard card = cbxDeck.Card;

                if (card != null) //if card isn't null
                {
                    card.FaceUp = true;

                    //Make it a cardbox for the player
                    CardBox.CardBox playerCardBox = new CardBox.CardBox(card);

                    playerCardBox.Size = normalCardSize;

                    //Wire events
                    WireCardBoxEventHandlers(playerCardBox);
                    //playerCardBox.Click += CardBox_Click; //When the player clicks a card in their hand
                    //click or drag logic here at a later date


                    //Add cardbox to panel
                    pnlPlayerCards.Controls.Add(playerCardBox);
                    cbxDeck.Card = mainDeck.DrawCard();

                    card = cbxDeck.Card;
                    CardBox.CardBox computerCardBox = new CardBox.CardBox(card);

                    computerCardBox.Size = normalCardSize;
                    //Make a cardbox for the computer
                    //card = cbxDeck.Card;
                    //card.FaceUp = true;

                    pnlComputerCards.Controls.Add(computerCardBox);
                    cbxDeck.Card = mainDeck.DrawCard();
                }
            }
            RealignAllCards();
        }

        /// <summary>
        /// RoundDeal - deals both the computer and players card until they have 6 cards in their hand to proceed to the round
        /// </summary>
        private void RoundDeal()
        {
            for (int i = pnlPlayerCards.Controls.Count; i < 6; i++)
            {
                PlayingCard card = cbxDeck.Card;
                card.FaceUp = true;

                pnlPlayerCards.Controls.Add(new CardBox.CardBox(card));
                cbxDeck.Card = mainDeck.DrawCard();
            }
            RealignCards(pnlPlayerCards);

            for (int i = pnlComputerCards.Controls.Count; i < 6; i++)
            {
                PlayingCard card = cbxDeck.Card;
                card.FaceUp = true;
                pnlComputerCards.Controls.Add(new CardBox.CardBox(card));
                cbxDeck.Card = mainDeck.DrawCard();
            }

            while (pnlDefended.Controls.Count > 0)
            {
                pnlDefended.Controls.RemoveAt(0);
            }

            RealignCards(pnlComputerCards);
        }

        /// <summary>
        /// Adds mouse and drag-and-drop events to each cardbox instance
        /// </summary>
        /// <param name="aCardBox">The cardbox that drag-and-drop, as well as click events are to be wired to</param>
        private void WireCardBoxEventHandlers(CardBox.CardBox aCardBox)
        {
            //wire cardbox mouse enter
            aCardBox.MouseEnter += CardBox_MouseEnter;
            //wire cardbox mouse leave
            aCardBox.MouseLeave += CardBox_MouseLeave;

            aCardBox.MouseDown += CardBox_MouseDown;
            aCardBox.DragEnter += CardBox_DragEnter;
            aCardBox.DragDrop += CardBox_DragDrop;
        }


        /// <summary>
        /// Compares the attacking and defending cards and establishes which cards should have their functionality disabled if they are not possible options for moving 
        /// the game forward. Modelled on SuperDurak tutorial referenced in final project outline document.
        /// </summary>
        /// <param name="attackingCard">The CardBox object presented by the attacking player</param>
        /// <param name="defendingCard">The CardBox object presented by the defending player</param>
        /// <param name="initialAttackDefended">A boolean representing the status of the initial attack having been successful defended - significant for disabling of 
        /// invalid card selections within player hand</param>
        private void CompareCards(CardBox.CardBox attackingCard, CardBox.CardBox defendingCard, Boolean initialAttackDefended)
        {
            // Check if the initial attack was successfully defended
            if (initialAttackDefended)
            {
                // Checks if rank of attacking card is eligible based on initial defense status
                // ***TODO: Not sure if we need this, as I believe it is used elsewhere in a more suitable way*** 
                // ***TODO: Unclear on specifics of rules -> just previous defense and attack ranks, or ALL previously played defense/attack ranks***
                if (!attackingCard.Card.Rank.Equals(defendingCard.Card.Rank))
                {
                    MessageBox.Show("Sorry attacker. You can only follow an attack with a card of the same rank as the last defense.");
                    CardBox.CardBox invalidCard = (CardBox.CardBox)pnlActiveCards.Controls[0];
                    pnlActiveCards.Controls.RemoveAt(0);
                    pnlPlayerCards.Controls.Add(invalidCard);

                    return;
                }
            }

            // Establish if successful defense occurs based on greater rank, or event where defense card is a trump and attack is not 
            //(otherwise, if both trump suits, initial rank comparison is already valid)
            //***TODO: Incorporate class library comparison operators with trump card -> currently something miss with deck/card collection constructor where comparison operators aren't
            //         accounting for trumps ***
            if (defendingCard.Card.Rank > attackingCard.Card.Rank || (defendingCard.Card.Suit == cbxTrumpCard.Card.Suit && attackingCard.Card.Suit != cbxTrumpCard.Card.Suit))
            {
                if (playerAttacking)
                {
                    MessageBox.Show("Successfully defended. You may THROW IN, but only with a card with a rank of " + defendingCard.Card.Rank.ToString() + " or " + attackingCard.Card.Rank.ToString());
                    MessageBox.Show("You can also click on the attacker button to PASS THE ATTACK to the computer.");

                    // Put successfully defendeded card into stacks of two in associated panel
                    // Loop through all cards in the players hand and disable any cards outside of those with valid ranks
                    foreach (CardBox.CardBox playedCard in pnlActiveCards.Controls)
                    {
                        txtPlayHistory.Text += Environment.NewLine + " new card going into defended stack of 2 " + playedCard.ToString();
                        //Make it a cardbox for the player
                        CardBox.CardBox defendedCardBox = new CardBox.CardBox(playedCard.Card);

                        defendedCardBox.Size = normalCardSize;

                        //Add cardbox to panel
                        pnlDefended.Controls.Add(defendedCardBox);

                        //RealignDefendedCards();
                        pnlDefended.Controls[0].Top = 38;
                        pnlDefended.Controls[0].Left = 5;
                    }

                    while (pnlActiveCards.Controls.Count > 0)
                    {
                        pnlActiveCards.Controls.RemoveAt(0);
                    }
                }
                else
                {
                    disableInvalidChoices(attackingCard.Card.Rank, defendingCard.Card.Rank);

                    bool playerHasChoices = false;

                    foreach (Control playerCard in pnlPlayerCards.Controls)
                    {
                        if (playerCard.Enabled = true)
                        {
                            playerHasChoices = true;
                        }
                    }

                    // If all the players cards are disabled as invalid choices, inform them of their loss
                    if (playerHasChoices == false)
                    {
                        MessageBox.Show("Computers wins. There's nothing you can do.");
                        MessageBox.Show("You can also click on the attacker button to PASS THE ATTACK to the computer.");
                    }
                    else
                    {
                        MessageBox.Show("Go ahead and defend!");
                    }
                }
                disableInvalidChoices(attackingCard.Card.Rank, defendingCard.Card.Rank);

                this.rankOfLastDefense = defendingCard.Rank;
                initialAttackDefended = true;

                // 


            }
            else
            {
                // Move auto-played computer card from pnlActiveCards back into computer hand before it renders in the form
                // ***TODO: put this logic before comparison - shouldn't be required to move, then move back invalid defense for comparison ***
                if (playerAttacking)
                {
                    CardBox.CardBox invalidCard = new CardBox.CardBox(defendingCard.Card);
                    pnlComputerCards.Controls.Add(invalidCard);
                }
                MessageBox.Show("Attacker wins. ");
            }
        }

        private void RealignDefendedCards()
        {
            for (int i = 0; i < pnlDefended.Controls.Count; i++) { 
                if (i == pnlDefended.Controls.Count - 1)
                {
                    pnlDefended.Controls[i].Left = 500;
                    pnlDefended.Controls[i].Top = 500;
                }                
            }
                
        }

        /// <summary>
        /// Disables all cards in the player's hand that are not eligible for play in the ongoing round
        /// </summary>
        /// <param name="attackingRank">The rank of the last played attack card</param>
        /// <param name="defendingRank">The rank of the last played defense card</param>
        /// ***TODO: Review rules and ensure properly interpreting how eligible cards are figured out (aka just previous two, or ALL played so far in current attack round***
        /// ***TODO: If pursuing multiplayer -> pass in various panels of controls, rather than assume single player panel needs be disabled
        private void disableInvalidChoices(CardRank attackingRank, CardRank defendingRank)
        {
            bool playableCard = false;
            // Loop through all cards in the players hand and disable any cards outside of those with valid ranks
            foreach (CardBox.CardBox playerCard in pnlPlayerCards.Controls)
            {
                if (attackingRank == playerCard.Card.Rank || defendingRank == playerCard.Card.Rank)
                {
                    playerCard.Enabled = true;
                    playableCard = true;
                }
                else
                {
                    playerCard.Enabled = false;
                }
            }

            if(playableCard == false)
            {
                MessageBox.Show("You are out of options for attacking. Click button to end your turn as attacker.");
                btnStopAttacking.Text = "END TURN";
            }
        }

        /// <summary>
        /// Re-enables potentially disabled controls where an attack is over and all cards are to be re-assigned their event handlers
        /// </summary>
        private void reenableAllCards()
        {
            foreach (CardBox.CardBox playerCard in pnlPlayerCards.Controls)
            {
                playerCard.Enabled = true;
                WireCardBoxEventHandlers(playerCard);
            }
        }

        #endregion

        #region EMPTY EVENT HANDLERS

        private void lblOutOfCards_Click(object sender, EventArgs e)
        {

        }

        private void cbxDeck_Load(object sender, EventArgs e)
        {

        }

        private void lblClickedState_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region AI LOGIC

        /// <summary>
        /// Basic algorithm to determine best choice, based on lowest value card that can be opposing hand. If no options exist, defaults to index 9.
        /// </summary>
        /// <param name="computerHand">The computer's current hand that the best possible choice is being determines from</param>
        /// <returns>bestChoiceIndex - the best possible choice the computer could make by considering entire hand.</returns>
        /// ***TODO: add select statement corresponding to level of difficulty selected by player at the start of the game (Possibly in the same form menu where the 
        ///          choice to use log functionality is displayed????????????***
        /// ***TODO: remove all debug code (unless we think it would be cool to push it to a file to log the AI/computer's selection process - likely not!**** 
        protected int determineBestPlay(Panel computerHand)
        {
            bool noGoodChoice = true;
            // Keep track of best player choice based on nearest winnable rank to attacking card
            int idealChoiceIndex = 0;
            CardBox.CardBox idealChoice  = computerHand.Controls[idealChoiceIndex] as CardBox.CardBox;
            
            CardBox.CardBox cardToBeat = pnlActiveCards.Controls[pnlActiveCards.Controls.Count - 1] as CardBox.CardBox;

            txtPlayHistory.Text += "Computer is considering it's best choice.";                    
            txtPlayHistory.Text += computerHand.ToString();
            // See what is being retrieved from the computer's hand of cards
            for (int i = 0; i < computerHand.Controls.Count; i++)
            {                
                // Ensure only CardBox instances are being compared to the player's selected card
                if (computerHand.Controls[i].GetType().ToString().Contains("CardBox"))
                {
                    CardBox.CardBox currentCard = computerHand.Controls[i] as CardBox.CardBox;

                    // Compare by rank foremost, unless case where computer possesses a trump suited card and they are up against a non-trump card
                    if ((currentCard.Card.Rank > cardToBeat.Card.Rank) || (currentCard.Card.Suit == cbxTrumpCard.Card.Suit && (cardToBeat.Card.Suit != cbxTrumpCard.Card.Suit)))
                    {
                        noGoodChoice = false;
                        txtPlayHistory.Text += Environment.NewLine + currentCard.Rank + " of " + currentCard.Suit + " could win against the opponent's " + cardToBeat.Rank + " of " + cardToBeat.Suit;

                        // Check to see if option to beat player card is a more efficient (AKA lower value card that current selection) way to beat the opponent and reserve high ranking cards for later
                        if (((int)currentCard.Card.Rank) < (int)idealChoice.Card.Rank)
                        {
                            idealChoiceIndex = i;
                        }
                    }
                }
            }

            // If the computer has no cards prospective to attack or defend, admit defeat and pass the attack or return value that will cause computer to 
            // take the discarded cards
            if (noGoodChoice == false)
            {
                txtPlayHistory.Text += idealChoiceIndex + " is the index a wise AI would choose here.";
            }
            else 
            { 
                txtPlayHistory.Text += "AI has no good choices. Human player is wins this one!";
                return -1;
            }              

            return idealChoiceIndex;
        }

        #endregion

    }
}
