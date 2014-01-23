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
    /// Interaction logic for OrderReportCardItemControl.xaml
    /// </summary>
    public partial class OrderReportCardItemControl : UserControl
    {
        public OrderReportCardItemControl()
        {
            InitializeComponent();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            OrderReportCard card = e.NewValue as OrderReportCard;
            //try
            //{
            //    if (card != null)
            //    {
            //        gridProgress.ColumnDefinitions[0].Width = new GridLength((double)card.ProgressPercentage, GridUnitType.Star);
            //        gridProgress.ColumnDefinitions[1].Width = new GridLength((double)(100 - card.ProgressPercentage), GridUnitType.Star);
            //    }
            //}
            //catch { }
        }
    }
}
