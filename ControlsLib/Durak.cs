﻿using CardLib;
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

        /*
         * Declare a global streamreader and stream writer, then update the 
         * txtPlayHistory gameplay recordings to write to our log file instead of the
         * txtPlayHistory textbox
         */
        
        

        
        
        


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

            txtPlayHistory.Text += currentPlayer.ToString();
            //Dictionary<string, StatsPlayer> allPlayers = new Dictionary<string, StatsPlayer>();
            //List<StatsPlayer> tempAllPlayers = StatsPlayer.CreatePlayerList();

            //for (int i = 0; i < tempAllPlayers.Count; i++)
            //{
            //    allPlayers.Add(tempAllPlayers[i].getPlayerName(), tempAllPlayers[i]);
            //}


            //if (allPlayers.ContainsKey(name)) //this username already exists
            //{
            //    StatsPlayer currentPlayer = allPlayers[name];
            //    txtPlayHistory.Text += currentPlayer.ToString();
            //}
            //else //first time player
            //{
            //    StatsPlayer currentPlayer = new StatsPlayer(name);
            //    txtPlayHistory.Text += currentPlayer.ToString();
            //}
        }

        /// <summary>
        /// When the main form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmGame_Load(object sender, EventArgs e)
        {
            string logPath = ControlsLib.Properties.Resources.logs;
            StreamWriter logs = new StreamWriter("logs.txt");
            //ResourceWriter logs = new ResourceWriter("logs.txt");
            

            // Get the players choice of deck size
            frmSelectDeckSize choiceSelect = new frmSelectDeckSize();
            choiceSelect.ShowDialog();
            sizeChoice = choiceSelect.GetSizeChoice();

            //THIS IS THE CODE THAT WRITES TO THE FILE WHICH CAN BE FOUND IN THE Durak/bin/Debug folder in the project itself
            using (logs)
            {
                logs.WriteLine("Player chose a deck of " + choiceSelect.GetSizeChoice() + " cards.");
            }

            choiceSelect.Close();
            mainDeck = new Deck((SizeOfDecks) sizeChoice);

            // adding all of the card panels to a list
            cardPanels.Add(pnlActiveCards);
            cardPanels.Add(pnlComputerCards);
            cardPanels.Add(pnlPlayerCards);
            cardPanels.Add(pnlDefended);

            StartGame();

            //Testing StatsPlayer Methods
            txtPlayHistory.Text += Environment.NewLine + "! Welcome to the game, " + playerName + "!";


        }

        /// <summary>
        /// When the user clicks on the deck
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDeck_Click(object sender, EventArgs e)
        {

            lblClickedState.Text = cbxDeck.ToString() + " was last clicked." + Environment.NewLine;

            if (mainDeck.Size >= 0)
            {
                PlayingCard card = new PlayingCard();
                SettingsWithCards();

                //Create a new card
                card = (mainDeck.Size == 0) ? cbxTrumpCard.Card : cbxDeck.Card;
                card.FaceUp = true;

                if (card != null)
                {
                    //Create new cardbox control based on card drawn
                    CardBox.CardBox aCardBox = new CardBox.CardBox(card);
                    aCardBox.Size = normalCardSize;
                    // wire drag drop
                    WireCardBoxEventHandlers(aCardBox);
                    //add new controls to the appropriate panel
                    pnlPlayerCards.Controls.Add(aCardBox);

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

                        //realign the controls
                        RealignAllCards();
                    }
                    catch (IndexOutOfRangeException)
                    {
                        System.Diagnostics.Debug.WriteLine("Exception caught when trying to draw card out of index.");
                    }
                }
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

        private void btnStopAttacking_Click(object sender, EventArgs e)
        {
            // if the player chooses to stop defending
            if (!playerAttacking)
            {
                MoveCards(pnlActiveCards, pnlPlayerCards);
                MoveCards(pnlDefended, pnlPlayerCards);

                // Flip discarded pile to facedown without flipping every card
                UpdateDefendedAndDiscardPanelControls();
                RoundDeal(); //Deal cards until both players have at least 6 cards
                ReenableAllCards(); //Allows the player to use their cards again

                txtComputerAttacker.Visible = false; //Flash the image that shows the computer is attacking
                btnStopAttacking.Visible = true; //remove the button that lets the user end their attacking turn
                playerAttacking = true; //mark that the computers attack is starting

                //Increment losses for game stats
                
            }
            else //if the player chooses to stop attacking
            {
                MoveCards(pnlActiveCards, pnlDiscard); //Move cards from the active panel to discard
                MoveCards(pnlDefended, pnlDiscard); //Move cards from the defended panel to discard

                // Flip discarded pile to facedown without flipping every card
                UpdateDefendedAndDiscardPanelControls();
                RoundDeal(); //Deal cards until both players have at least 6 cards
                ReenableAllCards(); //Allows the player to use their cards again

                txtComputerAttacker.Visible = true; //Flash the image that shows the computer is attacking
                btnStopAttacking.Visible = false; //remove the button that lets the user end their attacking turn
                btnStopAttacking.Text = "Stop Attacking";
                playerAttacking = false; //mark that the computers attack is starting

                ComputerAttacks(); //proceed with the computer attack
            }
           //DisableInvalidCardsInHands(); //Determine what the player can defend with

            RealignAllCards();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ComputerAttacks()
        {
            // allow player to end turn if there are no options to play
            btnStopAttacking.Visible = true;
            btnStopAttacking.Text = "End Defend";

            Random rand = new Random(); //initialize a random object
            int computerChoiceIndex = rand.Next(pnlComputerCards.Controls.Count); //generates a random number between 0 and the number of cards the computer has

            txtPlayHistory.Text += Environment.NewLine + "Computers chosen attack index: " + computerChoiceIndex; //Shows the computers randomly chosen card

            ReenableAllCards();

            if (computerChoiceIndex >= 0)
            {
                CardBox.CardBox computerCard = pnlComputerCards.Controls[computerChoiceIndex] as CardBox.CardBox; //create a copy of the card object
                pnlComputerCards.Controls.Remove(computerCard); //remove the card from the computers hand
                pnlActiveCards.Controls.Add(computerCard);      //place the card into the active play panel

                txtPlayHistory.Text += Environment.NewLine + "Computer responds with " + computerCard.ToString(); //Computers choice (RELEVANT FOR GAMEPLAY LOG FILE)
                txtPlayHistory.Text += "PlayerAttacking bool = " + playerAttacking.ToString();

                if(playerAttacking == false)
                {
                    ReenableAllCards();
                    DisableInvalidPlayerDefenseChoices(computerCard);
                }                
            }

            RealignAllCards();
            UpdateDefendedAndDiscardPanelControls();
        }


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
                MoveCards(pnlDefended, pnlDiscard);
                RoundDeal();

                txtComputerAttacker.Visible = false;
                btnStopAttacking.Visible = true;

                playerAttacking = true;
                ReenableAllCards();
            }
             //generates a random number between 0 and the number of cards the computer has
            if (playerAttacking == false)
            {
                CardBox.CardBox computerCard = validCards[cardIndex];

                pnlComputerCards.Controls.Remove(computerCard); //remove the card from the computers hand
                pnlActiveCards.Controls.Add(computerCard);      //place the card into the active play panel

                txtPlayHistory.Text += Environment.NewLine + "Computer responds with " + computerCard.ToString(); //Computers choice (RELEVANT FOR GAMEPLAY LOG FILE)
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
                // hidding frmGame
                this.Hide();

                // new frmMainMenu instance
                frmMainMenu mainMenu = new frmMainMenu();

                // show the frmMainMenu form
                mainMenu.ShowDialog();

                // close frmGame
                this.Close();
            }
            if (pnlComputerCards.Controls.Count == 0 && mainDeck.Size == 0)
            {
                MessageBox.Show("Sorry! You lost.");
                // hidding frmGame
                this.Hide();

                // new frmMainMenu instance
                frmMainMenu mainMenu = new frmMainMenu();

                // show the frmMainMenu form
                mainMenu.ShowDialog();

                // close frmGame
                this.Close();
            }
        }

        /// <summary>
        /// This adds the cards not defended by the player from the defended and the active piles
        /// </summary>
        private void AddCardsForDefeat()
        {
            //MoveCards(pnlActiveCards, pnlPlayerCards);
            //MoveCards(pnlDefended, pnlPlayerCards);

            //RealignAllCards();
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
                btnStopAttacking.Text = "Stop Attacking";
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
            // shuffle
            mainDeck.Shuffle();

            // seeing the order of the deck in debug console for debugging
            mainDeck.ShowDeck(); // This shows all cards, turn this off when done development
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
                txtPlayHistory.Text += firstCard.Suit + " is the current trump suit."; //displays the trump suit to the log  //USE FOR LOGS
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
            cbxDeck.FaceUp = false;
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
        private void InitialDeal()
        {
            PlayingCard playersCard;
            PlayingCard AIsCard;
            PlayingCard lowestCard = new PlayingCard(PlayingCard.trumpSuit, CardRank.Ace); // start as the highest possible card

            // setting the first card
            cbxDeck.Card = mainDeck.GetCard(0);
            mainDeck.DrawCard();

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
                    cbxDeck.Card = mainDeck.DrawCard();

                    card = cbxDeck.Card;
                    CardBox.CardBox computerCardBox = new CardBox.CardBox(card);
                    AIsCard = card;

                    computerCardBox.Size = normalCardSize;
                    //Make a cardbox for the computer
                    pnlComputerCards.Controls.Add(computerCardBox);
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
                        // add players card
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
                        pnlPlayerCards.Controls.Add(new CardBox.CardBox(card));
                        playerHand.Add(card);
                        cbxDeck.Card = mainDeck.DrawCard();
                    }
                    else if (AIHand.Count < 6)
                    {
                        card = cbxTrumpCard.Card;
                        card.FaceUp = true;
                        // add trump card to players hand
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
                        btnStopAttacking.Visible = true;
                        playerAttacking = true;
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
                btnStopAttacking.Visible = true;
                btnStopAttacking.Text = "Stop Attacking";
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

            if (playableCard == false)
            {
                btnStopAttacking.Text = "END TURN";
            }
        }
      
        /// <summary>
        /// Re-enables potentially disabled controls where an attack is over and all cards are to be re-assigned their event handlers
        /// </summary>
        private void ReenableAllCards()
        {
            CheckIfWon();

            foreach (CardBox.CardBox playerCard in pnlPlayerCards.Controls)
            {
                playerCard.Enabled = true;
                WireCardBoxEventHandlers(playerCard);
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
                //Debug Log - This will tell us what card we're moving, from which panel, and where to
                txtPlayHistory.Text += Environment.NewLine + "Moving " + card.ToString() + " from " + panelWithCards.Name + " to " + panelCardsGoTo.Name;

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
        }

        //TODO: Create a function that will move a single card from one panel to another
        // will look something like:
        // private void MoveACard(PlayingCard cardToBeMoved, Panel panelWithCard, Panel panelCardGoesTo)
        // then within the function it should act the same as the way we move cards everywhere else but instead of the specific panel names, we use the inserts to call them

        #endregion

        #region EMPTY EVENT HANDLERS

        private void frmGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            
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
                    if (currentCard.Card.Suit == cbxTrumpCard.Card.Suit && (cardToBeat.Card.Suit != cbxTrumpCard.Card.Suit))
                    { //current card wins
                        noGoodChoice = false;
                        txtPlayHistory.Text += Environment.NewLine + currentCard.Rank + " of " + currentCard.Suit + " could win against the opponent's " + cardToBeat.Rank + " of " + cardToBeat.Suit
                                            + "   " + i + "  " + pnlComputerCards.Controls.IndexOf(currentCard).ToString();
                        idealChoiceIndex = i;
                    }
                    else if (cardToBeat.Card.Suit == cbxTrumpCard.Card.Suit && (currentCard.Card.Suit != cbxTrumpCard.Card.Suit))
                    { //cardto beat wins

                        txtPlayHistory.Text += Environment.NewLine + currentCard.Rank + " of " + currentCard.Suit + " CANNOT win against the opponent's " + cardToBeat.Rank + " of " + cardToBeat.Suit;
                    }
                    else //if neither cards are trump, or both cards are
                    {
                        if (currentCard.Card > cardToBeat.Card)  //win
                        {
                            noGoodChoice = false;
                            txtPlayHistory.Text += Environment.NewLine + currentCard.Rank + " of " + currentCard.Suit + " could win against the opponent's " + cardToBeat.Rank + " of " + cardToBeat.Suit
                                                + "   " + i + "  " + pnlComputerCards.Controls.IndexOf(currentCard).ToString();
                            idealChoiceIndex = i;
                            //end players turn

                        }
                    }
                    // Check to see if option to beat player card is a more efficient (AKA lower value card that current selection) way to beat the opponent and reserve high ranking cards for later
                    if ((currentCard.Card < idealChoice.Card) && noGoodChoice == false)
                    {
                        idealChoiceIndex = i - 1;
                    }
                }
            }//end of loop

            if (noGoodChoice && playerAttacking) //computer cannot defend
            {
                txtPlayHistory.Text += "Computer Cannot Defend";
            }

            // If the computer has no cards prospective to attack or defend, admit defeat and pass the attack or return value that will cause computer to 
            // take the discarded cards
            if (noGoodChoice == false) //computer wins
            {
                txtPlayHistory.Text += idealChoiceIndex + " is the index a wise AI would choose here.";
            }
            else //computer loses
            {
                txtPlayHistory.Text += "AI has no good choices. Human player wins this one!";

                txtComputerAttacker.Visible = true;
                btnStopAttacking.Visible = false;
                playerAttacking = false;
                //MessageBox.Show("PLAYER! You are now the attacker. Fire when ready");
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
                txtPlayHistory.Text += Environment.NewLine + "Computer responds with " + computerCardBox.ToString(); //Computers choice (RELEVANT FOR GAMEPLAY LOG FILE)

                //Compares cards in players hands, determines if they can attack again by comparing their hand to pair in active panel
                CardBox.CardBox tempCard = (CardBox.CardBox)pnlActiveCards.Controls[0];
                txtPlayHistory.Text += Environment.NewLine + "temp card is " + tempCard.ToString();

                CompareCards(tempCard, computerCardBox, this.initialAttackDefended); //deciding which cards can be played on a successive attack
                ReenableAllCards();
                disableInvalidChoices(tempCard.Rank, computerCardBox.Card.Rank);

            }
            else //Computer Cannot Defend
            {
                txtPlayHistory.Text += Environment.NewLine + "COMPUTER HAS NO GOOD CHOICES. Human wins this attack/defense. Things will happen here to proceed with gameplay." + Environment.NewLine;
                txtPlayHistory.Text += Environment.NewLine + "Number of cards in defended " + pnlDefended.Controls.Count;
                MoveCards(pnlDefended, pnlComputerCards);
                MoveCards(pnlActiveCards, pnlComputerCards);
                

                ReenableAllCards();
                RealignAllCards();
                txtComputerAttacker.Visible = true;
                btnStopAttacking.Visible = false;
                playerAttacking = false;

                ComputerAttacks();
            }
            //DisableInvalidCardsInHands();
        }

        #endregion

        
    }
}
