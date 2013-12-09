﻿using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace ordermanager.Views.PopUps
{
    /// <summary>
    /// Interaction logic for IssueToPopupBox.xaml
    /// </summary>
    public partial class IssueToPopupBox
    {
        JobOrder m_jobOrder = null;
        public IssueToPopupBox()
        {
            InitializeComponent();
            JobOrder = new JobOrder();
            this.JobOrder.RequiredDateWrapper = DateTime.Now;
            

        }
        public IssueToPopupBox(JobOrder jOrder, ObservableCollection<JobOrderType> nextJobTypes):this()
        {          
            JobOrder = jOrder;
            JobOrder.RequiredDateWrapper = DateTime.Now;
            materialName.Text = jOrder.GRNReciept.OrderedItem.ProductMaterialItem.SubMaterial.Name;
            quantity.Text = jOrder.JobQuantity.ToString();
            issueToComboBox.ItemsSource = nextJobTypes;           
        }

        public JobOrder JobOrder
        {
            get
            {
                return m_jobOrder;
            }
            set
            {
                m_jobOrder = value;
                this.DataContext = value;
            }
        }


        //public MaterialName MaterialName
        //{
        //    get;
        //    set;
        //}


        public decimal JobQuantity
        {
            get
            {
                return JobOrder.JobQuantity;
            }
            set
            {
                JobOrder.JobQuantity = value;
            }
        }

        //public JobOrderType IssueTo
        //{
        //    get;
        //    set;
        //}

        //public decimal ChargesInINR
        //{
        //    get
        //    {
        //        return JobOrder.ChargesInINR;
        //    }
        //    set
        //    {
        //        JobOrder.ChargesInINR = value;
        //    }
        //}

        //public Company Supplier
        //{
        //    get
        //    {
        //        return JobOrder.Supplier;
        //    }
        //    set
        //    {
        //        JobOrder.Supplier = value;
        //    }
        //}

        //public string Instructions
        //{
        //    get;
        //    set;
        //}

        public DateTime RequiredDate
        {
            get;
            set;
        }

        public GRNReciept Receipt
        {
            get
            {
                return JobOrder.GRNReciept;
            }
            set
            {
                JobOrder.GRNReciept = value;
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void updateToDatabaseBtn_Click(object sender, RoutedEventArgs e)
        {
            JobOrder.Validate();
            if (!JobOrder.HasErrors)
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void SupplierComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void issueToComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (issueToComboBox.SelectedItem != null)
            {
                JobOrderType selectedJobOrder = issueToComboBox.SelectedItem as JobOrderType;
                if (selectedJobOrder.Type.ToLower() == "stock")
                {
                    ControlUIEnable(false);
                }
                else
                {
                    ControlUIEnable(true);
                }
            }
        }

        private void ControlUIEnable(bool isEnabled)
        {
            chargesInINR.IsEnabled = isEnabled;
            supplierComboBox.IsEnabled = isEnabled;
            jobInstruction.IsEnabled = isEnabled;
            expectedDeliveryDate.IsEnabled = isEnabled;
        }
    }
}
