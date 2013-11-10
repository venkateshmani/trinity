using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.Interfaces_And_Enums
{
    interface IJobExecutionView
    {
        void SetOrder(Order order);
    }
}
