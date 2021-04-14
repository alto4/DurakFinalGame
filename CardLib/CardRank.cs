/*@project          OOPFinal Project
 *@file             CardRank.cs 
 *@version          1.0 
 *@since            2021-03-04 
 *@author           Eduardo San Martin Celi, Scott Alton, Nick Sturch-Flint
 *@modified         This program is based on the code presented in chapter 11 of our course textbook. 
 *@see              Beginning Visual C# 2012 Programming by Karli Watson et al.
 *@description      Defines an enumeration for ranks of a standard card deck.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLib
{
    /// <summary>
    /// Enum for rank of a card.
    /// REFERENCE SOURCE: https://www.tutorialsteacher.com/csharp/csharp-enum
    /// REFERENCE DESCRIPTION: I found this article as a solid source for explaining how enums work and how to assign specific values to each member
    /// </summary>
    public enum CardRank : byte
    {
        Ace = 1,
        King = 13,
        Queen = 12,
        Jack = 11,
        Ten = 10,
        Nine = 9,
        Eight = 8,
        Seven = 7,
        Six = 6,
        Five = 5,
        Four = 4,
        Three = 3,
        Two = 2
    }
    
}
