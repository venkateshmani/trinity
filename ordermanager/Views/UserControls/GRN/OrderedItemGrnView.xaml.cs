using MahApps.Metro.Controls;
using ordermanager.DatabaseModel;
using ordermanager.Interfaces_And_Enums;
using ordermanager.ViewModel;
using ordermanager.Views.PopUps;
using ordermanager.Views.UserControls.JobOrderControls;
using ordermanager.Views.UserControls.PurchaseOrderControls;
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

namespace ordermanager.Views.UserControls.GRN
{
    /// <summary>
    /// Interaction logic for OrderedItemGrnView.xaml
    /// </summary>
    public partial class OrderedItemGrnView : UserControl
    {
        public OrderedItemGrnView()
        {
            InitializeComponent();
        }

        private OrderedItemGrnViewModel m_ViewModel = null;
        public OrderedItemGrnViewModel ViewModel
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

        private void SupplierComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBox supplierComboBox = sender as ComboBox;
            if (supplierComboBox != null)
            {
                StackPanel parentContainer = supplierComboBox.Parent as StackPanel;
                if (parentContainer != null)
                {
                    Button btnAddSupplier = GetControl(parentContainer, "btnAddSupplier") as Button;
                    Button btnEditSupplier = GetControl(parentContainer, "btnEditSupplier") as Button;
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

                    Button btnGeneratePo = GetControl(parentContainer, "generateNewPO") as Button;
                    btnGeneratePo.Tag = supplierComboBox.SelectedItem as Company;
                }
            }
        }

