using MahApps.Metro.Controls;
using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using ordermanager.ViewModel.PurchaseOrderControl;
using ordermanager.Views.PopUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ordermanager.Views.UserControls.PurchaseOrderControls
{
    /// <summary>
    /// Interaction logic for CreateNewPurchaseOrderControl.xaml
    /// </summary>
    public partial class CreateNewPurchaseOrderControl : UserControl
    {
        public CreateNewPurchaseOrderControl()
        {
            InitializeComponent();
        }

        private CreateNewPurchaseOrderViewModel m_ViewModel = null;
        public CreateNewPurchaseOrderViewModel ViewModel
        {
            get
            {
                return m_ViewModel;
            }
            set
            {
                m_ViewModel = value;
                this.DataContext = value;
            }
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

        public void SetOrder(Order order)
        {
            if (ViewModel == null || ViewModel.Order != order)
            {
                this.Order = order;
                ViewModel = new CreateNewPurchaseOrderViewModel(order);
            }
        }

        #region Supplier Management

            private void addNewSupplierBtn_Click_1(object sender, RoutedEventArgs e)
            {
                var newSupplier = NewCompany("Supplier", supplierComboBox.Text);
                if (newSupplier != null)
                {
                    supplierComboBox.SelectedItem = newSupplier;
                }
            }

            private void editExistingSupplier_Click_1(object sender, RoutedEventArgs e)
            {
                EditCompany(supplierComboBox.SelectedItem as Company);
            }

            private void supplierComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
            {
                if (supplierComboBox.SelectedItem == null)
                {
                    addNewSupplierBtn.Visibility = System.Windows.Visibility.Visible;
                    editExistingSupplier.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    addNewSupplierBtn.Visibility = System.Windows.Visibility.Collapsed;
                    editExistingSupplier.Visibility = System.Windows.Visibility.Visible;
                }
            }
   
            private void EditCompany(Company company)
            {
                CustomerDetailsControl details = new CustomerDetailsControl(company.CompanyType.Type);
                details.DataContext = company;

                if (details.ShowDialog() == true)
                {
                    DBResources.Instance.Save();
                }
            }

            private Company NewCompany(string type, string companyName)
            {
                CustomerDetailsControl details = new CustomerDetailsControl(type);
                Company newCompany = new Company();
                newCompany.Name = companyName;
                details.DataContext = newCompany;
                if (details.ShowDialog() == true)
                {
                    return DBResources.Instance.SaveNewCompany(newCompany, type);
                }
                return null;
            }

        #endregion 

        #region Purchase Order Generate & Discard Management

            private void btnGeneratePO_Click_1(object sender, RoutedEventArgs e)
            {
                ViewModel.PurchaseOrder.Validate();
                if (!ViewModel.PurchaseOrder.HasErrors)
                {
                    foreach (ProductMaterialItem item in ViewModel.SelectedItems)
                    {
                        OrderedItem itemToOrder = new OrderedItem { ProductMaterialItem = item, OrderedQuantity = item.Quantity };
                        item.SupplierWrapper = ViewModel.PurchaseOrder.Company;
                        ViewModel.PurchaseOrder.OrderedItems.Add(itemToOrder);
                        Order.PurchaseOrders.Add(ViewModel.PurchaseOrder);
                    }

                    DBResources.Instance.Save();
                    InformUser("Purchase Order Successfully Created");
                    Reset();
                }
                else
                {
                    InformUser("Fix the Highlighted Errors and try again");
                }
            }

            private void InformUser(string message)
            {
                PopupBox informer = new PopupBox();
                informer.Message = message;
                informer.PopupButton = PopupButton.OK;
                informer.ShowDialog();
            }

            private void btnDiscard_Click(object sender, RoutedEventArgs e)
            {
                Reset();
            }

            public void Reset()
            {
                ViewModel.ResetUserSelection();
                ViewModel = null;
                ViewModel = new CreateNewPurchaseOrderViewModel(Order);
            }

        #endregion 

        private void btnChooseItems_Click_1(object sender, RoutedEventArgs e)
        {
            BillOfMaterialBrowser bomBrowser = new BillOfMaterialBrowser(ViewModel.Order, ViewModel.PurchasableMaterials);
            bomBrowser.ShowDialog();
        }

       
    }
}
