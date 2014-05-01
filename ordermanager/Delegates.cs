using ordermanager.DatabaseModel;
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

    public delegate void OnSelectionChanged(OrderProduct product, DateTime date);

    public delegate void TreeViewSelectionChangedDelegate(object selectedObject);

    public delegate void MoveToJOListDelegate();

    public delegate void OnCloseDialogDelegate(bool dialogResult);

}
