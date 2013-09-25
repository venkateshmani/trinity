using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel
{
    public class NewEnquiryViewModel
    {
        public NewEnquiryViewModel()
        {

        }

        private Order m_Order = null;
        public Order Order
        {
            get
            {
                if (m_Order == null)
                {
                    
                }
                return m_Order;
            }
            set
            {
                m_Order = value;
            }
        }

        public void Save()
        {

        }
    }
}
