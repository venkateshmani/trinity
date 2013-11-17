using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.InStock
{
    public class Stock
    {
        public Stock()
        {
            IsLoaded = false;
            this.Items = new List<object>();
        }

        public string Name
        {
            get;
            set;
        }

        public List<object> Items
        {
            get;
            set;
        }

        public bool IsLoaded
        {
            get;
            set;
        }

        public virtual void Load()
        {
            IsLoaded = true;
        }

    }
}
