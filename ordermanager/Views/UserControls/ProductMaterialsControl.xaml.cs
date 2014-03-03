using MahApps.Metro.Controls;
using ordermanager.DatabaseModel;
using ordermanager.Utilities;
using ordermanager.ViewModel;
using ordermanager.Views.PopUps;
using Reports;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ProductMaterials.xaml
    /// </summary>
    public partial class ProductMaterialsControl : UserControl
    {
        ProductMaterialsViewModel m_ViewModel;
        BudgetReportControl budgetReportControl = null;

        public ProductMaterialsControl()
        {
            InitializeComponent();
            budgetReportControl = new BudgetReportControl();
        }

        public ProductMaterialsViewModel ViewModel
        {
            get
            {
                return m_ViewModel;

            }
        }

        private void productsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrderProduct product = productsList.SelectedItem as OrderProduct;
            if (product != null && m_ViewModel != null)
                m_ViewModel.SelectedProduct = product;
        }

        private void AddNewMaterial(object sender, RoutedEventArgs e)
        {
            Button addBtn = sender as Button;
            if (addBtn != null)
            {
                Grid parentGrid = addBtn.Parent as Grid;
                if (parentGrid != null)
                {
                    ComboBox comboBox = parentGrid.FindName("materialsComboBox") as ComboBox;
                    if (comboBox != null && m_ViewModel != null)
                    {
                        comboBox.SelectedItem = m_ViewModel.CreateNewMaterial(comboBox.Text);
                        addBtn.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            m_ViewModel = DataContext as ProductMaterialsViewModel;
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            if (m_ViewModel != null)
            {
                m_ViewModel.AddNewMaterialItem();
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (m_ViewModel != null)
            {
                //m_ViewModel.ProductMaterialsList.Remove(materialsGrid.CurrentItem as ProductMaterial);             
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Persist(false);
        }

        private void InformUser(string message)
        {
            PopupBox informer = new PopupBox();
            informer.Message = message;
            informer.PopupButton = PopupButton.OK;
            informer.ShowDialog();
        }

        private void productsList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            productColumn.Width = productsList.ActualWidth;
        }

        private void comboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBox materialComboBox = sender as ComboBox;
            if (materialComboBox != null)
            {
                Grid parentGrid = materialComboBox.Parent as Grid;

                if (parentGrid != null)
                {
                    Button addbtn = parentGrid.FindName("addBtn") as Button;
                    if (addbtn != null)
                    {
                        if (materialComboBox.SelectedItem != null)
                        {
                            addbtn.Visibility = System.Windows.Visibility.Collapsed;
                        }
                        else
                        {
                            addbtn.Visibility = System.Windows.Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void discardBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Persist(true))
            {
                m_ViewModel.Refresh();
                InformUser("Successfully submitted to next level !");
            }
        }

        public bool Persist(bool isSubmit)
        {
            m_ViewModel.HasUserClickedSaveOrSubmit = true;
            if (m_ViewModel.HasError)
            {
                InformUser("Errors highlighted in red color !. Fix it and retry");
            }
            else
            {
                if (isSubmit)
                {
                    CommentBox commentBox = new CommentBox(Util.GetParentWindow(this));
                    if ((commentBox.ShowDialog() == true))
                    {
                        if (m_ViewModel.Save(isSubmit, commentBox.Comment))
                        {
                            btnAddNewItem.Visibility = System.Windows.Visibility.Collapsed;
                            materialsGrid.IsReadOnly = true;
                            return true;
                        }
                        return false;
                    }
                }
                else
                    return m_ViewModel.Save(isSubmit, "");
            }

            return false;
        }

        private void AddNewExtraCostItem(object sender, RoutedEventArgs e)
        {
            if (m_ViewModel != null)
            {
                Button addBtn = sender as Button;
                if (addBtn != null)
                {
                    Grid parentGrid = addBtn.Parent as Grid;
                    if (parentGrid != null)
                    {
                        ComboBox comboBox = parentGrid.FindName("extraTypeComboBox") as ComboBox;
                        if (comboBox != null && m_ViewModel != null)
                        {
                            comboBox.SelectedItem = m_ViewModel.CreateNewExtraCostType(comboBox.Text);
                            addBtn.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void AddNewExtraCostItem_Click(object sender, RoutedEventArgs e)
        {
            if (m_ViewModel != null)
            {
                m_ViewModel.AddNewExtraCostItem();
            }
        }

        private void extraTypeComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBox extraTypeComboBox = sender as ComboBox;
            if (extraTypeComboBox != null)
            {
                Grid parentGrid = extraTypeComboBox.Parent as Grid;

                if (parentGrid != null)
                {
                    Button addbtn = parentGrid.FindName("addBtn") as Button;
                    if (addbtn != null)
                    {
                        if (extraTypeComboBox.SelectedItem != null)
                        {
                            addbtn.Visibility = System.Windows.Visibility.Collapsed;
                        }
                        else
                        {
                            addbtn.Visibility = System.Windows.Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void btnGeneratePDF_Click_1(object sender, RoutedEventArgs e)
        {
            //Generate Parameters
            BudgetReportParameters parameters = new BudgetReportParameters();
            parameters.Buyer = ViewModel.Order.Company.Name;
            parameters.CurrencyValueInINR = GetFormattedValueString(ViewModel.SelectedProduct.CurrencyValueInINR);
            parameters.DateOfGeneration = DBResources.Instance.GetServerTime().ToShortDateString();
            parameters.ExpectedQuantity = GetFormattedValueString( ViewModel.SelectedProduct.ExpectedQuantity);
            parameters.OrderDate = ViewModel.Order.OrderDate.ToShortDateString();
            parameters.OrderedProductCurrency = ViewModel.SelectedProduct.Currency.Symbol;
            parameters.OrderID = ViewModel.Order.OrderID.ToString();
            parameters.OurPrice = GetFormattedValueString(ViewModel.SelectedProduct.OurCostInProductCurrenyValue)  + " " + parameters.OrderedProductCurrency;
            parameters.PerUnitBuyerTargetPrice = GetFormattedValueString(ViewModel.SelectedProduct.CustomerTargetPrice) + " " + parameters.OrderedProductCurrency;
            parameters.ProductName = ViewModel.SelectedProduct.ProductName.Name;
            parameters.ProfitOrLoss = GetFormattedValueString(ViewModel.SelectedProduct.ProfitOrLossAmount) + " " + parameters.OrderedProductCurrency;
            parameters.StyleID = ViewModel.SelectedProduct.ProductName.StyleID;
            parameters.TotalValue = GetFormattedValueString(ViewModel.SelectedProduct.OrderValue) + " " + "INR";
            parameters.NumberOfItems = ViewModel.SelectedProduct.ProductMaterials.Count.ToString();
            parameters.OrderConfirmComment = "No Comments";

            foreach (History history in ViewModel.Order.Histories)
            {
                if (history.OrderChanges.Contains("confirmed the Order"))
                {
                    parameters.OrderConfirmComment = history.Comment;
                }
            }

            string tempFilePathForPdf = System.IO.Path.Combine(
                                           System.IO.Path.GetTempPath(), "OM_Budget-" + parameters.OrderID + "-" + parameters.StyleID + "-" + parameters.DateOfGeneration.Replace(@"/","_") + ".pdf");

            if (File.Exists(tempFilePathForPdf))
            {
                File.Delete(tempFilePathForPdf);
            }

            budgetReportControl.SetParameters(parameters);
            budgetReportControl.CreateReportAsPDF(ViewModel.Order.OrderID, ViewModel.SelectedProduct.ProductID, tempFilePathForPdf);
            System.Diagnostics.Process.Start(tempFilePathForPdf);
        }

        private string GetFormattedValueString(decimal value)
        {
            return String.Format(((Math.Round(value) == value) ? "{0:0}" : "{0:0.00}"), value);
        }

        private void materialsComboBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
        }
    }
}
