﻿using MahApps.Metro.Controls;
using ordermanager.DatabaseModel;
using ordermanager.Interfaces_And_Enums;
using ordermanager.Utilities;
using ordermanager.ViewModel.Execution;
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

namespace ordermanager.Views.UserControls.Execution
{
    /// <summary>
    /// Interaction logic for PackagingControl.xaml
    /// </summary>
    public partial class PackagingControl : UserControl, IJobExecutionView
    {
        public PackagingControl()
        {
            InitializeComponent();
            this.DataContextChanged += PackagingControl_DataContextChanged;
        }

        void PackagingControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ObjectDataProvider sub = this.FindResource("GetCartonBoxes") as ObjectDataProvider;
            if (sub != null && m_ViewModel != null && m_ViewModel.Order != null)
            {
                sub.MethodParameters.Clear();
                sub.MethodParameters.Add(m_ViewModel.Order.OrderID);
                sub.Refresh();
            }
        }

        #region View Model Initialization

        public void SetOrder(Order order)
        {
            ViewModel.Order = order;
        }

        private PackagingViewModel m_ViewModel = null;
        public PackagingViewModel ViewModel
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

        #endregion

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            m_ViewModel.Order.CalculatePackaging();
            m_ViewModel.Save("Saved packaging details.", "Packaging");
        }

        private void TreeViewItemSelected(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.OriginalSource as TreeViewItem;
            if (item != null)
            {
                TreeViewItem parent = ItemsControl.ItemsControlFromItemContainer(item) as TreeViewItem;
                if (parent != null && parent.Header is OrderProduct)
                {
                    ViewModel.SelectedDate = tvProducts.SelectedItem.ToString();
                    ViewModel.SelectedProduct = parent.Header as OrderProduct;
                    executionDetails.ItemsSource = ViewModel.SelectedProduct.GetPackagings(ViewModel.SelectedDate);
                    executionDetails.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    m_ViewModel.SelectedProduct = tvProducts.SelectedItem as OrderProduct;
                    executionDetails.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        private void cartonBoxNumber_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            ComboBox subMaterialsComboBox = sender as ComboBox;
            if (subMaterialsComboBox != null)
            {
                Grid parentGrid = subMaterialsComboBox.Parent as Grid;
                if (parentGrid != null)
                {
                    Button btnAddNewSubMaterial = GetControl(parentGrid, "btnAddCartonBox") as Button;
                    if (btnAddNewSubMaterial != null)
                    {
                        if (subMaterialsComboBox.SelectedItem != null)
                            btnAddNewSubMaterial.Visibility = System.Windows.Visibility.Collapsed;
                        else if (!string.IsNullOrWhiteSpace(subMaterialsComboBox.Text))
                            btnAddNewSubMaterial.Visibility = System.Windows.Visibility.Visible;
                        else
                            btnAddNewSubMaterial.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }
        }

        private object GetControl(Grid parent, string controlName)
        {
            if (parent != null)
            {
                return parent.FindName(controlName);
            }
            return null;
        }


        private void btnAddCartonBox_Click_1(object sender, RoutedEventArgs e)
        {   
            Button addCartonBoxButton = sender as Button;
            if (addCartonBoxButton != null)
            {
                Grid parentGrid = addCartonBoxButton.Parent as Grid;
                if (parentGrid != null)
                {
                    ComboBox cartonBoxNumberLists = GetControl(parentGrid, "cartonBoxNumberLists") as ComboBox;
                    if (cartonBoxNumberLists != null && !string.IsNullOrEmpty(cartonBoxNumberLists.Text))
                    {
                        if (ViewModel.AddNewCartonBox(cartonBoxNumberLists.Text))
                        {
                            addCartonBoxButton.Visibility = System.Windows.Visibility.Collapsed;
                        }
                        else
                        {
                            InformUser("Carton Box With Number " + cartonBoxNumberLists.Text.ToUpper() + " has been added already !, Try another one");
                        }
                    }
                }
            }
        }

        private void InformUser(string message)
        {
            PopupBox informer = new PopupBox();
            informer.Message = message;
            informer.PopupButton = PopupButton.OK;
            informer.ShowDialog();
        }
    }
}
