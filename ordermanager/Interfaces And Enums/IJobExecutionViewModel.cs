using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.Interfaces_And_Enums
{
    public interface IJobExecutionViewModel
    {
        Order Order { get; set; }
        List<OrderProduct> Products { get; set; }
        OrderProduct SelectedProduct { get; set; }
        string SelectedDate { get; set; }

        bool IsReadOnly { get; set; }
        bool Save(string userComment);
        void AddNewRecord(DateTime date);
    }
}
