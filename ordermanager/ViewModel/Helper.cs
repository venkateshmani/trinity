using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel
{
    [Flags]
    public enum OrderStatusEnum : short
    {
        None = -1,
        EnquiryCreated = 1,
        MaterialsAdded = 2,
        MaterialsCostAdded = 3,       
        EnquiryRejected = 6,
        EnquiryApproved = 7,
        OrderConfirmed = 8,
        SubMaterialsJobCompleted = 9,
        EnquiryCancelled = 10,
        EnquirySentToSpecialApprover=12,
    }

    public class Helper
    {
        public static OrderStatusEnum GetOrderStatusEnumFromString(string statusText)
        {
            OrderStatusEnum status;
            if (Enum.TryParse(statusText, true, out status))
            {
                return status;
            }
            return OrderStatusEnum.None;
        }
    }
}