        private void AddNewSupplier_Click(object sender, RoutedEventArgs e)
        {
            Button btnAddSupplier = sender as Button;
            if (btnAddSupplier != null && btnAddSupplier.Tag != null)
            {
                Company newSupplier = AddNewSupplier("Supplier", Convert.ToString(btnAddSupplier.Tag));
                if (newSupplier != null)
                {
                    StackPanel parentContainer = btnAddSupplier.Parent as StackPanel;
                    if (parentContainer != null)
                    {
                        ComboBox comboBox = GetControl(parentContainer, "supplierComboBox") as ComboBox;
                        if (comboBox != null)
                        {
                            comboBox.SelectedItem = newSupplier;
                            btnAddSupplier.Visibility = System.Windows.Visibility.Collapsed;
                            Button btnEditSupplier = GetControl(parentContainer, "btnEditSupplier") as Button;
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
                StackPanel parentContainer = btnEditSupplier.Parent as StackPanel;
                if (parentContainer != null)
                {
                    ComboBox comboBox = GetControl(parentContainer, "supplierComboBox") as ComboBox;
                    if (comboBox != null && comboBox.SelectedItem != null)
                    {
                        Company supplier = comboBox.SelectedItem as Company;
                        if (supplier != null)
                            EditSupplier(supplier);
                    }
                }
            }
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

        private object GetControl(FrameworkElement parent, string controlName)
        {
            if (parent != null)
            {
                return parent.FindName(controlName);
            }
            return null;
        }

        private void saveChanges_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        GRNReciept selectedGRNReciept = null;
        private void issueBtn_Click_1(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            selectedGRNReciept = btn.Tag as GRNReciept;
            btn.ContextMenu.IsOpen = true;
        }


        private JobOrderType GetJobOrderType(object sender)
        {
            Button issueToBtn = sender as Button;
            if (issueToBtn != null)
            {
                StackPanel parentContainer = issueToBtn.Parent as StackPanel;

                if (parentContainer != null)
                {
                    ComboBox comboBox = parentContainer.FindName("issueToComboBox") as ComboBox;
                    if (comboBox != null)
                    {
                        return comboBox.SelectedItem as JobOrderType;
                    }
                }
            }

            return null;
        }

        private void generateNewPO_Click(object sender, RoutedEventArgs e)
        {
            Button btnGeneratePo = sender as Button;

            Company Supplier = null;
            if (btnGeneratePo != null && btnGeneratePo.Tag != null)
            {
                Supplier = btnGeneratePo.Tag as Company;
            }

            if (Supplier != null)
            {
                PurchaseOrder newPurchaseOrder = new PurchaseOrder();
                newPurchaseOrder.PurchaseOrderNumber = Constants.GetPurchaseOrderNumber(ViewModel.SelectedGRNReceipt.OrderedItem.PurchaseOrder.Company, ViewModel.SelectedGRNReceipt.OrderedItem.PurchaseOrder.Order);
                newPurchaseOrder.PurchaseOrderDate = DBResources.Instance.GetServerTime();
                newPurchaseOrder.PurchaseOrderStatusID = 1;
                newPurchaseOrder.Company = Supplier;
                newPurchaseOrder.TermsAndConditions = ViewModel.SelectedGRNReceipt.OrderedItem.PurchaseOrder.TermsAndConditions;

                OrderedItem newOrderedItem = ViewModel.OrderedItem.Clone() as OrderedItem;
                newOrderedItem.OrderedQuantity = ViewModel.SelectedGRNReceipt.QualityFailedQuantityWrapper;
                newOrderedItem.CostWrapper = ViewModel.SelectedGRNReceipt.OrderedItem.CostWrapper;
                newOrderedItem.TaxPerUnitWrapper = ViewModel.SelectedGRNReceipt.OrderedItem.TaxPerUnitWrapper;
                newOrderedItem.PurchaseOrder = newPurchaseOrder;
                
                newPurchaseOrder.OrderedItems.Add(newOrderedItem);

                ViewModel.SelectedGRNReceipt.SpawnedNewPurchaseOrder = true;
                ViewModel.SelectedGRNReceipt.PurchaseOrder = newPurchaseOrder;

                CreatePOWindow poWindow = new CreatePOWindow();
                poWindow.SetOrder(ViewModel.Order, newPurchaseOrder);

                if (poWindow.ShowDialog() == true)
                {
                    ViewModel.SelectedGRNReceipt.RefreshUIEnablers();
                }
            }
            else
            {
                InformUser("Please select the supplier to generate new purchase order");
            }

        }

        private void InformUser(string message)
        {
            PopupBox informer = new PopupBox();
            informer.Message = message;
            informer.PopupButton = PopupButton.OK;
            informer.ShowDialog();
        }

        private Company GetSupplier()
        {

            return null;
        }

        private void materialGRNGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (materialGRNGrid != null && ViewModel != null)
            {
                ViewModel.SelectedGRNReceipt = materialGRNGrid.SelectedItem as GRNReciept;
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            DependencyObject obj = item.GetParentObject();
            switch (item.Header.ToString())
            {
                case "Knitting":
                    CreateJoWindow knittWindow = new CreateJoWindow();
                    knittWindow.Order = this.selectedGRNReciept.OrderedItem.PurchaseOrder.Order;
                    knittWindow.PurchaseOrder = this.selectedGRNReciept.OrderedItem.PurchaseOrder;
                    knittWindow.Quantity = this.selectedGRNReciept.QualityPassedQuantity.Value;
                    knittWindow.GRNRefNo = this.selectedGRNReciept.GRNReciptID.ToString();
                    knittWindow.GRNReciept = this.selectedGRNReciept;
                    knittWindow.InitializeForKnitting();
                    knittWindow.ShowDialog();
                    break;
                case "Dyeing":
                    CreateJoWindow dyeWindow = new CreateJoWindow();
                    dyeWindow.Order = this.selectedGRNReciept.OrderedItem.PurchaseOrder.Order;
                    dyeWindow.PurchaseOrder = this.selectedGRNReciept.OrderedItem.PurchaseOrder;
                    dyeWindow.Quantity = this.selectedGRNReciept.QualityPassedQuantity.Value;
                    dyeWindow.GRNRefNo = this.selectedGRNReciept.GRNReciptID.ToString();
                    dyeWindow.GRNReciept = this.selectedGRNReciept;
                    dyeWindow.InitializeForDyeing();
                    dyeWindow.ShowDialog();
                    break;
                case "Compacting":
                    CreateJoWindow compactingWindow = new CreateJoWindow();
                    compactingWindow.Order = this.selectedGRNReciept.OrderedItem.PurchaseOrder.Order;
                    compactingWindow.PurchaseOrder = this.selectedGRNReciept.OrderedItem.PurchaseOrder;
                    compactingWindow.Quantity = this.selectedGRNReciept.QualityPassedQuantity.Value;
                    compactingWindow.GRNRefNo = this.selectedGRNReciept.GRNReciptID.ToString();
                    compactingWindow.GRNReciept = this.selectedGRNReciept;
                    compactingWindow.InitializeForCompacting();
                    compactingWindow.ShowDialog();
                    break;
                case "Printing":
                case "Washing":
                case "Other":
                case "Stock":
                    if (selectedGRNReciept != null)
                    {
                        GRNReciept receipt = selectedGRNReciept;
                        IssueToPopupBox issuePopupBox = new IssueToPopupBox(DBResources.Instance.AllJobsTypes, item.Header.ToString());
                        issuePopupBox.Receipt = receipt;
                        issuePopupBox.JobQuantity = issuePopupBox.Receipt.QualityPassedQuantityWrapper;

                        if (issuePopupBox.ShowDialog() == true)
                        {
                            if (issuePopupBox.JobOrder.JobOrderType.Type.ToLower() == "stock")
                            {
                                MaterialStock stock = new MaterialStock();
                                stock.Order = ViewModel.Order;
                                stock.SubMaterial = ViewModel.OrderedItem.ProductMaterialItem.SubMaterial;
                                stock.InStockDateTime = DBResources.Instance.GetServerTime();
                                stock.StockQuantity = receipt.QualityPassedQuantityWrapper;
                                stock.UnitsOfMeasurement = ViewModel.OrderedItem.ProductMaterialItem.UnitsOfMeasurementWrapper;
                                ViewModel.OrderedItem.ProductMaterialItem.SubMaterial.MaterialStocks.Add(stock);
                                receipt.ReceiptStatusID = (byte)RecieptStatus.IssuedToStock;
                            }
                            else
                            {
                                receipt.JobOrders.Add(issuePopupBox.JobOrder);
                            }

                            DBResources.Instance.Save();
                            ViewModel.SelectedGRNReceipt.RefreshUIEnablers();
                        }
                    }
                    break;
            }

            ViewModel.SelectedGRNReceipt.RefreshUIEnablers();
        }
      
    }
}
