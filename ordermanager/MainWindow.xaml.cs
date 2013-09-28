using MahApps.Metro;
using ordermanager.DatabaseModel;
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
        LoginScreen m_LoginScreen;
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
        }

        public MainWindow(LoginScreen loginScreen)
            : this()
        {
            m_LoginScreen = loginScreen;
            m_LoginScreen.Hide();
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

        private void ViewOrdersControl_OnOrderClick(object sender)
        {
            Order order = sender as Order;
            if (order != null)
            {
                viewOrdersTabControl.Visibility = System.Windows.Visibility.Collapsed;
                orderWorkBench.Order = order;
                orderWorkBench.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void orderWorkBench_OnGoBack_1()
        {
            viewOrdersTabControl.Visibility = System.Windows.Visibility.Visible;
            orderWorkBench.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void LaunchSettings(object sender, RoutedEventArgs e)
        {

        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            if (m_LoginScreen != null)
            {
                this.Closing -= MainWindow_Closing;
                this.Close();
                m_LoginScreen.ShowDialog();
            }
            else
                Application.Current.Shutdown();
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
