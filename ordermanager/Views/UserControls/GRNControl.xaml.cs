using MahApps.Metro.Controls;
using ordermanager.DatabaseModel;
using ordermanager.Extended_Database_Models;
using ordermanager.ViewModel;
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

namespace ordermanager.Views.UserControls
{
    /// <summary>
    /// Interaction logic for GRNControl.xaml
    /// </summary>
    public partial class GRNControl : UserControl
    {
        public GRNControl()
        {
            InitializeComponent();
            this.Loaded +=GRNControl_Loaded;
        }

        GRNViewModel m_GRNViewModel = null;
        public GRNViewModel ViewModel
        {
            get
            {
                return m_GRNViewModel;
            }
            set
            {
                m_GRNViewModel = value;
                this.DataContext = value;
            }
        }
        void GRNControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel = new GRNViewModel();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.HasErrors)
            {
                InformUser("Fix the Errors highlighted in red borders and try again !!");
            }
            else
            {
                Dictionary<Company, PurchaseOrder> supplierMapping = new Dictionary<Company, PurchaseOrder>();
                foreach (OrderedItem orderedItem in ViewModel.SelectedPurchaseOrder.OrderedItems)
                {

                    if (orderedItem.QualityFailedQuantityWrapper != 0 && (orderedItem.SpawnedNewPurchaseOrder == null || orderedItem.SpawnedNewPurchaseOrder.Value != true) && orderedItem.Supplier != null)
                    {
                        PurchaseOrder po = null;
                        if (supplierMapping.ContainsKey(orderedItem.Supplier))
                        {
                            po = supplierMapping[orderedItem.Supplier];
                        }
                        else
                        {
                            po = new PurchaseOrder();
                            string purchaseOrderNumber = Constants.GetPurchaseOrderNumber(orderedItem.Supplier, ViewModel.SelectedPurchaseOrder.Order);
                            po.PurchaseOrderNumber = purchaseOrderNumber;
                            po.PurchaseOrderStatusID = 1;

                            ViewModel.SelectedPurchaseOrder.Order.PurchaseOrders.Add(po);
                            po.SupplierID = orderedItem.Supplier.CompanyID;
                            supplierMapping.Add(orderedItem.Supplier, po);
                        }

                        OrderedItem child = orderedItem.Clone() as OrderedItem;
                        child.OrderedItemStatuWrapper = DBResources.Instance.Context.OrderedItemStatus.Find(1); //1 = None

                        po.OrderedItems.Add(child);

                        orderedItem.SpawnedNewPurchaseOrderWrapper = true;
                        orderedItem.SpawnedPurchaseOrder = po;
                        orderedItem.OrderedItemStatuWrapper = DBResources.Instance.Context.OrderedItemStatus.Find(3); //New Purchase Order Generated
                    }

                    if (orderedItem.QualityFailedQuantityWrapper == 0)
                        orderedItem.OrderedItemStatuWrapper = DBResources.Instance.Context.OrderedItemStatus.Find(2); //Completed

                }

                ViewModel.Save(false);
                InformUser("Sucessfully Saved !!");
                saveBtn.Content = "Save Changes";
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.HasErrors)
            {
                InformUser("Fix the Errors highlighted in red borders and try again !!");
            }
            else
            {
                ViewModel.Save(true);
                InformUser("Submitted Successfully !!");
            }
        }

        private void InformUser(string message)
        {
            PopupBox informer = new PopupBox();
            informer.Message = message;
            informer.PopupButton = PopupButton.OK;
            informer.ShowDialog();
        }


        private void tvProducts_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tvProducts.SelectedItem != null)
            {
                DBResources.Instance.DiscardChanges();
                if (tvProducts.SelectedItem is Company)
                {
                    supplierSelectedInfo.Visibility = System.Windows.Visibility.Visible;
                    grnDetailsControl.Visibility = System.Windows.Visibility.Hidden;
                }
                else if (tvProducts.SelectedItem is PurchaseOrder)
                {
                    PurchaseOrder po = tvProducts.SelectedItem as PurchaseOrder;
                    ViewModel.SelectedPurchaseOrder = po;
                    materialsReciptGrid.ItemsSource = po.OrderedItems;
                    qualityGrid.ItemsSource = po.OrderedItems;
                    grnDetailsControl.Visibility = System.Windows.Visibility.Visible;
                    supplierSelectedInfo.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        private void DetermineGeneratePOText()
        {
            foreach (OrderedItem item in ViewModel.SelectedPurchaseOrder.OrderedItems)
            {
                if (item.Supplier != null && item.SpawnedPurchaseOrder == null)
                {
                    saveBtn.Content = "Save Changes and Generate PO(s)";
                    return;
                }
            }

            saveBtn.Content = "Save Changes";
        }

        private void generatePO_Click(object sender, RoutedEventArgs e)
        {
          
            DBResources.Instance.Save();
        }

        private void AddNewSupplier_Click(object sender, RoutedEventArgs e)
        {
            Button btnAddSupplier = sender as Button;
            if (btnAddSupplier != null && btnAddSupplier.Tag != null)
            {
                Company newSupplier = AddNewSupplier("Supplier", Convert.ToString(btnAddSupplier.Tag));
                if (newSupplier != null)
                {
                    Grid parentGrid = btnAddSupplier.Parent as Grid;
                    if (parentGrid != null)
                    {
                        ComboBox comboBox = GetControl(parentGrid, "supplierComboBox") as ComboBox;
                        if (comboBox != null)
                        {
                            comboBox.SelectedItem = newSupplier;
                            btnAddSupplier.Visibility = System.Windows.Visibility.Collapsed;
                            Button btnEditSupplier = GetControl(parentGrid, "btnEditSupplier") as Button;
                            if (btnEditSupplier != null)
                                btnEditSupplier.Visibility = System.Windows.Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void EditSupplier_Click(object sender, RoutedEventArgs e)
        {
            Button btnEditSupplier = sender as Button;
            if (btnEditSupplier != null && btnEditSupplier.Tag != null)
            {
                Grid parentGrid = btnEditSupplier.Parent as Grid;
                if (parentGrid != null)
                {
                    ComboBox comboBox = GetControl(parentGrid, "supplierComboBox") as ComboBox;
                    if (comboBox != null && comboBox.SelectedItem != null)
                    {
                        Company supplier = comboBox.SelectedItem as Company;
                        if (supplier != null)
                            EditSupplier(supplier);

                    }
                }
            }
        }

        private void SupplierComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBox supplierComboBox = sender as ComboBox;
            if (supplierComboBox != null)
            {
                Grid parentGrid = supplierComboBox.Parent as Grid;
                if (parentGrid != null)
                {
                    Button btnAddSupplier = GetControl(parentGrid, "btnAddSupplier") as Button;
                    Button btnEditSupplier = GetControl(parentGrid, "btnEditSupplier") as Button;
                    if (btnAddSupplier != null && btnEditSupplier != null)
                    {
                        btnAddSupplier.Tag = supplierComboBox.Text;
                        btnEditSupplier.Tag = supplierComboBox.Text;
                        if (supplierComboBox.SelectedItem != null)
                        {
                            btnAddSupplier.Visibility = System.Windows.Visibility.Collapsed;
                            btnEditSupplier.Visibility = System.Windows.Visibility.Visible;
                        }
                        else if (!string.IsNullOrWhiteSpace(supplierComboBox.Text))
                        {
                            btnEditSupplier.Visibility = System.Windows.Visibility.Collapsed;
                            btnAddSupplier.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            btnAddSupplier.Visibility = System.Windows.Visibility.Collapsed;
                            btnEditSupplier.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }
                }

                DetermineGeneratePOText();
            }
        }

        private Company AddNewSupplier(string type, string companyName)
        {
            CustomerDetailsControl details = new CustomerDetailsControl("Supplier");
            Company newCompany = new Company();
            newCompany.Name = companyName;
            details.DataContext = newCompany;
            if (details.ShowDialog() == true)
            {
                return DBResources.Instance.SaveNewCompany(newCompany, type);
            }
            return null;
        }

        private bool EditSupplier(Company company)
        {
            CustomerDetailsControl details = new CustomerDetailsControl("Supplier");
            details.DataContext = company;
            if (details.ShowDialog() == true)
            {
                DBResources.Instance.Save();
                return true;
            }
            return false;
        }

        private object GetControl(Grid parent, string controlName)
        {
            if (parent != null)
            {
                return parent.FindName(controlName);
            }
            return null;
        }

    }
}
