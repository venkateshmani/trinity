﻿using MahApps.Metro.Controls;
using ordermanager.DatabaseModel;
using ordermanager.Utilities;
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
    /// Interaction logic for ProductMaterials.xaml
    /// </summary>
    public partial class ProductMaterialsControl : UserControl
    {
        ProductMaterialsViewModel m_ViewModel;

        public ProductMaterialsControl()
        {
            InitializeComponent();
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

            if (m_ViewModel != null)
                m_ViewModel.PropertyChanged -= m_ViewModel_PropertyChanged;
            m_ViewModel = DataContext as ProductMaterialsViewModel;           
            if (m_ViewModel != null)
            {
                m_ViewModel.PropertyChanged += m_ViewModel_PropertyChanged;
                SetControlState();
            }
        }

        void m_ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Order")
                SetControlState();
        }

        private void SetControlState()
        {
            if (m_ViewModel != null)
            {
                gridButtons.Visibility = m_ViewModel.ActionButtonsVisibility;
                spAddDeleteButtons.Visibility = m_ViewModel.NewItemAddBtnVisibility;
                gridDetails.IsEnabled = m_ViewModel.IsEnabled;             
            }
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
            if (Persist(false))
            {
                InformUser("Saved Successfully !!");
            }
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
                 InformUser("Successfully submitted to next level !");
            }
        }

        private bool Persist(bool isSubmit)
        {
            if (m_ViewModel != null)
            {
                CommentBox commentBox = new CommentBox(Util.GetParentWindow(this));

                m_ViewModel.HasUserClickedSaveOrSubmit = true;
                if (m_ViewModel.HasError)
                {
                    InformUser("Errors highlighted in red color !. Fix it and retry");
                }
                else
                {
                    if ((commentBox.ShowDialog() == true))
                    {
                        if (m_ViewModel.Save(isSubmit, commentBox.Comment))
                        {
                            if (isSubmit)
                            {
                                btnAddNewItem.Visibility = System.Windows.Visibility.Hidden;
                                materialsGrid.IsReadOnly = true;
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
