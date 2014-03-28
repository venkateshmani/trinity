using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.Extended_Database_Models
{
    public interface IJobOrderInfo
    {
        string JobOrderNumber { get;  }
        string SupplierName { get;  }
        string Status { get;  }
        string Type { get; }

        void RefreshInfoJobOrderInfo();
    }
}
