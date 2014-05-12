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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ordermanager.Views.CommonControls
{
    /// <summary>
    /// Interaction logic for BudgetVsActual.xaml
    /// </summary>
    public partial class BudgetVsActual : UserControl
    {
        public BudgetVsActual()
        {
            InitializeComponent();
        }

        private decimal m_BudgetInINR = 0;
        public decimal BudgetInINR
        {
            get
            {
                return m_BudgetInINR;
            }
            set
            {
                m_BudgetInINR = Math.Round(value,2);
                budgetTextBlock.Text = m_BudgetInINR.ToString();
            }
        }

        private decimal m_PreviousActualInINR = 0;
        public decimal PreviousActualInINR
        {
            get
            {
                return m_PreviousActualInINR;
            }
            set
            {
                m_PreviousActualInINR = Math.Round(value,2);
                actualTextBlock.Text = Math.Round(PreviousActualInINR + m_CurrentJOValue, 2).ToString();
            }
        }

        private decimal m_CurrentJOValue = 0;
        public decimal CurrentJOValue
        {
            get
            {
                return m_CurrentJOValue;
            }
            set
            {
                m_CurrentJOValue = Math.Round(value, 2);
                actualTextBlock.Text = Math.Round(PreviousActualInINR + m_CurrentJOValue, 2).ToString();
            }
        }

        public void Initialize(OrderedItem orderedItem, JobOrder parentJO)
        {
            CalculateBudget(orderedItem);
            CalculateActual(orderedItem, parentJO);
        }

        private void CalculateBudget(OrderedItem orderedItem)
        {
            BudgetInINR = orderedItem.BudgetInINR / orderedItem.OrderedQuantity;
        }

        private void CalculateActual(OrderedItem orderedItem, JobOrder Jo)
        {
            //Job Order Charges
            PreviousActualInINR += Jo.GetCumulativeJOCharges();

            //GRN Reciept Value
            if (Jo.GRNReciept.OtherChargesInINR != null && Jo.GRNReciept.OtherChargesInINR.Value != 0 && Jo.GRNReciept.RecievedInHandWrapper != 0)
            {
                decimal perUnitGRNRecieptValue = Jo.GRNReciept.OtherChargesInINR.Value / Jo.GRNReciept.RecievedInHandWrapper;
                PreviousActualInINR += perUnitGRNRecieptValue;
            }

            //Purchase Order Value
            PreviousActualInINR += orderedItem.ActualInINR / orderedItem.OrderedQuantity;
        }
             
    }
}
