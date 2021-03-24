/*@project          OOPFinal Projct
 *@file             CardCollection.cs 
 *@version          1.0 
 *@since            2021-03-04 
 *@author           Eduardo San Martin Celi, Scott Alton, Nick Sturch-Flint
 *@modified         This program is based on the code presented in chapter 11 of our course textbook. 
 *@see              Beginning Visual C# 2012 Programming by Karli Watson et al.
 *@description      Implements a class that acts as a collection of any amount of Card objects less than or equal to 52.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLib
{
    /// <summary>
    /// Collection of cards
    /// </summary>
    public class CardCollection : List<PlayingCard>, ICloneable
    {

        /// <summary>
        /// Utility method for copying card instances into another Cards
        /// instance—used in Deck.Shuffle(). This implementation assumes that
        /// source and target collections are the same size.
        /// </summary>
        public void CopyTo(CardCollection targetCards)
        {
            for (int i = 0; i < this.Count; i++)
            {
                targetCards[i] = this[i];
            }
        }

        /// <summary>
        /// Deep copies a Cards object
        /// </summary>
        /// <returns>Cards object</returns>
        public object Clone()
        {
            CardCollection newCards = new CardCollection();
            foreach (PlayingCard sourceCard in this)
            {
                newCards.Add((PlayingCard)sourceCard.Clone());
            }
            return newCards;
        }

        /// <summary>
        /// Displays a player's current hand
        /// </summary>
        /// <returns></returns>
        public void ShowHand()
        {
            foreach (PlayingCard drawnCard in this)
            {
                System.Diagnostics.Debug.WriteLine("* Rank: {0} \n* Suit: {1}", drawnCard.Rank, drawnCard.Suit);
            }
        }

    }
}
