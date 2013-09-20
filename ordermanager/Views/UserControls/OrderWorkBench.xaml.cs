using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for OrderWorkBench.xaml
    /// </summary>
    public partial class OrderWorkBench : UserControl
    {
        public event OnGoBackDelegate OnGoBack = null;
        public OrderWorkBench()
        {
            InitializeComponent();
        }

        private void GoBack_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (OnGoBack != null)
            {
                OnGoBack();
            }
        }

        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void Button_PreviewMouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (OnGoBack != null)
                OnGoBack();
        }

    }
}
