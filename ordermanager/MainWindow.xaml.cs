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

namespace ordermanager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
                this.WindowState = System.Windows.WindowState.Normal;
            else
                this.WindowState = System.Windows.WindowState.Maximized;
        }

        private void ViewOrdersControl_OnOrderClick()
        {
            viewOrdersTabControl.Visibility = System.Windows.Visibility.Collapsed;
            orderWorkBench.Visibility = System.Windows.Visibility.Visible;
        }

        private void orderWorkBench_OnGoBack_1()
        {
            viewOrdersTabControl.Visibility = System.Windows.Visibility.Visible;
            orderWorkBench.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void LaunchSettings(object sender, RoutedEventArgs e)
        {

        }
       
    }
}
