using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.Views.UserControls
{
    public class GRNExpressItem : INotifyPropertyChanged
    {
        private List<GRNReciept> m_GRNReceipts = null;
        public List<GRNReciept> GRNReceipts
        {
            get
            {
                return m_GRNReceipts;
            }
            set
            {
                m_GRNReceipts = value;
            }
        }

        public decimal TotalQuantity
        {
            get
            {
                decimal quantity = 0;

                if (GRNReceipts != null)
                {
                    foreach (var receipt in GRNReceipts)
                    {
                        quantity += receipt.QualityPassedQuantityWrapper;
                    }
                }

                return quantity;
            }
        }

        private decimal m_SelectedQuantity = 0;
        public decimal SelectedQuantity
        {
            get
            {
                return m_SelectedQuantity;
            }
            set
            {
                m_SelectedQuantity = value;
                OnPropertyChanged("SelectedQuantity");
                
            }
        }

        public string ItemName
        {
            get;
            set;
        }
    


        public GRNExpressItem(string itemName)
        {
            ItemName = itemName;
        }

        public void AddGrnReceipt(GRNReciept grnReceipt)
        {
            if (m_GRNReceipts == null)
                m_GRNReceipts = new List<GRNReciept>();


            grnReceipt.PropertyChanged += grnReceipt_PropertyChanged;
            OnPropertyChanged("TotalQuantity"); //To Refresh UI
            m_GRNReceipts.Add(grnReceipt);
        }

        void grnReceipt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                decimal passedQuantity = 0;
                foreach (var receipt in GRNReceipts)
                {
                    if (receipt.IsChecked)
                    {
                        passedQuantity += receipt.QualityPassedQuantityWrapper;
                    }
                }

                SelectedQuantity = passedQuantity;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ClearSelection()
        {
            foreach (var receipt in GRNReceipts)
            {
                receipt.IsChecked = false;
            }
        }

        public string GRNreceiptNumbers
        {
            get
            {
                string recieptNumbers = string.Empty;

                foreach (var item in GRNReceipts)
                {
                    if (recieptNumbers != string.Empty)
                        recieptNumbers += ",";

                    recieptNumbers += item.GRNReciptID;
                }
                return recieptNumbers;
            }
        }

    }
}
