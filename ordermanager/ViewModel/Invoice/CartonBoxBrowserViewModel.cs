﻿using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.Invoice
{
    public class CartonBoxBrowserViewModel : ViewModelBase
    {
        public CartonBoxBrowserViewModel(Order order)
        {
            this.Order = order;
        }
    }
}
