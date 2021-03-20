/*@project          OOPFinal Projct
 *@file             Game.cs 
 *@version          1.0 
 *@since            2021-03-04 
 *@author           Eduardo San Martin Celi, Scott Alton, Nick Sturch-Flint
 *@modified         This program is based on the code presented in chapter 11 of our course textbook. 
 *@see              Beginning Visual C# 2012 Programming by Karli Watson et al.
 *@description      Contains the primary gameplay logic involving various interactions between class library components.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLib
{
    public class Game
    {
        private int currentCard;
        private Deck playDeck;
        private Player[] players;
        private Cards discardedCards;

        public Game()
        {
            currentCard = 0;
            playDeck = new Deck(true);
            //playDeck.LastCardDrawn += Reshuffle;
            playDeck.Shuffle();
            discardedCards = new Cards();
        }

        private void Reshuffle(object source, EventArgs args)
        {
            Console.WriteLine("Discarded cards reshuffled into deck.");
            ((Deck)source).Shuffle();
            discardedCards.Clear();
            currentCard = 0;
        }

        public void SetPlayers(Player[] newPlayers)
        {
            if (newPlayers.Length == 1)
            {
                Console.WriteLine("\n{0}, you are playing against the computer. Good luck!\n", newPlayers[0].Name);
            }
            else if (newPlayers.Length > 7)
                throw new ArgumentException(
                   "\nA maximum of 7 players may play this game.");
            else if (newPlayers.Length < 1)
                throw new ArgumentException(
                   "\nThere must be at least 2 players to continue to the game.");

            players = newPlayers;
        }

        private void DealHands()
        {
            for (int p = 0; p < players.Length; p++)
            {
                for (int c = 0; c < 6; c++)
                {
                    players[p].PlayHand.Add(playDeck.GetCard(currentCard++));
                }
            }
        }

        /// <summary>
        /// PlayGame - The flow of the game, which cycles through each player, shows them a list of their current cards, and then prompts them to make their move
        /// </summary>
        /// <returns></returns>
        public int PlayGame()
        {
            // Only play if players exist.
            if (players == null)
                return -1;
            // Deal initial hands.
            DealHands();

            // Initialize game vars, including an initial card to place on the
            // table: playCard.
            bool GameWon = false;
            int currentPlayer;
            PlayingCard playCard = playDeck.GetCard(currentCard++);
            discardedCards.Add(playCard);

            // Main game loop, continues until GameWon == true.
            do
            {
                // Loop through players in each game round.
                for (currentPlayer = 0; currentPlayer < players.Length;
                     currentPlayer++)
                {
                    // Draw starting trump card
                    PlayingCard initialCardDrawn = playDeck.GetCard(currentCard++);
                    CardSuit trumpSuit = initialCardDrawn.Suit;

                    Console.WriteLine("\nThe starting trump suit is {0}.\n", trumpSuit);

                    // Write out current player, player hand, and the card on the
                    // table.
                    Console.WriteLine("\n{0}'s turn.", players[currentPlayer].Name);
                    Console.WriteLine("********************");
                    Console.WriteLine("Current hand:");
                    foreach (PlayingCard card in players[currentPlayer].PlayHand)
                    {
                        Console.WriteLine(card);
                    }
                    Console.WriteLine("Initial card drawn from top of the deck: {0}", initialCardDrawn);
                    // Prompt player to pick up card on table or draw a new one.
                    bool inputOK = false;
                    do
                    {
                        Console.WriteLine("Press T to throw card or S to skip");
                        string input = Console.ReadLine();
                        if (input.ToLower() == "t")
                        {
                            // Add card from table to player hand.
                            Console.WriteLine("Thrown: {0}", playCard);

                            //// Remove from discarded cards if possible (if deck
                            //// is reshuffled it won't be there any more)
                            //if (discardedCards.Contains(playCard))
                            //{
                            //    discardedCards.Remove(playCard);
                            //}

                            players[currentPlayer].PlayHand.Remove(playCard);
                            inputOK = true;
                        }
                        if (input.ToLower() == "s")
                        {
                            // Add new card from deck to player hand.
                            PlayingCard newCard;
                            // Only add card if it isn't already in a player hand
                            // or in the discard pile
                            bool cardIsAvailable;
                            do
                            {
                                newCard = playDeck.GetCard(currentCard++);
                                // Check if card is in discard pile
                                cardIsAvailable = !discardedCards.Contains(newCard);
                                if (cardIsAvailable)
                                {
                                    // Loop through all player hands to see if newCard
                                    // is already in a hand.
                                    foreach (Player testPlayer in players)
                                    {
                                        if (testPlayer.PlayHand.Contains(newCard))
                                        {
                                            cardIsAvailable = false;
                                            break;
                                        }
                                    }
                                }
                            } while (!cardIsAvailable);
                            // Add the card found to player hand.
                            Console.WriteLine("Drawn: {0}", newCard);
                            players[currentPlayer].PlayHand.Add(newCard);
                            inputOK = true;
                        }
                    } while (inputOK == false);

                    // Display new hand with cards numbered.
                    Console.WriteLine("New hand:");
                    for (int i = 0; i < players[currentPlayer].PlayHand.Count; i++)
                    {
                        Console.WriteLine("{0}: {1}", i + 1,
                                          players[currentPlayer].PlayHand[i]);
                    }
                    // Prompt player for a card to discard.
                    inputOK = false;
                    int choice = -1;
                    do
                    {
                        Console.WriteLine("Choose card to discard:");
                        string input = Console.ReadLine();
                        try
                        {
                            // Attempt to convert input into a valid card number.
                            choice = Convert.ToInt32(input);
                            if ((choice > 0) && (choice <= 8))
                                inputOK = true;
                        }
                        catch
                        {
                            // Ignore failed conversions, just continue prompting.
                        }
                    } while (inputOK == false);

                    // Place reference to removed card in playCard (place the card
                    // on the table), then remove card from player hand and add
                    // to discarded card pile.
                    playCard = players[currentPlayer].PlayHand[choice - 1];
                    players[currentPlayer].PlayHand.RemoveAt(choice - 1);
                    discardedCards.Add(playCard);
                    Console.WriteLine("Discarding: {0}", playCard);

                    // Space out text for players
                    Console.WriteLine();

                    // Check to see if player has won the game, and exit the player
                    // loop if so.
                    GameWon = players[currentPlayer].HasWon();
                    if (GameWon == true)
                        break;
                }
            } while (GameWon == false);

            // End game, noting the winning player.
            return currentPlayer;
        }
    }
}
