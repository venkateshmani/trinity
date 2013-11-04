using ordermanager.ViewModel;
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
    /// Interaction logic for POProductDetails.xaml
    /// </summary>
    public partial class POProductDetails : UserControl
    {
        PurchaseOrderControlViewModel m_ViewModel;
        public POProductDetails()
        {
            InitializeComponent();
        }

        public PurchaseOrderControlViewModel ViewModel
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


        public bool IsReadOnly
        {
            get
            {
                return m_ViewModel.IsReadOnly;
            }
            set
            {
                m_ViewModel.IsReadOnly = value;
                countryWiseBreakUpGrid.IsReadOnly = value;
            }
        }

        private void UserControl_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void btnAddNewItem_Click(object sender, RoutedEventArgs e)
        {
            if (m_ViewModel != null)
                m_ViewModel.AddNewBreakUp();
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {

        }

        #region Country

            private void countryComboBox_TextChanged(object sender, TextChangedEventArgs e)
            {
                ComboBox countryComboBox = sender as ComboBox;
                if (countryComboBox != null)
                {
                    Grid parentGrid = countryComboBox.Parent as Grid;

                    if (parentGrid != null)
                    {
                        Button addbtn = parentGrid.FindName("btnAddNewCountry") as Button;
                        if (addbtn != null)
                        {
                            if (countryComboBox.SelectedItem != null)
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

            private void btnAddNewCountry_Click(object sender, RoutedEventArgs e)
            {
                Button addBtn = sender as Button;
                if (addBtn != null)
                {
                    Grid parentGrid = addBtn.Parent as Grid;
                    if (parentGrid != null)
                    {
                        ComboBox comboBox = parentGrid.FindName("countryComboBox") as ComboBox;
                        if (comboBox != null && m_ViewModel != null)
                        {
                            comboBox.SelectedItem = m_ViewModel.AddNewCountry(comboBox.Text);
                            addBtn.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }
                }
            }

        #endregion 

        #region  ProductSize

            private void sizeComboBox_TextChanged(object sender, TextChangedEventArgs e)
            {
                ComboBox sizeComboBox = sender as ComboBox;
                if (sizeComboBox != null)
                {
                    Grid parentGrid = sizeComboBox.Parent as Grid;

                    if (parentGrid != null)
                    {
                        Button addbtn = parentGrid.FindName("btnAddNewSize") as Button;
                        if (addbtn != null)
                        {
                            if (sizeComboBox.SelectedItem != null)
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

            private void btnAddNewSize_Click(object sender, RoutedEventArgs e)
            {
                Button addBtn = sender as Button;
                if (addBtn != null)
                {
                    Grid parentGrid = addBtn.Parent as Grid;
                    if (parentGrid != null)
                    {
                        ComboBox comboBox = parentGrid.FindName("sizeComboBox") as ComboBox;
                        if (comboBox != null && m_ViewModel != null)
                        {
                            comboBox.SelectedItem = m_ViewModel.AddNewProductSize(comboBox.Text);
                            addBtn.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }
                }
            }

        #endregion 

        #region Color

        private void btnAddNewColor_Click_1(object sender, RoutedEventArgs e)
        {
            Button addBtn = sender as Button;
            if (addBtn != null)
            {
                Grid parentGrid = addBtn.Parent as Grid;
                if (parentGrid != null)
                {
                    ComboBox comboBox = parentGrid.FindName("colorComboBox") as ComboBox;
                    if (comboBox != null && m_ViewModel != null)
                    {
                        comboBox.SelectedItem = m_ViewModel.AddNewColor(comboBox.Text);
                        addBtn.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }
        }

        private void colorComboBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            ComboBox colorComboBox = sender as ComboBox;
            if (colorComboBox != null)
            {
                Grid parentGrid = colorComboBox.Parent as Grid;

                if (parentGrid != null)
                {
                    Button addbtn = parentGrid.FindName("btnAddNewColor") as Button;
                    if (addbtn != null)
                    {
                        if (colorComboBox.SelectedItem != null)
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

        #endregion 
    }
}
