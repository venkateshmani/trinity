using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel    
{
    public partial class CartonBox : EntityBase
    {
        bool m_SelectedForInvoiceGeneration = true;
        public bool SelectedForInvoiceGeneration
        {
            get { return m_SelectedForInvoiceGeneration; }
            set { m_SelectedForInvoiceGeneration = value; }
        }
    }
}
