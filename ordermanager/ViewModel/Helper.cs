using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel
{
    [Flags]
    public enum OrderStatusEnum
    {
        EnquiryCreated = 1,
        MaterialsAdded = 2,
        MaterialsCostAdded = 4,
        MaterialsConsumptionAdded = 8,
        MaterialsJobCompleted = 16,
        EnquiryReInvoked = 32,
        EnquiryApproved = 64,
        OrderConfirmed = 128,
        SubMaterialsJobCompleted = 256,
    }
}
