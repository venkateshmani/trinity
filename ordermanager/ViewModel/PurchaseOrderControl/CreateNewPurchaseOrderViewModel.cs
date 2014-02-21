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

        private PurchaseOrder m_PurchaseOrder = null;
        public PurchaseOrder PurchaseOrder
        {
            get
            {
                if (m_PurchaseOrder == null)
                {
                    m_PurchaseOrder = new PurchaseOrder();
                    m_PurchaseOrder.PurchaseOrderStatusID = 1;
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
                            foreach (ProductMaterialItem purchasableMaterial in budgetMaterial.ProductMaterialItems)
                            {
                                materialList.Add(purchasableMaterial);
                                purchasableMaterial.PropertyChanged += purchasableMaterial_PropertyChanged;
                                purchasableMaterial.CalculateItemCost();
                            }
                        }
                    }
                }

                return materialList;
            }
        }

        private ObservableCollection<ProductMaterialItem> m_SelectedItems = null;
        public ObservableCollection<ProductMaterialItem> SelectedItems
        {
            get
            {
                if (m_SelectedItems == null)
                {
                    foreach (var item in PurchaseOrder.ProductMaterialItems)
                    {
                        item.IsSelectedToGeneratePO = true;
                    }
                    m_SelectedItems = new ObservableCollection<ProductMaterialItem>(PurchaseOrder.ProductMaterialItems);
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
                    SelectedItems.Add(item);
                }
                else
                {
                    item.PurchaseOrder = null;
                    SelectedItems.Remove(item);
                }
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
