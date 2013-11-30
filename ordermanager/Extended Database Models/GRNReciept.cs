using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class GRNReciept : EntityBase
    {

        public string ReceiptNumber
        {
            get
            {
                if (this.GRNReciptID != 0)
                {
                    return "Receipt-" + this.GRNReciptID;
                }

                return string.Empty;
            }
        }

        private ObservableCollection<JobOrder> m_KnittingItemsWrapper = null;
        private ObservableCollection<JobOrder> m_DyeingItemsWrapper = null;
        private ObservableCollection<JobOrder> m_PrintingItemsWrapper = null;
        private ObservableCollection<JobOrder> m_CompactingItemsWrapper = null;
        private ObservableCollection<JobOrder> m_WashingItemsWrapper = null;
        private ObservableCollection<JobOrder> m_OtherItemsWrapper = null;

        public ObservableCollection<JobOrder> KnittingItemsWrapper
        {
            get
            {
                if (m_KnittingItemsWrapper == null)
                {
                    m_KnittingItemsWrapper = GetJobOrdersByType("Knitting");
                }
                return m_KnittingItemsWrapper;
            }
            private set
            {
                m_KnittingItemsWrapper = value;
            }
        }

        public ObservableCollection<JobOrder> DyeingItemsWrapper
        {
            get
            {
                if (m_DyeingItemsWrapper == null)
                {
                    m_DyeingItemsWrapper = GetJobOrdersByType("Dyeing");
                }
                return m_DyeingItemsWrapper;
            }
            private set
            {
                m_DyeingItemsWrapper = value;
            }
        }

        public ObservableCollection<JobOrder> PrintingItemsWrapper
        {
            get
            {
                if (m_PrintingItemsWrapper == null)
                {
                    m_PrintingItemsWrapper = GetJobOrdersByType("Printing");
                }
                return m_PrintingItemsWrapper;
            }
            private set
            {
                m_PrintingItemsWrapper = value;
            }
        }

        public ObservableCollection<JobOrder> CompactingItemsWrapper
        {
            get
            {
                if (m_CompactingItemsWrapper == null)
                {
                    m_CompactingItemsWrapper = GetJobOrdersByType("Compacting");
                }
                return m_CompactingItemsWrapper;
            }
            private set
            {
                m_CompactingItemsWrapper = value;
            }
        }

        public ObservableCollection<JobOrder> WashingItemsWrapper
        {
            get
            {
                if (m_WashingItemsWrapper == null)
                {
                    m_WashingItemsWrapper = GetJobOrdersByType("Washing");
                }
                return m_WashingItemsWrapper;
            }
            private set
            {
                m_WashingItemsWrapper = value;
            }
        }

        public ObservableCollection<JobOrder> OtherItemsWrapper
        {
            get
            {
                if (m_OtherItemsWrapper == null)
                {
                    m_OtherItemsWrapper = GetJobOrdersByType("Other");
                }
                return m_OtherItemsWrapper;
            }
            private set
            {
                m_OtherItemsWrapper = value;
            }
        }

        private ObservableCollection<JobOrder> GetJobOrdersByType(string type)
        {
            return new ObservableCollection<JobOrder>(JobOrders.Where(jo => jo.JobOrderType.Type == type).Select(jo => jo).ToList());
        }
    }
}
