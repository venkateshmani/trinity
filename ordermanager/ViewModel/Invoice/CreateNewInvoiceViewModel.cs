using ordermanager;
using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.Invoice
{
    public class CreateNewInvoiceViewModel : ViewModelBase
    {
        public CreateNewInvoiceViewModel()
        {
            NewInvoice = new DatabaseModel.Invoice(); 
            NewInvoice.InvoiceDateWrapper = DateTime.Now;
            this.PropertyChanged += CreateNewInvoiceViewModel_PropertyChanged;
        }

        void CreateNewInvoiceViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Order")
            {
                NewInvoice.InvoiceNumberWrapper = Constants.GetInvoiceNumber(this.Order);
                NewInvoice.ExporterRefNumberWrapper = this.Order.OrderID.ToString();
            }
        }

        DatabaseModel.Invoice m_NewInvoice;
        public DatabaseModel.Invoice NewInvoice
        {
            get { return m_NewInvoice; }
            set
            {
                m_NewInvoice = value;
                OnPropertyChanged("NewInvoice");
            }
        }

        ObservableCollection<InvoiceCartonBoxSummary> m_InvoiceCartonBoxSummaries;
        public ObservableCollection<InvoiceCartonBoxSummary> InvoiceCartonBoxSummaries
        {
            get { return m_InvoiceCartonBoxSummaries; }
            set
            {
                m_InvoiceCartonBoxSummaries = value;
                OnPropertyChanged("InvoiceCartonBoxSummaries");
            }
        }
        
        public void UpdateCartonBoxSummaries()
        {
            ObservableCollection<InvoiceCartonBoxSummary> col = new ObservableCollection<InvoiceCartonBoxSummary>();
            Dictionary<string, Dictionary<string, InvoiceCartonBoxSummary>> summaryDict = new Dictionary<string, Dictionary<string, InvoiceCartonBoxSummary>>(1);
            foreach (CartonBox cBox in Order.ShippableCartonBoxes)
            {
                if (cBox.SelectedForInvoiceGeneration)
                {
                    foreach (Package pkg in cBox.Packages)
                    {
                        if (!summaryDict.ContainsKey(pkg.SizeWrapper.Size))
                        {
                            Dictionary<string, InvoiceCartonBoxSummary> tmp = new Dictionary<string, InvoiceCartonBoxSummary>(1);
                            summaryDict.Add(pkg.SizeWrapper.Size, tmp);

                        }

                        if (!summaryDict[pkg.SizeWrapper.Size].ContainsKey(pkg.ColorWrapper.Name))
                        {
                            InvoiceCartonBoxSummary box = new InvoiceCartonBoxSummary();
                            box.StyleID = pkg.StyleID;
                            box.Size = pkg.SizeWrapper.Size;
                            box.Color = pkg.ColorWrapper.Name;
                            box.Quantity = pkg.PackagedWrapper;
                            box.CostPerUnit = pkg.ProductBreakUpSummary.OrderProduct.OurCostInProductCurrenyValue;
                            summaryDict[pkg.SizeWrapper.Size].Add(pkg.ColorWrapper.Name, box);
                            col.Add(box);
                        }
                        else
                        {
                            summaryDict[pkg.SizeWrapper.Size][pkg.ColorWrapper.Name].Quantity += pkg.PackagedWrapper;
                        }
                    }
                }
            }
            InvoiceCartonBoxSummaries = col;
        }

        public bool CreateNewInvoice()
        {
            this.Order.Invoices.Add(NewInvoice);
            foreach (CartonBox cBox in Order.ShippableCartonBoxes)
            {
                if (cBox.SelectedForInvoiceGeneration)
                {
                    cBox.Invoice = NewInvoice;
                    cBox.InvoiceGenerated = true;
                }
            }
            bool result = DBResources.Instance.Save();
            if (result == true)            
                Discard();            
            else
                this.Order.Invoices.Remove(NewInvoice);
            return result;
        }

        public void Discard()
        {
            NewInvoice = new DatabaseModel.Invoice();
            NewInvoice.InvoiceDateWrapper = DateTime.Now;
            InvoiceCartonBoxSummaries = null;
        }
    }
}
