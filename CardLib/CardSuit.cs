/*@project          OOP Final Project
 *@file             CardSuit.cs 
 *@version          1.0 
 *@since            2021-03-04 
 *@author           Eduardo San Martin Celi, Scott Alton, Nick Sturch-Flint
 *@modified         This program is based on the code presented in chapter 11 of our course textbook. 
 *@see              Beginning Visual C# 2012 Programming by Karli Watson et al.
 *@description      Defines an enumeration for suits of a standard card deck.
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLib
{
    /// <summary>
    /// Enum for Suit of a card
    /// </summary>
    public enum CardSuit : byte
    {
        Diamonds,
        Hearts,
        Spades,
        Clubs
    }
}
