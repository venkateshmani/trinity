using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class DyeingJO  : EntityBase
    {
        private ObservableCollection<DyeingJoItem> m_Items = null;
        public ObservableCollection<DyeingJoItem> Items
        {
            get
            {
                if (m_Items == null)
                {
                     m_Items = new ObservableCollection<DyeingJoItem>(this.DyeingJoItems);
                     foreach (var item in m_Items)
                     {
                         item.PropertyChanged += item_PropertyChanged; 
                     }
                }

                return m_Items;
            }
        }

        public string PurchaseOrderNumber
        {
            get
            {
                if (PurchaseOrder != null)
                    return PurchaseOrder.PurchaseOrderNumber;

                return string.Empty;
            }
            set
            {
                if (this.Order != null)
                {
                    foreach (var po in this.Order.PurchaseOrders)
                    {
                        if (po.PurchaseOrderNumber == value)
                            PurchaseOrder = po;
                    }
                }
            }
        }

        public void Add()
        {
            DyeingJoItem item = new DyeingJoItem();
            item.DyeingJO = this;
            item.PropertyChanged +=item_PropertyChanged;
            Items.Add(item);

            
            CalcualteTotalAmount();
        }

        public void Remove(DyeingJoItem item)
        {
            item.PropertyChanged -= item_PropertyChanged;
            Items.Remove(item);
            if(DyeingJoItems.Contains(item))
            {
                DyeingJoItems.Remove(item);
            }
            item.DyeingJO = null;

            
            CalcualteTotalAmount();
        }

        void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TotalamountWrapper")
            {
                CalcualteTotalAmount();
            }
        }

        private void CalcualteTotalAmount()
        {
            TotalValue = 0;
            foreach (var item in Items)
            {
                TotalValue += item.TotalAmountWrapper;
            }
        }
    }
}
