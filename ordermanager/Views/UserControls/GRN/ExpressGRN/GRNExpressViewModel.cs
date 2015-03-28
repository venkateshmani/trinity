using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.Views.UserControls
{
    public class GRNExpressViewModel : ViewModelBase
    {
        public GRNExpressViewModel(Order order) : base()
        {
            this.Order = order;
        }

        private Dictionary<string, GRNExpressItem> m_Items = null;
        public IList<GRNExpressItem> Items
        {
            get
            {
                if (m_Items == null)
                    FillData();

                return m_Items.Values.ToList();
            }
            set
            {
                OnPropertyChanged("Items");
            }
        }

        private GRNExpressItem m_SelectedItem = null;
        public GRNExpressItem SelectedItem
        {
            get
            {
                return m_SelectedItem;
            }
            set
            {
                m_SelectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public void ResetData()
        {
            m_Items = null;
            OnPropertyChanged("Items");
        }

        private void FillData()
        {
            m_Items = new Dictionary<string, GRNExpressItem>();
            foreach (PurchaseOrder po in Order.PurchaseOrders)
            {
                foreach (OrderedItem item in po.OrderedItems)
                {
                    foreach (GRNReciept grnReceipt in item.GRNReciepts)
                    {
                        if (grnReceipt.JobOrders != null && grnReceipt.JobOrders.Count > 0)
                            continue;

                        if ((grnReceipt.ReceiptStatusID == null || grnReceipt.ReceiptStatusID.Value == 10 || grnReceipt.ReceiptStatusID.Value == 1) && grnReceipt.QualityPassedQuantityWrapper != 0)
                        {
                            string materialName = item.SubMaterial.Name.ToUpper();
                            GRNExpressItem expressItem = null;
                            if (m_Items.ContainsKey(materialName))
                            {
                                expressItem = m_Items[materialName];
                            }
                            else
                            {
                                expressItem = new GRNExpressItem(materialName);
                                m_Items.Add(materialName, expressItem);
                            }
                            expressItem.AddGrnReceipt(grnReceipt);
                        }
                    }
                }
            }
        }
    }
}
