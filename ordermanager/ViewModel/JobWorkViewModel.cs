using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel
{
   public class JobWorkViewModel 
    {        
        public ObservableCollection<Company> Suppliers
        {
            get { return DBResources.Instance.Suppliers; }
        }
    }
}
