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

namespace ordermanager.Views.UserControls.JobOrderControls
{
    /// <summary>
    /// Interaction logic for CreateJOCtrl.xaml
    /// </summary>
    public partial class CreateJOCtrl : UserControl
    {

        IJobOrderControl selectedJobOrderControl = null;

        public CreateJOCtrl()
        {
            InitializeComponent();
        }

        

        private void jobOrderType_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                ComboBoxItem selectedItem = jobOrderType.SelectedItem as ComboBoxItem;
                if (selectedItem != null && selectedItem.Content != null)
                {
                    if (selectedItem.Content.ToString() == "Dyeing")
                    {
                        dyeingJOControl.Visibility = System.Windows.Visibility.Visible;
                        knittingJOControl.Visibility = System.Windows.Visibility.Hidden;

                        selectedJobOrderControl = dyeingJOControl;
                    }
                    else if (selectedItem.Content.ToString() == "Knitting")
                    {
                        dyeingJOControl.Visibility = System.Windows.Visibility.Hidden;
                        knittingJOControl.Visibility = System.Windows.Visibility.Visible;

                        selectedJobOrderControl = knittingJOControl;
                    }
                    else
                    {
                        dyeingJOControl.Visibility = System.Windows.Visibility.Hidden;
                        knittingJOControl.Visibility = System.Windows.Visibility.Hidden;

                        selectedJobOrderControl = null;
                    }
                }
            }
        }
    }
}
