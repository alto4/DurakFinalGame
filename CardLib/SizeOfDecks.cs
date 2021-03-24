/*@project          OOPFinal Projct
 *@file             SizeOfDecks.cs 
 *@version          1.0 
 *@since            2021-03-24 
 *@author           Eduardo San Martin Celi, Scott Alton, Nick Sturch-Flint
 *@description      This is the enum to choose the size of the deck in a game of Durak.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLib
{
    public enum SizeOfDecks : ushort
    {
        Small = 20,
        Normal = 36,
        Large = 52
    }
}
