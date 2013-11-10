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
    /// Interaction logic for ExecutionTreeViewControl.xaml
    /// </summary>
    public partial class ExecutionTreeViewControl : UserControl
    {
        public event OnSelectionChanged SelectionChanged = null;

        public ExecutionTreeViewControl()
        {
            InitializeComponent();
        }

        private void tvProducts_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

            if (SelectionChanged != null)
            {
                //For now. It should be worked. 
                SelectionChanged(null, DateTime.Now);
            }
        }
    }
}
