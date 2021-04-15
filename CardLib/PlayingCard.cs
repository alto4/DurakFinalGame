/*@project          OOP Final Project
 *@file             PlayingCard.cs 
 *@version          1.0 
 *@since            2021-03-04 
 *@author           Eduardo San Martin Celi, Scott Alton, Nick Sturch-Flint
 *@modified         This program is based on the code presented in chapter 11 of our course textbook. 
 *@see              Beginning Visual C# 2012 Programming by Karli Watson et al.
 *@description      This program demonstrates various extended functionalities of the Ch11CardLib class library.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CardLib
{
    /// <summary>
    /// Card object is simply a suit and a rank.
    /// </summary>
    public class PlayingCard : ICloneable, IComparable
    {
        private CardSuit mySuit;
        private CardRank myRank;
        private int myValue;

        /// <summary>
        /// Enabling allows for a selected card to be valued higher than others
        /// </summary>
        public static bool useTrumps = false;
        public static CardSuit trumpSuit = CardSuit.Clubs;

        public static bool isAceHigh = true;

        public bool FaceUp { get; set; }

        public CardSuit Suit
        {
            get { return mySuit; }
            set { mySuit = value; }
        }

        public CardRank Rank
        {
            get { return myRank; }
            set { myRank = value; }
        }

        public int CardValue
        {
            get { return myValue; }
            set { myValue = value; }
        }

        /// <summary>
        /// private default constructor of a Card object
        /// </summary>
        public PlayingCard() { }

        /// <summary>
        /// Parameterized constructor of a Card object
        /// </summary>
        /// <param name="suit">The Card Suit</param>
        /// <param name="rank">The Card Rank</param>
        public PlayingCard(CardSuit suit, CardRank rank, bool faceUp = false)
        {
            this.mySuit = suit;
            this.myRank = rank;
            this.myValue = (int)rank;
            this.FaceUp = faceUp;
        }

        /// <summary>
        /// override to string
        /// </summary>
        /// <returns>The Card object described as a string</returns>
        public override string ToString()
        {
            string cardString;

            if (FaceUp)
            {
                cardString = string.Format("Suit: {0} of {1}", mySuit, myRank);
            }
            else
            {
                cardString = "Face Down";
            }

            return cardString;
        }

        /// <summary>
        /// This is good enough since there are no object members, only value data fields
        /// </summary>
        /// <returns>Shallow Clone</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region "Operators"
        /// <summary>
        /// Equality operator - checks if 2 cards are made up of the same suit and rank
        /// </summary>
        /// <param name="card1">first card in comparison</param>
        /// <param name="card2">second card in comparison</param>
        /// <returns>boolean representing equality status</returns>
        //public static bool operator ==(PlayingCard card1, PlayingCard card2) => ((card1.Suit == card2.Suit) && (card1.Rank == card2.Rank));

        /// <summary>
        /// Inequality operator - checks if 2 cards are not made up of the same suit and rank
        /// </summary>
        /// <param name="card1">first card in comparison</param>
        /// <param name="card2">second card in comparison</param>
        /// <returns>boolean representing inequality status</returns>
        //public static bool operator !=(PlayingCard card1, PlayingCard card2) => !(card1 == card2);

        /// <summary>
        /// Equals - checks if 2 cards are made up of the same suit and rank
        /// </summary>
        /// <param name="card">card to compare to</param>
        /// <returns>boolean representing equality status</returns>
        public override bool Equals(object obj)
        {
            return (this.CardValue == ((PlayingCard)obj).CardValue);
        }

        /// <summary>
        /// GetCardImage - retrieves card image from resource library - NOTE NAMING CONVENTIONS
        /// </summary>
        /// <returns></returns>
        public Image GetCardImage()
        {
            string imageName;
            Image cardImage;

            if (!FaceUp)
            {
                imageName = "Back";
            }
            else
            {
                imageName = mySuit.ToString() + "_" + myRank.ToString();
            }


            // Get Card Image
            cardImage = Properties.Resources.ResourceManager.GetObject(imageName) as Image;

            return cardImage;
        }

        public string DebugString()
        {
            string cardState = (string)(myRank.ToString() + " of " + mySuit.ToString()).PadLeft(20);
            cardState += (string)((FaceUp) ? "(Face Up)" : "(Face Down)").PadLeft(12);
            cardState += " Value: " + myValue.ToString().PadLeft(2);

            return cardState;
        }

        /// <summary>
        /// GetHashCode - returns cards hash code that identifies card using a unique integer
        /// </summary>
        /// <returns>integer hash code generated based on the card's suit and rank</returns>
        public override int GetHashCode()
        {
            return this.myValue * 100 + (int)this.mySuit * 10;
        }

        /// <summary>
        /// CompareTo - used to sort cards based on value comparison
        /// </summary>
        /// <param name="obj">Generic object to base comparison on</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            // Check if a comparison object exists
            if (obj == null)
            {
                throw new ArgumentNullException("Unable to compare a card with an absent object.");
            }

            PlayingCard compareCard = obj as PlayingCard;

            if (compareCard != null)
            {
                // Account for value as more import than rank to accomodate use of trumps in Durak and other card games using trumps
                int thisSort = this.myValue * 10 + (int)this.mySuit;
                int compareCardSort = compareCard.myValue * 10 + (int)compareCard.mySuit;
                return (thisSort.CompareTo(compareCardSort));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Greater than operator - returns boolean representing the status of left card as greater than right card
        /// </summary>
        /// <param name="card1">the first card for comparison</param>
        /// <param name="card2">the second card for comparison</param>
        /// <returns>boolean representing status of first card as greater than second card</returns>
        public static bool operator >(PlayingCard card1, PlayingCard card2)
        {
            if (card1.Suit == card2.Suit)
            {
                if (isAceHigh)
                {
                    if (card1.Rank == CardRank.Ace)
                    {
                        return (card2.Rank == CardRank.Ace) ? false : true;
                    }
                    else
                    {
                        return (card2.Rank == CardRank.Ace) ? false : (card1.Rank > card2.Rank);
                    }
                }
                else
                {
                    return (card1.Rank > card2.Rank);
                }
            }
            else
            {
                if (card2.Suit == PlayingCard.trumpSuit)
                    return false;

                if (card1.Suit == PlayingCard.trumpSuit)
                    return true;
                               
                if (card1.Rank == CardRank.Ace)
                {
                    return (card2.Rank == CardRank.Ace) ? false : true;
                }
                else
                {
                    return (card2.Rank == CardRank.Ace) ? false : (card1.Rank > card2.Rank);
                }
                
                //return (useTrumps && (card2.Suit == PlayingCard.trumpSuit)) ? false : true;
            }
        }

        // <summary>
        /// Greater than or equal to operator - returns boolean representing the status of left card as greater than or equal to right card
        /// </summary>
        /// <param name="card1">the first card for comparison</param>
        /// <param name="card2">the second card for comparison</param>
        /// <returns>boolean representing status of first card as greater than or equal to second card</returns>
        public static bool operator >=(PlayingCard card1, PlayingCard card2) => (card1 > card2 || card1 == card2);


        // <summary>
        /// Less than operator - returns boolean representing the status of left card as less than or equal to right card
        /// </summary>
        /// <param name="card1">the first card for comparison</param>
        /// <param name="card2">the second card for comparison</param>
        /// <returns>boolean representing status of first card as less than the second card</returns>
        public static bool operator <(PlayingCard card1, PlayingCard card2) => !(card1 >= card2);


        // <summary>
        /// Less than or equal to operator - returns boolean representing the status of left card as less than or equal to right card
        /// </summary>
        /// <param name="card1">the first card for comparison</param>
        /// <param name="card2">the second card for comparison</param>
        /// <returns>boolean representing status of first card as less than or equal to second card</returns>
        public static bool operator <=(PlayingCard card1, PlayingCard card2) => !(card1 > card2);
        #endregion
    }
}
