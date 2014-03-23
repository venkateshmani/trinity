using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.PurchaseOrderControl
{
    public class CreateNewPurchaseOrderViewModel : ViewModelBase
    {
        public CreateNewPurchaseOrderViewModel(Order order)
        {
            Order = order;
        }

        public CreateNewPurchaseOrderViewModel(Order order, PurchaseOrder po) : this(order)
        {
            PurchaseOrder = po;

            //Only for PO generated from GRN failed quantity
            if (PurchaseOrder.ProductMaterialItems.Count == 0)
            {
                if (PurchaseOrder.OrderedItems.Count != 0)
                {
                    foreach (OrderedItem item in PurchaseOrder.OrderedItems)
                    {
                        item.PropertyChanged -= item_PropertyChanged;
                        item.PropertyChanged += item_PropertyChanged;

                        TotalPOValue += item.ItemCostInItemCurrency;
                    }
                }
            }
        }

        void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            TotalPOValue = 0;
            foreach (OrderedItem item in PurchaseOrder.OrderedItems)
            {
                TotalPOValue += item.ItemCostInItemCurrency;
            }
        }

        private bool m_IsReadOnly = false;
        public bool IsReadOnly
        {
            get
            {
                if (PurchaseOrder.Approval != null)
                {
                    if (PurchaseOrder.Approval.IsApproved == null || PurchaseOrder.Approval.IsApproved == true)
                    {
                        m_IsReadOnly = true;
                    }
                    else
                    {
                        m_IsReadOnly = false;
                    }
                }


                return m_IsReadOnly;
            }
            set
            {
                m_IsReadOnly = value;
                OnPropertyChanged("IsReadOnly");
            }
        }


        private Currency m_POCurrency = null;
        public Currency POCurrency
        {
            get
            {
                return m_POCurrency;
            }
            set
            {
                m_POCurrency = value;
                PurchasableMaterials = null;
                foreach (var item in SelectedItems)
                {
                    var pmItem = item as ProductMaterialItem;
                    if (pmItem  != null)
                    {
                        pmItem.IsSelectedToGeneratePO = false;
                    }
                }
                OnPropertyChanged("POCurrency");
            }
        }


        private PurchaseOrder m_PurchaseOrder = null;
        public PurchaseOrder PurchaseOrder
        {
            get
            {
                if (m_PurchaseOrder == null)
                {
                    m_PurchaseOrder = new PurchaseOrder();
                    m_PurchaseOrder.PurchaseOrderStatusID = 1;
                    POCurrency = DBResources.Instance.Context.Currencies.Find(1);
                    m_PurchaseOrder.PurchaseOrderDateWrapper = DBResources.Instance.GetServerTime();
                    m_PurchaseOrder.PropertyChanged += m_PurchaseOrder_PropertyChanged;
                }
                return m_PurchaseOrder;
            }
            set
            {
                m_PurchaseOrder = value;
                if(m_PurchaseOrder != null)
                    m_PurchaseOrder.PropertyChanged += m_PurchaseOrder_PropertyChanged;

                var firstItem = value.ProductMaterialItems.FirstOrDefault();
                if (firstItem != null)
                {
                    POCurrency = firstItem.Currency;
                }
                else
                {
                    var firstOrderedItem = value.OrderedItems.FirstOrDefault();
                    if (firstOrderedItem != null)
                    {
                        POCurrency = firstOrderedItem.Currency;
                    }
                }
            }
        }

        void m_PurchaseOrder_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Supplier")
            {
                m_PurchaseOrder.PurchaseOrderNumberWrapper = Constants.GetPurchaseOrderNumber(m_PurchaseOrder.Supplier, Order);
            }
        }

        private ObservableCollection<ProductMaterialItem> materialList = null;
        public ObservableCollection<ProductMaterialItem> PurchasableMaterials
        {
            get
            {
                if (materialList == null)
                {
                    materialList = new ObservableCollection<ProductMaterialItem>();
                    foreach (OrderProduct product in Order.OrderProducts)
                    {
                        foreach (ProductMaterial budgetMaterial in product.ProductMaterials)
                        {
                            if (budgetMaterial.Approval == null || (budgetMaterial.Approval.IsApproved != null && budgetMaterial.Approval.IsApproved.Value))
                            {
                                foreach (ProductMaterialItem purchasableMaterial in budgetMaterial.ProductMaterialItems)
                                {
                                    if (purchasableMaterial.PurchaseOrderID != null && purchasableMaterial.PurchaseOrderID.Value == this.PurchaseOrder.PurchaseOrderID)
                                        purchasableMaterial.IsSelectedToGeneratePO = true;

                                    if (purchasableMaterial.CurrencyID == POCurrency.CurrencyID && (purchasableMaterial.PurchaseOrder == null || purchasableMaterial.PurchaseOrder.PurchaseOrderID == this.PurchaseOrder.PurchaseOrderID))
                                    {
                                        materialList.Add(purchasableMaterial);
                                        purchasableMaterial.PropertyChanged += purchasableMaterial_PropertyChanged;
                                        purchasableMaterial.CalculateItemCost();
                                    }
                                }
                            }
                        }
                    }
                }

                return materialList;
            }
            private set
            {
                materialList = null;
            }
        }

        private ObservableCollection<IPurchaseOrderItem> m_SelectedItems = null;
        public ObservableCollection<IPurchaseOrderItem> SelectedItems
        {
            get
            {
                if (m_SelectedItems == null)
                {

                    if (PurchaseOrder.ProductMaterialItems.Count == 0 && PurchaseOrder.OrderedItems.Count != 0)
                    {
                        m_SelectedItems = new ObservableCollection<IPurchaseOrderItem>(PurchaseOrder.OrderedItems);
                    }
                    else
                    {
                        foreach (var item in PurchaseOrder.ProductMaterialItems)
                        {
                            item.IsSelectedToGeneratePO = true;
                        }
                        m_SelectedItems = new ObservableCollection<IPurchaseOrderItem>(PurchaseOrder.ProductMaterialItems);
                    }
                }

                return m_SelectedItems;
            }
            set
            {
                m_SelectedItems = value;
            }
        }

        void purchasableMaterial_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelectedToGeneratePO")
            {
                ProductMaterialItem item = sender as ProductMaterialItem;
                if (item.IsSelectedToGeneratePO)
                {
                    TotalPOValue += item.ItemCostInItemCurrency;
                    SelectedItems.Add(item);
                }
                else
                {
                    TotalPOValue -= item.ItemCostInItemCurrency;
                    this.PurchaseOrder.ProductMaterialItems.Remove(item);
                    item.PurchaseOrder = null;
                    SelectedItems.Remove(item);
                }
            }
        }

        private decimal m_TotalPOValue = -1;
        public decimal TotalPOValue
        {
            get
            {
                if (m_TotalPOValue == -1)
                {
                    m_TotalPOValue = 0;
                    foreach (var item in SelectedItems)
                    {
                        m_TotalPOValue += item.ItemCostInItemCurrency;
                    }
                }
                return m_TotalPOValue;
            }
            set
            {
                m_TotalPOValue = value;
                OnPropertyChanged("TotalPOValue");
            }
        }


        public void ResetUserSelection()
        {
            foreach (ProductMaterialItem purchasableMaterial in PurchasableMaterials)
            {
                purchasableMaterial.IsSelectedToGeneratePO = false;
                purchasableMaterial.PropertyChanged -= purchasableMaterial_PropertyChanged;
            }

            m_PurchaseOrder.PropertyChanged -= m_PurchaseOrder_PropertyChanged;
        }

    }
}
