using ordermanager.Interfaces_And_Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager
{
    public delegate void OnOrderClickDelegate(object sender);

    public delegate void OnGoBackDelegate();

    public delegate void OnNavigateToDelegate(OrderManagerTab tab);

}
