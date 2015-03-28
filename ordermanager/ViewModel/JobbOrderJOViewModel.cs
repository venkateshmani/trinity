using ordermanager.DatabaseModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel
{
    public class JobbOrderJOViewModel : INotifyPropertyChanged
    {

        public JobbOrderJOViewModel(Order order)
        {
            this.Order = order;
        }

        private Order m_Order = null;
        public Order Order
        {
            get
            {
                return m_Order;
            }
            set
            {
                m_Order = value;
            }
        }

        private ObservableCollection<Company> m_Suppliers = null;
        public ObservableCollection<Company> Suppliers
        {
            get 
            {
                if (m_Suppliers == null)
                {
                    m_Suppliers = new ObservableCollection<Company>();
                    foreach (PurchaseOrder po in Order.PurchaseOrders)
                    {
                        foreach (OrderedItem item in po.OrderedItems)
                        {
                            foreach (GRNReciept receipt in item.GRNReciepts)
                            {
                                foreach (JobOrder jo in receipt.JobOrders)
                                {
                                    if (!m_Suppliers.Contains(jo.Company))
                                    {
                                        m_Suppliers.Add(jo.Company);
                                    }
                                }
                            }
                        }
                    }
                }

                return m_Suppliers;
            }
        }

        private Company m_SelectedSupplier = null;
        public Company SelectedSupplier
        {
            get
            {
                return m_SelectedSupplier;
            }
            set
            {
                m_SelectedSupplier = value;
                RefreshJobOrders();                
                OnPropertyChanged("SelectedSupplier");
            }
        }


        private void RefreshJobOrders()
        {
            if (this.Order == null || this.SelectedSupplier == null)
                return;
            //Select the Job Orders
            var activeJobOrders = new ObservableCollection<JobOrder>();

            foreach (var dyeingJo in Order.DyeingJOes)
            {
                if (dyeingJo.Supplier == this.SelectedSupplier && dyeingJo.JobOrder != null && !activeJobOrders.Contains(dyeingJo.JobOrder)
                    && dyeingJo.JobOrder.IsIssued == false)
                {
                    activeJobOrders.Add(dyeingJo.JobOrder);
                }
            }

            foreach (var knittingJo in Order.KnittingJOes)
            {
                if (knittingJo.Supplier == this.SelectedSupplier && knittingJo.JobOrder != null && !activeJobOrders.Contains(knittingJo.JobOrder)
                    && knittingJo.JobOrder.IsIssued == false)
                {
                    activeJobOrders.Add(knittingJo.JobOrder);
                }
            }

            foreach (var compactingJo in Order.CompactingJoes)
            {
                if (compactingJo.Supplier == this.SelectedSupplier && compactingJo.JobOrder != null && !activeJobOrders.Contains(compactingJo.JobOrder)
                    && compactingJo.JobOrder.IsIssued == false)
                {
                    activeJobOrders.Add(compactingJo.JobOrder);
                }
            }

            foreach (var jo in this.SelectedSupplier.JobOrders)
            {
                if (jo.GRNReciept != null && jo.GRNReciept.OrderedItem.PurchaseOrder.Order == this.Order && !activeJobOrders.Contains(jo) && jo.IsIssued == false)
                {
                    activeJobOrders.Add(jo);
                }
            }

            JobOrders = activeJobOrders;
        }

       

        private ObservableCollection<JobOrder> m_JobOrders = null;
        public ObservableCollection<JobOrder> JobOrders
        {
            get
            {
                return m_JobOrders;
            }
            set
            {
                m_JobOrders = value;
                OnPropertyChanged("JobOrders");
            }
        }

        public bool IssueNewJob(JobOrder jobOrder)
        {
            bool res = false;
            if (jobOrder != null)
            {
                if (jobOrder.JobOrderType.Type.ToLower() == "stock")
                {
                    if (jobOrder.GRNReciept != null)
                    {
                        if (jobOrder.GRNReciept.OrderedItem.ProductMaterialItem.SubMaterial.InStock == null)
                            jobOrder.GRNReciept.OrderedItem.ProductMaterialItem.SubMaterial.InStock = 0;

                        MaterialStock stock = new MaterialStock();
                        stock.Order = this.Order;
                        stock.SubMaterial = jobOrder.GRNReciept.OrderedItem.ProductMaterialItem.SubMaterial;
                        stock.InStockDateTime = DBResources.Instance.GetServerTime();
                        stock.StockQuantity = jobOrder.JobQuantity;
                        stock.UnitsOfMeasurement = jobOrder.GRNReciept.OrderedItem.ProductMaterialItem.UnitsOfMeasurementWrapper;
                        jobOrder.GRNReciept.OrderedItem.ProductMaterialItem.SubMaterial.MaterialStocks.Add(stock);
                        jobOrder.GRNReciept.OrderedItem.ProductMaterialItem.SubMaterial.InStock += jobOrder.JobQuantity;
                    }
                    else
                        return false;
                }
                else
                {
                    jobOrder.GRNReciept.JobOrders.Add(jobOrder);
                }
                res = Save();
                if (res)
                    jobOrder.Refresh();
            }
            return res;
        }
        
        public bool Save()
        {
            return DBResources.Instance.Save();
        }

        public bool SendForSpecialApproval(JobOrder jOrder)
        {
            bool res = false;
            jOrder.IsWaitingForApproval = true;
            res = Save();
            if (res)
                jOrder.Refresh();
            return res;
        }

        public bool SpecialApproval(JobOrder jOrder)
        {
            bool res = false;
            jOrder.IsWaitingForApproval = false;
            jOrder.HasApproved = true;
            res = Save();
            if (res)
                jOrder.Refresh();
            return res;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
