using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel    
{
    public partial class GRNReciept : EntityBase
    {

        public string ReceiptNumber
        {
            get
            {
                if (this.GRNReciptID != 0)
                {
                    return "Receipt-" + this.GRNReciptID;
                }

                return string.Empty;
            }
        }
    }
}
