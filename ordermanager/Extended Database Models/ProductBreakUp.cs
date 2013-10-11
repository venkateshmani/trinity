using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class ProductBreakUp : EntityBase
    {
        public double ToleranceWrapper
        {
            get
            {
                return Tolerance;
            }
            set
            {
                Tolerance = value;
            }
        }
        public string PriceTermsWrapper
        {
            get
            {
                return PriceTerms;
            }
            set
            {
                PriceTerms = value;
            }
        }
        public string PaymentTermsWrapper
        {
            get
            {
                return PaymentTerms;
            }
            set
            {
                PaymentTerms = value;
            }
        }
        public string DocumentsRequiredWrapper
        {
            get
            {
                return DocumentsRequired;
            }
            set
            {
                DocumentsRequired = value;
            }
        }

        public ShipmentMode ShipmentModeWrapper
        {
            get
            {
                return ShipmentMode;
            }
            set
            {
                ShipmentMode = value;
            }
        }

        ObservableCollection<ProductCountryWiseBreakUp> m_ProductCountryWiseBreakUpWrapper;
        public ObservableCollection<ProductCountryWiseBreakUp> ProductCountryWiseBreakUpWrapper
        {
            get
            {
                if (m_ProductCountryWiseBreakUpWrapper == null)
                {
                    m_ProductCountryWiseBreakUpWrapper = new ObservableCollection<ProductCountryWiseBreakUp>(this.ProductCountryWiseBreakUps);
                    foreach (var item in m_ProductCountryWiseBreakUpWrapper)
                    {
                        item.PropertyChanged += ProductCountryWiseBreakUpWrapper_PropertyChanged;
                    }
                    m_ProductCountryWiseBreakUpWrapper.CollectionChanged += ProductCountryWiseBreakUpWrapper_CollectionChanged;
                }
                return m_ProductCountryWiseBreakUpWrapper;
            }
        }

        void ProductCountryWiseBreakUpWrapper_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    ProductCountryWiseBreakUp newItem = item as ProductCountryWiseBreakUp;
                    newItem.ProductBreakUp = this;
                    newItem.PropertyChanged += ProductCountryWiseBreakUpWrapper_PropertyChanged;
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems)
                {
                    ProductCountryWiseBreakUp deletedItem = item as ProductCountryWiseBreakUp;
                    deletedItem.ProductBreakUp = null;
                    deletedItem.PropertyChanged -= ProductCountryWiseBreakUpWrapper_PropertyChanged;
                }
            }
            OnPropertyChanged("ProductCountryWiseBreakUpWrapper");
        }

        void ProductCountryWiseBreakUpWrapper_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // throw new NotImplementedException();
        }
    }
}
