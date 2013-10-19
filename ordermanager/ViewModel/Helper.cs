﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel
{
    [Flags]
    public enum OrderStatusEnum
    {
        None = -1,
        EnquiryCreated = 1,
        MaterialsAdded = 2,
        MaterialsCostAdded = 3,
        MaterialsJobCompleted = 5,
        EnquiryRejected = 6,
        EnquiryApproved = 7,
        OrderConfirmed = 8,
        SubMaterialsJobCompleted = 9,
        EnquiryCancelled = 10,
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
