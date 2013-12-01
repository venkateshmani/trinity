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
    public class PoGrnSummaryViewModel : INotifyPropertyChanged
    {
        public PoGrnSummaryViewModel(PurchaseOrder purchaseOrder)
        {
            this.PurchaseOrder = purchaseOrder;
            Receipts = new ObservableCollection<GRNReciept>();
        }

        private ObservableCollection<GRNReciept> m_Receipts = null;
        public ObservableCollection<GRNReciept> Receipts
        {
            get
            {
                return m_Receipts;
            }
            set
            {
                m_Receipts = value;
                OnPropertyChanged("Receipts");
            }
        }

        private string m_InvoiceNumber = string.Empty;
        public string InvoiceNumber
        {
            get
            {
                return m_InvoiceNumber;
            }
            set
            {
                m_InvoiceNumber = value;
                foreach (GRNReciept grnReceipt in Receipts)
                {
                    grnReceipt.InvoiceNumber = m_InvoiceNumber;
                }
            }
        }

        private DateTime m_InvoiceDate = DateTime.Now;
        public DateTime InvoiceDate
        {
            get
            {
                return m_InvoiceDate;
            }
            set
            {
                m_InvoiceDate = value;
                foreach (GRNReciept grnReceipt in Receipts)
                {
                    m_InvoiceDate = value;
                }
            }
        }

        private DateTime m_ReceivedDate = DateTime.Now;
        public DateTime ReceivedDate
        {
            get
            {
                return m_ReceivedDate;
            }
            set
            {
                m_ReceivedDate = value;
                foreach (GRNReciept grnReceipt in Receipts)
                {
                    m_ReceivedDate = value;
                }
            }
        }

        private PurchaseOrder m_PurchaseOrder = null;
        public PurchaseOrder PurchaseOrder
        {
            get
            {
                return m_PurchaseOrder;
            }
            set
            {
                m_PurchaseOrder = value;
                OnPropertyChanged("PurchaseOrder");
            }
        }

        public List<OrderedItem> AvailableItemsInPoToCreateGRNReceipt
        {
            get
            {
                List<OrderedItem> items = new List<OrderedItem>();

                if(PurchaseOrder != null)
                {
                    foreach (OrderedItem item in PurchaseOrder.OrderedItems)
                    {
                        bool hasItemAlreadyAdded = false;
                        foreach (GRNReciept receipt in Receipts)
                        {
                            if (receipt.OrderedItem == item)
                            {
                                hasItemAlreadyAdded = true;
                                break;
                            }
                        }

                        if (!hasItemAlreadyAdded)
                        {
                            items.Add(item);
                        }
                    }
                }
                return items;
            }
        }
        
        public void AddItems(List<OrderedItem> items)
        {
            foreach (OrderedItem item in items)
            {
                GRNReciept newReceipt = new GRNReciept();
                newReceipt.OrderedItem = item;
                newReceipt.InvoiceNumber = this.InvoiceNumber;
                newReceipt.InvoiceDate = this.InvoiceDate;
                newReceipt.RecievedDate = this.ReceivedDate;
                this.Receipts.Add(newReceipt);
                item.GRNReciepts.Add(newReceipt);
            }
        }

        public void AddReceipt()
        {
            foreach (var grnReceipt in Receipts)
            {
                grnReceipt.PurchaseOrder = this.PurchaseOrder;
            }

            DBResources.Instance.Save();
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
