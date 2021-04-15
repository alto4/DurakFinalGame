/*@project          OOPFinal Project
 *@file             Durak.cs 
 *@version          1.0 
 *@since            2021-03-04 
 *@author           Eduardo San Martin Celi, Scott Alton, Nick Sturch-Flint
 *@description      This is the main game logic and event handling for a game of Durak.
 */

using CardLib;
using ControlsLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Durak
{
    public partial class frmGame : Form
    {

        #region FIELDS AND PROPERTIES
        StreamWriter logs = new StreamWriter("logs.txt");

        //String used to reference the player in the logs and stats
        string playerName;
        //Card and Deck Specifics Declarations
        int sizeChoice;
        // generate PlayingCard objects from a Deck
        Deck mainDeck;
        // enlarge a card by this value
        private const int ENLARGE = 35;
        // The default size of a card
        static private Size normalCardSize = new Size(100, 135);
        // makes card draggable
        private CardBox.CardBox dragCard;
        // to collect all of the card panels
        List<Panel> cardPanels = new List<Panel>();
        CardRank rankOfLastDefense;
        bool playerAttacking = true;
        bool initialAttackDefended = false;

        // string to display who has the first turn
        string firstTurn = "";
        // to reference the current attacking card
        PlayingCard attackingCard = new PlayingCard();
        // to reference the current defending card
        PlayingCard defendingCard = new PlayingCard();
        // to reference players current hand
        CardCollection playerHand = new CardCollection();
        // to reference AIs current hand
        CardCollection AIHand = new CardCollection();
        // Cards than have been played this turn
        CardCollection cardsPlayedThisTurn = new CardCollection();

        #endregion

        #region FORM AND STATIC CONTROL EVENT HANDLERS 
        /// <summary>
        /// Constructor for the main form
        /// </summary>
        public frmGame()
        {
            InitializeComponent();
            playerName = "Player 1";
            StatsPlayer currentPlayer = new StatsPlayer(playerName);
        }

        public frmGame(string name)
        {
            InitializeComponent();

            playerName = name;

            StatsPlayer currentPlayer = StatsPlayer.SearchForExistingUser(name);

        }

        /// <summary>
        /// When the main form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmGame_Load(object sender, EventArgs e)
        {
            
            // Get the players choice of deck size
            frmSelectDeckSize choiceSelect = new frmSelectDeckSize();
            choiceSelect.ShowDialog();
            sizeChoice = choiceSelect.GetSizeChoice();
                   
            string timestamp = DateTime.Now.ToString();
            logs.WriteLine("The current time is: " + timestamp + Environment.NewLine);
            logs.WriteLine("Player chose a deck of " + choiceSelect.GetSizeChoice() + " cards.");
     
            choiceSelect.Close();
            mainDeck = new Deck((SizeOfDecks) sizeChoice);

            // adding all of the card panels to a list
            cardPanels.Add(pnlActiveCards);
            cardPanels.Add(pnlComputerCards);
            cardPanels.Add(pnlPlayerCards);
            cardPanels.Add(pnlDefended);

            StartGame();

            //Testing StatsPlayer Methods
            logs.WriteLine("Player has started the game");   

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
        private void cbxDeck_OutOfCards(object sender, EventArgs e)
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

                        logs.WriteLine("Players plays " + dragCard.ToString());

                        RealignCards(thisPanel);
                        RealignCards(fromPanel);
                        /************************ATTACKING PLAYER LOGIC**************************************/
                        // Check if the player is attacking or defending, then trigger the appropriate events
                        if (playerAttacking)
                        {
                            ComputerDefends();
                        }
                        /************************DEFENDING PLAYER LOGIC**************************************/
                        else if (playerAttacking == false) 
                        {
                            MoveCards(pnlActiveCards, pnlDefended);

                            CardBox.CardBox attackCard = (CardBox.CardBox)pnlDefended.Controls[0];
                            CardBox.CardBox defenseCard = (CardBox.CardBox)pnlDefended.Controls[1];

                            ComputerSuccessiveAttacks(attackCard, defenseCard);
                        }
                    }
                }
                RealignAllCards();
                UpdateDefendedAndDiscardPanelControls();
            }
        }
        #endregion

        #region CARDBOX EVENT HANDLERS

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

        private void btnEndTurn_Click(object sender, EventArgs e)
        {
            // if the player chooses to stop defending
            if (!playerAttacking)
            {
                logs.WriteLine("Player has accepted defeat, switching turns.");
                MoveCards(pnlActiveCards, pnlPlayerCards);
                MoveCards(pnlDefended, pnlPlayerCards);

                // Flip discarded pile to facedown without flipping every card
                UpdateDefendedAndDiscardPanelControls();
                RoundDeal(); //Deal cards until both players have at least 6 cards
                ReenableAllCards(); //Allows the player to use their cards again

                txtComputerAttacker.Visible = false; //Flash the image that shows the computer is attacking
                playerAttacking = true; //mark that the computers attack is starting

                //Increment losses for game stats
                
            }
            else //if the player chooses to stop attacking
            {
                logs.WriteLine("Player has chosen to hault the attack.");
                MoveCards(pnlActiveCards, pnlDiscard); //Move cards from the active panel to discard
                MoveCards(pnlDefended, pnlDiscard); //Move cards from the defended panel to discard

                

                // Flip discarded pile to facedown without flipping every card
                UpdateDefendedAndDiscardPanelControls();
                RoundDeal(); //Deal cards until both players have at least 6 cards
                ReenableAllCards(); //Allows the player to use their cards again

                txtComputerAttacker.Visible = true; //Flash the image that shows the computer is attacking
                playerAttacking = false; //mark that the computers attack is starting

                ComputerAttacks(); //proceed with the computer attack
            }
           //DisableInvalidCardsInHands(); //Determine what the player can defend with

            RealignAllCards();
            //DisableInvalidPlayerDefenseChoices(computerCard);
            //ReenableAllCards();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ComputerAttacks()
        {
            Random rand = new Random(); //initialize a random object
            int computerChoiceIndex = rand.Next(pnlComputerCards.Controls.Count); //generates a random number between 0 and the number of cards the computer has

            ReenableAllCards();

            if (computerChoiceIndex >= 0)
            {
                CardBox.CardBox computerCard = pnlComputerCards.Controls[computerChoiceIndex] as CardBox.CardBox; //create a copy of the card object
                computerCard.FaceUp = true;

                
                logs.WriteLine("Computer Plays " + computerCard.ToString() + " as an attack");
                pnlComputerCards.Controls.Remove(computerCard); //remove the card from the computers hand
                pnlActiveCards.Controls.Add(computerCard);      //place the card into the active play panel

                if(playerAttacking == false)
                {
                    ReenableAllCards();
                    DisableInvalidPlayerDefenseChoices(computerCard);
                }                
            }

            RealignAllCards();
            UpdateDefendedAndDiscardPanelControls();
        }

        /// <summary>
        /// Computers successive attack movements after initial attack is successfully defended
        /// </summary>
        /// <param name="card1"></param>
        /// <param name="card2"></param>
        private void ComputerSuccessiveAttacks(CardBox.CardBox card1, CardBox.CardBox card2)
        {
            Dictionary<int, CardBox.CardBox> validCards = new Dictionary<int, CardBox.CardBox>();
            int cardIndex = 0;
            for (int i = 1; i < pnlComputerCards.Controls.Count; i++)
            {
                CardBox.CardBox tempCard = (CardBox.CardBox)pnlComputerCards.Controls[i];

                if (tempCard.Rank == card1.Rank || tempCard.Rank == card2.Rank)
                {
                    validCards.Add(i, tempCard);
                    cardIndex = i;
                }
            }

            if (validCards.Count == 0)
            {
                logs.WriteLine("Computer can no longer attacks. Switching turns.");
                MoveCards(pnlDefended, pnlDiscard);
                RoundDeal();

                txtComputerAttacker.Visible = false;
               
                playerAttacking = true;
                ReenableAllCards();
            }
             //generates a random number between 0 and the number of cards the computer has
            if (playerAttacking == false)
            {
                CardBox.CardBox computerCard = validCards[cardIndex];
                logs.WriteLine("Computer attacks with " + computerCard.ToString());
                pnlComputerCards.Controls.Remove(computerCard); //remove the card from the computers hand
                pnlActiveCards.Controls.Add(computerCard);      //place the card into the active play panel

                Wait(1500);

                DisableInvalidPlayerDefenseChoices(computerCard);
            }
            UpdateDefendedAndDiscardPanelControls();
            DisableInvalidCardsInHands();
        }

        #endregion

        #region HELPER METHODS

        /// <summary>
        /// Checks if someone won yet
        /// </summary>
        private void CheckIfWon()
        {
            // TODO: add logs implementation here
            if (pnlPlayerCards.Controls.Count == 0 && mainDeck.Size == 0)
            {
                MessageBox.Show("Congratulations! You won.");
                if (logs.BaseStream != null)
                {
                    logs.WriteLine("Player Won the Game!");
                    logs.Close();
                }
                // hidding frmGame
                this.Hide();

                // new frmMainMenu instance
                frmMainMenu mainMenu = new frmMainMenu();

                // show the frmMainMenu form
                mainMenu.ShowDialog();
                Application.Exit(); // close frmGame
                
            }
            if (pnlComputerCards.Controls.Count == 0 && mainDeck.Size == 0)
            {
                MessageBox.Show("Sorry! You lost.");
                if (logs.BaseStream != null)
                {
                    logs.WriteLine("Player Lost the Game! Sorry!");
                    logs.Close();
                }
                // hidding frmGame
                this.Hide();

                // new frmMainMenu instance
                frmMainMenu mainMenu = new frmMainMenu();

                // show the frmMainMenu form
                //this.close() // close frmGame
                mainMenu.ShowDialog();
                Application.Exit();  
            }
        }

        /// <summary>
        /// This method disables all cards in the players cards that are lower
        /// than the current AIs attacking card. Also it disables cards that have
        /// not been played yet
        /// </summary>
        private void DisableInvalidCardsInHands()
        {
            if (!playerAttacking)
            {
                // start with all disabled cards
                foreach (CardBox.CardBox cardBox in pnlPlayerCards.Controls)
                {
                    cardBox.Enabled = false;
                }
                // start with empty collection
                cardsPlayedThisTurn.Clear();


                if (pnlActiveCards.Controls.Count > 0)
                {

                    attackingCard = ((CardBox.CardBox)pnlActiveCards.Controls[0]).Card;

                    // add the defended cards to collection
                    foreach (CardBox.CardBox cardBox in pnlDefended.Controls)
                    {
                        // add the defended cards
                        cardsPlayedThisTurn.Add(cardBox.Card);
                    }

                    // enable players cards in the cardsPlayedThisTurn collection
                    foreach (CardBox.CardBox cardBox in pnlPlayerCards.Controls)
                    {
                        // if the collection has this card then enable it in the players hand
                        if (cardsPlayedThisTurn.Contains(cardBox.Card) && cardBox.Card > attackingCard)
                            cardBox.Enabled = true;
                    }

                    // enable player cards higher than the current attacking card
                    foreach (CardBox.CardBox cardBox in pnlPlayerCards.Controls)
                    {
                        defendingCard = cardBox.Card;

                        // trump card or higher ranked card in same suit
                        if (defendingCard > attackingCard)
                        {
                            //MessageBox.Show(cardBox.Card + " is greater than " + attackingCard);
                            cardBox.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                foreach (CardBox.CardBox cardBox in pnlPlayerCards.Controls)
                {
                    cardBox.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Aligns the discarded cards to be in the center of the panel and 
        /// make defended cards elargeable
        /// </summary>
        private void UpdateDefendedAndDiscardPanelControls()
        {
            // make defended cards be able to get enlarged
            foreach (CardBox.CardBox cardBox in pnlDefended.Controls)
            {
                cardBox.Enabled = true;
                cardBox.MouseEnter += CardBox_MouseEnter;
                cardBox.MouseLeave += CardBox_MouseLeave;
            }

            // makes discarded cards go in the middle of the panel and flip over
            foreach (CardBox.CardBox cardBox in pnlDiscard.Controls)
            {
                cardBox.FaceUp = !cbxDeck.FaceUp;
                cardBox.Left = (pnlDiscard.Width - normalCardSize.Width) / 2;
                cardBox.Top = (pnlDiscard.Height - normalCardSize.Height) / 2;
            }

            //ReenableAllCards();
        }

        /// <summary>
        /// Refreshes the logic decks each time there is an action.
        /// </summary>
        private void RefreshLogicDeckFromPanels()
        {
            AIHand.Clear();
            playerHand.Clear();

            foreach(CardBox.CardBox card in pnlComputerCards.Controls)
            {
                AIHand.Add(card.Card);
            }

            foreach (CardBox.CardBox card in pnlPlayerCards.Controls)
            {
                playerHand.Add(card.Card);
            }
        }

        /// <summary>
        /// Initiates the gameplay by establishing the first drawn card as the designated trump suit, 
        /// dealing 6 cards to each player, and declaring the first player to attack
        /// based on which player possesses the lowest ranking trump card
        /// </summary>
        /// ***TODO: Determine first attacker based on lowest trump card in initial hand 
        /// (rather than assume the player is always the attacked off the bat)*** 
        private void StartGame()
        {
            cbxDeck.FaceUp = false;
            // shuffle
            mainDeck.Shuffle();

            // seeing the order of the deck in debug console for debugging
            //mainDeck.ShowDeck(); // This shows all cards, turn this off when done development
            System.Diagnostics.Debug.WriteLine(mainDeck.ToString());
            try
            {
                // The 14th card will be the trump
                cbxTrumpCard.Card = mainDeck.GetCard(13);
                System.Diagnostics.Debug.WriteLine(cbxTrumpCard.Card.ToString());
                PlayingCard.trumpSuit = cbxTrumpCard.Card.Suit;

                InitialDeal();

                PlayingCard firstCard = mainDeck.DrawCard();

                cbxTrumpCard.Card = firstCard; // Moving the trump card to bottom

                // add the trump card back but at the last place in the deck
                mainDeck.AddCardAtBottom(firstCard);
            }
            catch (IndexOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("Exception caught when trying to draw card out of index.");
            }
        }

        /// <summary>
        /// This called RealignCards() to realign all the cards in the form.
        /// </summary>
        private void RealignAllCards()
        {
            // refresh the logic cards
            RefreshLogicDeckFromPanels();
            // make sure all players cards are up side
            foreach(CardBox.CardBox cardBox in pnlPlayerCards.Controls)
            {
                cardBox.FaceUp = true;
            }

            // make sure discarded cards are facing down
            foreach (CardBox.CardBox cardBox in pnlDiscard.Controls)
            {
                cardBox.FaceUp = false;
            }

            foreach (var control in cardPanels)
            {
                RealignCards(control);
            }

            //ReenableAllCards();
        }

        /// <summary>
        /// Switching settings of several objects to show the deck is not out of cards
        /// </summary>
        private void SettingsWithCards()
        {
            cbxDeck.FaceUp = false;
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
           
            cbxDeck.FaceUp = false;
            lblOutOfCards.Visible = true;
            cbxDeck.Enabled = false;
            cbxTrumpCard.Visible = false;
            lblTrumpCard.Text = "Trump Suit: " + cbxTrumpCard.Card.Suit.ToString();
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
        private void InitialDeal()
        {
            cbxDeck.FaceUp = false;

            PlayingCard playersCard;
            PlayingCard AIsCard;
            PlayingCard lowestCard = new PlayingCard(PlayingCard.trumpSuit, CardRank.Ace); // start as the highest possible card

            // setting the first card
            cbxDeck.Card = mainDeck.GetCard(0);
            mainDeck.DrawCard();
            logs.WriteLine("Initial Deal Starting: \n");
            for (int i = 0; i < 6; i++)
            {
                PlayingCard card = cbxDeck.Card;

                if (card != null) //if card isn't null
                {
                    card.FaceUp = true;

                    //Make it a cardbox for the player
                    CardBox.CardBox playerCardBox = new CardBox.CardBox(card);
                    playersCard = card;

                    playerCardBox.Size = normalCardSize;

                    //Wire events
                    WireCardBoxEventHandlers(playerCardBox);
                    //playerCardBox.Click += CardBox_Click; //When the player clicks a card in their hand

                    //Add cardbox to panel
                    pnlPlayerCards.Controls.Add(playerCardBox);
                    logs.WriteLine("Player draws " + playerCardBox.ToString());
                    cbxDeck.Card = mainDeck.DrawCard();

                    card = cbxDeck.Card;
                    CardBox.CardBox computerCardBox = new CardBox.CardBox(card);
                    AIsCard = card;

                    computerCardBox.Size = normalCardSize;
                    //Make a cardbox for the computer
                    pnlComputerCards.Controls.Add(computerCardBox);

                    PlayingCard cardToLog = computerCardBox.Card;
                    cardToLog.FaceUp = true;

                    logs.WriteLine("Computer draws " + cardToLog.ToString());
                    cbxDeck.Card = mainDeck.DrawCard();

                    // determine who has the lowest trump card
                    // If no entity has a trump card then the player goes first
                    if (AIsCard < lowestCard && AIsCard.Suit == PlayingCard.trumpSuit)
                    {
                        lowestCard = AIsCard;
                        firstTurn = "the AI";
                    }
                    if (playersCard < lowestCard && playersCard.Suit == PlayingCard.trumpSuit)
                    {
                        lowestCard = playersCard;
                        firstTurn = "the player";
                    }
                }
            }

            ReenableAllCards();
            RealignAllCards();
        }

        /// <summary>
        /// RoundDeal - deals both the computer and players card until they have 6 cards in their hand to proceed to the round
        /// </summary>
        private void RoundDeal()
        {
            RefreshLogicDeckFromPanels();

            PlayingCard card = cbxDeck.Card;

            try
            {
                // alternate handing cards until deck is empty or both players have 6 cards.
                while (mainDeck.Size > 0)
                {
                    // break unless player has less than 5 cards
                    if (playerHand.Count <= 5)
                    {
                        card = cbxDeck.Card;
                        card.FaceUp = true;
                        // add players card\
                        logs.WriteLine("Player Draws a " + card.ToString());
                        pnlPlayerCards.Controls.Add(new CardBox.CardBox(card));
                        playerHand.Add(card);
                        cbxDeck.Card = mainDeck.DrawCard();
                    }
                    if (playerHand.Count >= 6 && AIHand.Count >= 6)
                    {
                        break;
                    }

                    // break unless ai has less than 5 cards
                    if (AIHand.Count <= 5 && mainDeck.Size > 0)
                    {
                        card = cbxDeck.Card;
                        card.FaceUp = true; // NOTE: change this for only player when done dev
                                            // add AIs card
                        logs.WriteLine("Computer Draws a " + card.ToString());
                        pnlComputerCards.Controls.Add(new CardBox.CardBox(card));
                        AIHand.Add(card);
                        cbxDeck.Card = mainDeck.DrawCard();
                        
                    }
                    if (playerHand.Count >= 6 && AIHand.Count >= 6)
                    {
                        break;
                    }
                }

                if (mainDeck.Size == 0 && cbxTrumpCard.Visible != false)
                {
                    if (playerHand.Count < 6)
                    {
                        card = cbxTrumpCard.Card;
                        card.FaceUp = true;
                        // add trump card to players hand
                        logs.WriteLine("Player Draws a Trump Card of " + card.ToString());
                        pnlPlayerCards.Controls.Add(new CardBox.CardBox(card));
                        playerHand.Add(card);
                        cbxDeck.Card = mainDeck.DrawCard();
                    }
                    else if (AIHand.Count < 6)
                    {
                        card = cbxTrumpCard.Card;
                        card.FaceUp = true;
                        // add trump card to players hand
                        logs.WriteLine("Computer Draws a Trump Card of " + card.ToString());

                        card.FaceUp = false;
                        pnlComputerCards.Controls.Add(new CardBox.CardBox(card));
                        AIHand.Add(card);
                        cbxDeck.Card = mainDeck.DrawCard();
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                System.Diagnostics.Debug.WriteLine("Out of cards when drawing");
                SettingsOutOfCards();
            }

            while (pnlDefended.Controls.Count > 0)
                pnlDefended.Controls.RemoveAt(0);
            
            RealignAllCards();
            ReenableAllCards();
            UpdateDefendedAndDiscardPanelControls();
        }

        /// <summary>
        /// Adds mouse and drag-and-drop events to each cardbox instance
        /// </summary>
        /// <param name="aCardBox">The cardbox that drag-and-drop, as well as click events are to be wired to</param>
        /// TODO: If there are any other events to be wired, add them here (GOOD FOR NOW)
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

        //  private void CanPlayerContinueAttack(Panel playerPanel, CardBox.CardBox)

        /// <summary>
        /// Compares the attacking and defending cards and establishes which cards should have their functionality disabled if they are not possible options for moving 
        /// the game forward. Modelled on SuperDurak tutorial referenced in final project outline document.
        /// </summary>
        /// <param name="attackingCard">The CardBox object presented by the attacking player</param>
        /// <param name="defendingCard">The CardBox object presented by the defending player</param>
        /// <param name="initialAttackDefended">A boolean representing the status of the initial attack having been successful defended - significant for disabling of 
        /// invalid card selections within player hand</param>
        private void CompareCards(CardBox.CardBox attackingCard, CardBox.CardBox defendingCard, bool initialAttackDefended)
        {
                if (playerAttacking)
                {
                    this.initialAttackDefended = true;
                    MoveCards(pnlActiveCards, pnlDefended); //Move cards from active panel to successfully defended panel
                }
                else
                {
                    if (playerAttacking == false)
                    {
                        DisableInvalidPlayerDefenseChoices(attackingCard);
                    }
                    bool playerHasChoices = false;

                    foreach (Control playerCard in pnlPlayerCards.Controls)
                    {
                        if (playerCard.Enabled == true)
                        {
                            playerHasChoices = true;
                        }
                    }

                    // If all the players cards are disabled as invalid choices, inform them of their loss
                    if (playerHasChoices == false)
                    {
                        MoveCards(pnlDefended, pnlPlayerCards);
                        MoveCards(pnlActiveCards, pnlPlayerCards);
                        RoundDeal(); //deal back to 6 cards
                        txtComputerAttacker.Visible = false;
                        playerAttacking = true;
                        
                        Wait(1500);
                        
                    }
                }
                disableInvalidChoices(attackingCard.Card.Rank, defendingCard.Card.Rank);
                rankOfLastDefense = defendingCard.Rank;
        }//end of COMPARECARDS

        private void DisableInvalidPlayerDefenseChoices(CardBox.CardBox attack)
        {
            ReenableAllCards();
            int counter = 0;
            int numberOfPlayerCards = pnlPlayerCards.Controls.Count;
            foreach (CardBox.CardBox playerCard in pnlPlayerCards.Controls)
            {
                //MessageBox.Show(playerCard.Card.ToString());
                if (playerCard.Card < attack.Card)
                {
                    //MessageBox.Show((playerCard.Card < attack.Card).ToString());
                    playerCard.Enabled = false;
                    counter++;
                }
            }

            if (counter == numberOfPlayerCards) //player cannot defend against the attacking card
            {
                //MessageBox.Show("Player cannot defend. Picking up cards");
                if (pnlDefended.Controls.Count > 0)
                {
                    MoveCards(pnlDefended, pnlPlayerCards);
                }
                
                MoveCards(pnlActiveCards, pnlPlayerCards);

                txtComputerAttacker.Visible = false;
                playerAttacking = true;

                RoundDeal();
                ReenableAllCards();
            }
        }

        /// <summary>
        /// Disables all cards in the player's hand that are not eligible for play in the ongoing round
        /// </summary>
        /// <param name="attackingRank">The rank of the last played attack card</param>
        /// <param name="defendingRank">The rank of the last played defense card</param>
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
        }
      
        /// <summary>
        /// Re-enables potentially disabled controls where an attack is over and all cards are to be re-assigned their event handlers
        /// </summary>
        private void ReenableAllCards()
        {
            cbxTrumpCard.FaceUp = true;

            CheckIfWon();

            foreach (CardBox.CardBox playerCard in pnlPlayerCards.Controls)
            {
                playerCard.Enabled = true;
                WireCardBoxEventHandlers(playerCard);
            }

            // Set all computer cards to be face down
            foreach (CardBox.CardBox computerCard in pnlComputerCards.Controls)
            {
                computerCard.FaceUp = false;
            }

            // Set all defended cards to be face up
            foreach (CardBox.CardBox defendedCard in pnlDefended.Controls)
            {
                defendedCard.FaceUp = true;  
            }

            // Set all discarded cards to be face down
            foreach (CardBox.CardBox discardedCard in pnlDiscard.Controls)
            {
                discardedCard.FaceUp = false;
            }

            // Set all discarded cards to be face down
            foreach (CardBox.CardBox activeCard in pnlActiveCards.Controls)
            {
                activeCard.FaceUp = true;
            }
        }

        /// <summary>
        /// This helper function will move all the cards from one panel to another. Used for moving active, or successfully defended cards to the discard pile. Or using it to
        /// move the active cards to a failed defense players hand.
        /// </summary>
        /// <param name="panelWithCards">The panel where the cards currently reside</param>
        /// <param name="panelCardsGoTo">The panel we want the cards to move to</param>
        private void MoveCards(Panel panelWithCards, Panel panelCardsGoTo)
        {
            foreach (CardBox.CardBox card in panelWithCards.Controls)
            {
                
                //Make it a cardbox for the player
                CardBox.CardBox movedCard = new CardBox.CardBox(card.Card);
                movedCard.Size = normalCardSize; //resize the card, in case it was already the appropriate size

                //Add cardbox to panel
                panelCardsGoTo.Controls.Add(movedCard);

                //RealignDefendedCards();
                panelCardsGoTo.Controls[0].Top = 38;
                panelCardsGoTo.Controls[0].Left = 5;
            }
            //remove the cards from the active panel
            while (panelWithCards.Controls.Count > 0)
            {
                panelWithCards.Controls.RemoveAt(0);
            }
            ReenableAllCards();
        }

        //TODO: Create a function that will move a single card from one panel to another
        // will look something like:
        // private void MoveACard(PlayingCard cardToBeMoved, Panel panelWithCard, Panel panelCardGoesTo)
        // then within the function it should act the same as the way we move cards everywhere else but instead of the specific panel names, we use the inserts to call them

        #endregion

        #region EMPTY EVENT HANDLERS

        private void frmGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (logs.BaseStream != null)
            {
                logs.WriteLine("Player closed the game");
                logs.Close();
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {

        }

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
        protected int determineBestPlay(Panel computerHand)
        {
            bool noGoodChoice = true;
            // Keep track of best player choice based on nearest winnable rank to attacking card
            int idealChoiceIndex = 0;
            
            CardBox.CardBox cardToBeat = pnlActiveCards.Controls[pnlActiveCards.Controls.Count - 1] as CardBox.CardBox;

            // See what is being retrieved from the computer's hand of cards
            for (int i = 0; i < computerHand.Controls.Count; i++)
            {
                CardBox.CardBox idealChoice = computerHand.Controls[idealChoiceIndex] as CardBox.CardBox;
       
                // Ensure only CardBox instances are being compared to the player's selected card
                if (computerHand.Controls[i].GetType().ToString().Contains("CardBox"))
                {
                    CardBox.CardBox currentCard = computerHand.Controls[i] as CardBox.CardBox;

                    if (currentCard.Card > cardToBeat.Card)
                    { //current card wins
                        noGoodChoice = false;

                        // Check to see if option to beat player card is a more efficient (AKA lower value card that current selection) way to beat the opponent and reserve high ranking cards for later
                        if (currentCard.Card < idealChoice.Card)
                        {
                            idealChoiceIndex = i;
                        }


                        idealChoiceIndex = i;
                    }
                    //else if (cardToBeat.Card.Suit == cbxTrumpCard.Card.Suit && (currentCard.Card.Suit != cbxTrumpCard.Card.Suit))
                    //{ //cardto beat wins

                    //    txtPlayHistory.Text += Environment.NewLine + currentCard.Rank + " of " + currentCard.Suit + " CANNOT win against the opponent's " + cardToBeat.Rank + " of " + cardToBeat.Suit;
                    //    //DELETE FOR SUBMISSION
                    //}
                    else //if neither cards are trump, or both cards are
                    {                       

                        if (currentCard.Card > cardToBeat.Card)  //win
                        {
                            noGoodChoice = false;
                           // txtPlayHistory.Text += Environment.NewLine + currentCard.Rank + " of " + currentCard.Suit + " could win against the opponent's " + cardToBeat.Rank + " of " + cardToBeat.Suit
                           //                    + "   " + i + "  " + pnlComputerCards.Controls.IndexOf(currentCard).ToString(); //DELETE FOR SUBMISSION
                            idealChoiceIndex = i;
                            //end players turn

                        }
                    }                    
                }
            }//end of loop

            // If the computer has no cards prospective to attack or defend, admit defeat and pass the attack or return value that will cause computer to 
            // take the discarded cards
            if (noGoodChoice) //computer wins
            {
                logs.WriteLine("Computer cannot defend, player wins the attack");

                txtComputerAttacker.Visible = true;
                playerAttacking = false;
                RoundDeal();
                ReenableAllCards();
                return -1;
            }

            return idealChoiceIndex;
        }

        private void ComputerDefends()
        {
            //AI DEFENSE Logic
            // AI Function to determine best card
            int computerChoiceIndex = determineBestPlay(pnlComputerCards);

            if (computerChoiceIndex >= 0) //Computer Can Defend
            {
                CardBox.CardBox computerCardBox = pnlComputerCards.Controls[computerChoiceIndex] as CardBox.CardBox;
                pnlComputerCards.Controls.Remove(computerCardBox); //remove from computers hand
                pnlActiveCards.Controls.Add(computerCardBox);      //add to the active play panel

                PlayingCard card = computerCardBox.Card;
                card.FaceUp = true;

                logs.WriteLine("Computer responds with " + card.ToString());

                //Compares cards in players hands, determines if they can attack again by comparing their hand to pair in active panel
                CardBox.CardBox tempCard = (CardBox.CardBox)pnlActiveCards.Controls[0];
           
                CompareCards(tempCard, computerCardBox, this.initialAttackDefended); //deciding which cards can be played on a successive attack
                ReenableAllCards();
                disableInvalidChoices(tempCard.Rank, computerCardBox.Card.Rank);

                
            }
            else //Computer Cannot Defend
            {
                MoveCards(pnlDefended, pnlComputerCards);
                MoveCards(pnlActiveCards, pnlComputerCards);                

                ReenableAllCards();
                RealignAllCards();
                txtComputerAttacker.Visible = true;
                playerAttacking = false;

                ComputerAttacks();
            }
            //DisableInvalidCardsInHands();
        }

        #endregion

        /// <summary>
        /// Creates a timer that delays UI activities by the specified amount of time
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <citation>
        ///     <source>https://stackoverflow.com/questions/10458118/wait-one-second-in-running-program</source>
        ///     <author>AustinWBryan</author>
        ///     <description>This code was taken from Stack Overflow for easy implementation of a timer in the UI</description>
        /// </citation>
        public void Wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private void cbxDeck_Click(object sender, EventArgs e)
        {

        }
    }
}
