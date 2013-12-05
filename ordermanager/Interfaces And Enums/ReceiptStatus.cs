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
	    IssuedToPrinting = 5, 
	    IssuedToCompacting = 6,
	    IssuedToWashing = 7,
	    IssuesToOther = 8
    }
}
