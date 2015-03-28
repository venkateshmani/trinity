using ordermanager.DatabaseModel;
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
using System.Windows.Shapes;

namespace ordermanager.Views.UserControls.JobOrderControls
{
    /// <summary>
    /// Interaction logic for DyeingJoWindow.xaml
    /// </summary>
    public partial class CreateJoWindow 
    {
        public CreateJoWindow()
        {
            InitializeComponent();
            createJoCtrl.OnCloseDialog += createJoCtrl_OnCloseDialog;
        }

        void createJoCtrl_OnCloseDialog(bool dialogResult)
        {
            this.DialogResult = dialogResult;
            this.Close();
        }

        public void InitializeForDyeing()
        {
            createJoCtrl.InitializeForDyeing();
        }

        public void InitializeForKnitting()
        {
            createJoCtrl.InitializeForKnitting();
        }

        public void InitializeForCompacting()
        {
            createJoCtrl.InitializeForCompacting();
        }

        public void Initialize(string jobOrderToInitialize)
        {
            switch (jobOrderToInitialize)
            {
                case "Dyeing":
                    InitializeForDyeing();
                    break;
                case "Knitting":
                    InitializeForKnitting();
                    break;
                case "Compacting":
                    InitializeForCompacting();
                    break;
            }
        }
             

        public bool JobOrderIssued
        {
            get
            {
                return createJoCtrl.JobOrderIssued;
            }
            set
            {
                createJoCtrl.JobOrderIssued = value;
            }
        }



        public decimal Quantity
        {
            get
            {
                return createJoCtrl.Quantity;
            }
            set
            {
                createJoCtrl.Quantity = value;
            }
        }

        public JobOrder ParentJobOrder
        {
            get
            {
                return createJoCtrl.ParentJobOrder;
            }
            set
            {
                createJoCtrl.ParentJobOrder = value;
            }
        }

        public string GRNRefNo
        {
            get
            {
                return createJoCtrl.GRNRefNo;
            }
            set
            {
                createJoCtrl.GRNRefNo = value;
            }
        }

        public GRNReciept GRNReciept
        {
            get
            {
                return createJoCtrl.GRNReciept;
            }
            set
            {
                createJoCtrl.GRNReciept = value;
            }
        }

        public Order Order
        {
            get
            {
                return createJoCtrl.Order;
            }
            set
            {
                createJoCtrl.Order = value;
            }
        }

        public PurchaseOrder PurchaseOrder
        {
            get
            {
                return createJoCtrl.PurchaseOrder;
            }
            set
            {
                createJoCtrl.PurchaseOrder = value;
            }
        }
    }
}
