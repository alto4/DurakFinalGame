/*@project          OOPFinal Projct
 *@file             Cards.cs 
 *@version          1.0 
 *@since            2021-03-04 
 *@author           Eduardo San Martin Celi, Scott Alton, Nick Sturch-Flint
 *@modified         This program is based on the code presented in chapter 11 of our course textbook. 
 *@see              Beginning Visual C# 2012 Programming by Karli Watson et al.
 *@description      Implements a list of cards that will be used in Durak gameplay to represent player hands, as well as actively face-up cards
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLib
{
    /// <summary>
    /// Cards class - collection of playing cards in hand
    /// </summary>
    public class Cards : List<PlayingCard>, ICloneable
    {
        /// <summary>
        /// Clone - clones collection of cards
        /// </summary>
        /// <returns>clone of card collection</returns>
        public object Clone()
        {
            Cards newCards = new Cards();

            foreach (PlayingCard sourceCard in this)
            {
                newCards.Add((PlayingCard)sourceCard.Clone());
            }

            return newCards;
        }

        /// <summary>
        /// CopyTo - copies card(s) to new collection
        /// </summary>
        /// <param name="targetCards">the card(s) to be copied</param>
        public void CopyTo(Cards targetCards)
        {
            for (int index = 0; index < this.Count; index++)
            {
                targetCards[index] = this[index];
            }
        }

    }
}
