/*@project          OOPFinal Projct
 *@file             Deck.cs 
 *@version          1.0 
 *@since            2021-03-04 
 *@author           Eduardo San Martin Celi, Scott Alton, Nick Sturch-Flint
 *@modified         This program is based on the code presented in chapter 11 of our course textbook. 
 *@see              Beginning Visual C# 2012 Programming by Karli Watson et al.
 *@description      Implements a class that acts as a collection of 52 Card objects.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLib
{
    /// <summary>
    /// Deck class holds an array of Card objects
    /// </summary>
    public class Deck : ICloneable
    {
        // define the standard size of a deck
        public static readonly int SIZE_OF_DECK = 52;

        // instantiate a CardCollection object
        private CardCollection cards = new CardCollection();

        /// <summary>
        /// Default constructor for a deck of cards.
        /// </summary>
        public Deck()
        {
            foreach (CardSuit suit in (CardSuit[])Enum.GetValues(typeof(CardSuit))) // for each suit in the Suit enum
            {
                foreach (CardRank rank in (CardRank[])Enum.GetValues(typeof(CardRank))) // for each rank in the Rank enum
                {
                    cards.Add(new PlayingCard(suit, rank));
                }
            }
        }

        /// <summary>
        /// return the card at the param index
        /// </summary>
        /// <param name="cardIndex">The cards index</param>
        /// <returns>The Card at this index</returns>
        public PlayingCard GetCard(int cardIndex)
        {
            if (cardIndex >= 0 && cardIndex <= 51)
            {
                return cards[cardIndex]; // card number 0 to SIZE_OF_DECK
            }
            else
            {
                throw new IndexOutOfRangeException(string.Format(" * Card index must be between {0} and {1}", 0, SIZE_OF_DECK - 1));
            }
        }

        /// <summary>
        /// return the card at the param index and remove it from the deck
        /// </summary>
        /// <param name="cardIndex">The cards index</param>
        /// <returns>The Card at this index</returns>
        public PlayingCard DrawCard(int cardIndex = 0)
        {
            if (cardIndex >= 0 && cardIndex <= 51)
            {
                PlayingCard tempCard = cards[cardIndex];
                cards.RemoveAt(cardIndex);
                return cards[cardIndex]; // card number 0 to SIZE_OF_DECK
            }
            else
            {
                throw new IndexOutOfRangeException(string.Format(" * Card index must be between {0} and {1}", 0, SIZE_OF_DECK - 1));
            }
        }

        public Deck(bool isAceHigh) : this() // high aces
        {
            PlayingCard.isAceHigh = isAceHigh;
        }

        public Deck(bool useTrump, CardSuit trumpSuit) : this() // setting the trump
        {
            PlayingCard.useTrumps = useTrump;
            PlayingCard.trumpSuit = trumpSuit;
        }

        public Deck(bool isAceHigh, bool useTrumps, CardSuit trumpSuit) : this()
        {
            PlayingCard.isAceHigh = isAceHigh;
            PlayingCard.useTrumps = useTrumps;
            PlayingCard.trumpSuit = trumpSuit;
        }

        /// <summary>
        /// swaps each card at an index with a random card, 5 times.
        /// </summary>
        public void Shuffle()
        {
            CardCollection randomDeck = new CardCollection();
            Random randSource = new Random();
            int randIndex = 0;
            PlayingCard tempCardHolder;

            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < SIZE_OF_DECK; i++)
                {
                    // Random index for each position
                    randIndex = i + randSource.Next(SIZE_OF_DECK - i);

                    // swap the cards
                    tempCardHolder = cards[randIndex];
                    cards[randIndex] = cards[i];
                    cards[i] = tempCardHolder;
                }
            }
            // copy the random deck to this deck
            randomDeck.CopyTo(cards);
        }

        /// <summary>
        /// Creates a deck for deep cloning
        /// </summary>
        /// <param name="newCards">The new cards</param>
        private Deck(CardCollection newCards)
        {
            cards = newCards;
        }

        /// <summary>
        /// creates a new deck by passing the private constructor a set of cards
        /// </summary>
        /// <returns>a new deck</returns>
        public object Clone()
        {
            Deck newDeck = new Deck(cards.Clone() as CardCollection);
            return newDeck;
        }

        public override string ToString()
        {
            string allTheCards = "";

            foreach (PlayingCard card in cards)
            {
                allTheCards += card.ToString() + "\n";
            }

            return allTheCards;
        }
    }
}
