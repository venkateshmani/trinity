﻿using ordermanager.ViewModel.Execution;
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
    /// Interaction logic for CuttingControl.xaml
    /// </summary>
    public partial class CuttingControl : UserControl
    {
        public CuttingControl()
        {
            InitializeComponent();
        }

        private CuttingViewModel m_ViewModel = null;
        public CuttingViewModel ViewModel
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExecutionTreeViewControl_SelectionChanged(DatabaseModel.OrderProduct product, DateTime date)
        {

        }
    }
}
