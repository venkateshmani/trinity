using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.Interfaces_And_Enums
{
    public enum RecieptStatus
    {
        None = 1, 
	    IssuedToStock = 2, 
	    IssuedToKnittting = 3,
	    IssuedToDyeing = 4,
        IssuesToPrinting = 5,
	    IssuedToPrinting = 6, 
	    IssuedToCompacting = 7,
	    IssuedToWashing = 8,
	    IssuedToOther = 9,
        Recieved = 10,
    }
}
