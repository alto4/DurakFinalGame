using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLib
{
    /// <summary>
    /// Entry point for the game
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n******************************************");
            Console.WriteLine("*  Durak Project: Console App Iteration  *");
            Console.WriteLine("******************************************\n");

            // Gameplay Pseudo-Code

            /*
            // GAME COMMENCES
            // 1. Deck of 36 cards is shuffled
            Deck testDeck1 = new Deck(true, Suit.Clubs);
            Console.WriteLine("Testing new shuffle algorithm...");
            Console.WriteLine(testDeck1.ToString());
            testDeck1.Shuffle();
            Console.WriteLine("Results of the shuffle algorithm:");
            Console.WriteLine(testDeck1.ToString());
            // 2. Each player is dealt 6 cards
            CardCollection hand1 = new CardCollection();
            CardCollection hand2 = new CardCollection();
            for(int i = 0; i < 6; i++)
            {
                hand1.Add(testDeck1.DrawCard());
                hand2.Add(testDeck1.DrawCard());
            }
                    
            Console.WriteLine("Results of drawing cards and placing in player hand:");
            Console.WriteLine("Hand Length:" + hand1.Count);
            Console.WriteLine("\nUpdated Deck :" + testDeck1.ToString());
            // 3. Bottom card is turned to face up state
            Card startingTrumpCard = testDeck1.GetCard(0);
            startingTrumpCard.isFaceUp = true;
            Console.WriteLine("\nFirst card drawn! It's a {0} of {1}.", startingTrumpCard.Rank, startingTrumpCard.Suit);
            Console.WriteLine("Testing faceup status of first drawn card: " + startingTrumpCard.isFaceUp);
            // 4. Suit of first card to be drawn becomes trump suit
            Console.WriteLine("\nThe starting trump suit is {0}.", startingTrumpCard.Suit);
            // ATTACK ROUND
            //// 5. First attack - current player attacks by playing a face-up card from their hand that trumps 
            Console.WriteLine("\nFirst players turn to attack");
            Console.WriteLine("\nPlayer's current hand: ");
            hand1.ShowHand();
            // Keep track of best player choice based on nearest winnable rank to attacking card
            int idealChoiceIndex =  0;
            // Check for all cards in hand that are potential plays to beat current card
            for(int i = 0; i < hand1.Count; i++)
            {
                if (hand1[i].Rank > startingTrumpCard.Rank)
                {
                    Console.WriteLine("Card #{0}, {1} of {2} IS an option to beat the {3} of {4}.", (i + 1), hand1[i].Rank, hand1[i].Suit, startingTrumpCard.Rank, startingTrumpCard.Suit);
                    // Check for relationship between higher card in hand and card defending against
                    Console.WriteLine("Card #{0}, {1} of {2} is {3} rank higher than {4} of {5}.", (i + 1), hand1[i].Rank, hand1[i].Suit, (hand1[i].Rank - startingTrumpCard.Rank), startingTrumpCard.Rank, startingTrumpCard.Suit);
                    if (((int)hand1[i].Rank - (int)startingTrumpCard.Rank) < ((int)hand1[idealChoiceIndex].Rank - (int)startingTrumpCard.Rank)) 
                    {
                        idealChoiceIndex = i;
                    }
                } 
                else
                {
                    Console.WriteLine("Card #{0}, {1} of {2} IS NOT an option to beat the {3} of {4}.", (i + 1), hand1[i].Rank, hand1[i].Suit, startingTrumpCard.Rank, startingTrumpCard.Suit);
                }
            }
            // Inform user of the best choice of cards
            if ((int)hand1[idealChoiceIndex].Rank > (int)startingTrumpCard.Rank)
            {
                Console.WriteLine("It has been determined that the best play is card #{0}, the {1} of {2}.", (idealChoiceIndex + 1), hand1[idealChoiceIndex].Rank, hand1[idealChoiceIndex].Suit);
            } else
            {
                Console.WriteLine("Sorry. You cannot beat the current card you are up against.");
            }
            
            //// 6. Defender attempts to beat attack card with a higher-ranking defence card
            //Console.WriteLine("\nDefender's current hand: ");
            //hand2.ShowHand();
            //// Test sorting algorithm and display in console
            //Console.WriteLine("\nPlayer 2 hand being arranged from highest to lowest values to assist with card selection for defense/attack.");
            //Console.WriteLine(hand2.Count);
            
            // 7. Defender loses - pick up all defending and attaching cards played during the attach round
            // 8. Defender is successful (no other player attacks or they successfully defend 6th attack card) and attack/defence cards discarded
            // 9. Defender becomes attacker
            // 10. Each player draws cards from deck until they have 6 (or deck runs out) - order for draw is attacker, then clockwise, then defender
            // 11. REPEAT ATTACK ROUND STEPS 5-10
            */


            // Initialize array of Player objects.
            Player[] players = new Player[2];

            // Get player name.
            Console.WriteLine("Please enter your name: ");
            string playerName = Console.ReadLine();
            players[0] = new Player(playerName);
            players[1] = new Player("Computer");

            // Start game.
            Game newGame = new Game();
            newGame.SetPlayers(players);
            int whoWon = newGame.PlayGame();

            // Display winning player.
            Console.WriteLine("{0} has won the game!", players[whoWon].Name);
            Console.ReadKey();
        }

        /// <summary>
        /// Looks for a sequence that makes a flush in a random deck, user for testing
        /// </summary>
        static void TryFlush()
        {
            Deck myDeck = new Deck();
            myDeck.Shuffle();

            int numberOfDraws = (int)Math.Round((double)(Deck.SIZE_OF_DECK / 5), 0); // Can draw 5 cards 10 times in a deck of 52
            bool isFlush = false;

            for (int i = 0; i < numberOfDraws; i++)
            {
                try
                {
                    int sameSuitCount = 0;
                    PlayingCard tempCard = myDeck.GetCard(i * 5);
                    CardSuit tempSuit = tempCard.Suit;
                    for (int j = 1; j <= 4; j++)
                    {
                        if (myDeck.GetCard(i * 5 + j).Suit == tempSuit) // same suit as temp card
                        {
                            sameSuitCount++;
                        }
                        if (sameSuitCount == 4) // the other 4 cards are the same suit as the first of the 5 draws
                        {
                            isFlush = true;
                            Console.WriteLine("*******************\n");
                            Console.WriteLine("Flush!!!");
                            for (int z = 0; z < 5; z++) // printing out the last 5 cards
                            {
                                Console.WriteLine(myDeck.GetCard(i * 5 + j - z).ToString());
                            }
                            return; // exit out of the method
                        }
                    }
                }
                catch (IndexOutOfRangeException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }
            }
            if (isFlush == false) // if no flush in this shuffle, show this msg
            {
                Console.WriteLine("No Flush");
            }
        }
    }
}
