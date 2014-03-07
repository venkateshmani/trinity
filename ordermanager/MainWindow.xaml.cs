using MahApps.Metro;
using ordermanager.DatabaseModel;
using ordermanager.Interfaces;
using ordermanager.ViewModel;
using ordermanager.Views.PopUps;
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
    public partial class MainWindow : IMaskable
    {
        LoginScreen m_LoginScreen;
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
            if (!DBResources.Instance.CurrentUser.UserRole.CanCreateUser)
                addNewUser.Visibility = System.Windows.Visibility.Collapsed;

            if (!DBResources.Instance.CurrentUser.UserRole.CanCreateNewEnquiry)
                newEnquiryTab.Visibility = System.Windows.Visibility.Collapsed;
            
            //if(!DBResources.Instance.CurrentUser.UserRole.CanModifyAssets)
            // assetsControl.Visibility = System.Windows.Visibility.Collapsed;
        }

        public MainWindow(LoginScreen loginScreen)
            : this()
        {
            m_LoginScreen = loginScreen;
            m_LoginScreen.Hide();
            UpdateUserName();

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

        private void UpdateUserName()
        {
            if (DBResources.Instance.CurrentUser != null)
            {
                userNameTextBlock.Text = "Hi, " + DBResources.Instance.CurrentUser.UserName + "!";
            }
            else
            {
                userNameTextBlock.Text = string.Empty;
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
                m_LoginScreen.ClearPassword();
                m_LoginScreen.ShowDialog();
                UpdateUserName();

            }
            else
                Application.Current.Shutdown();
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void Mask()
        {
            mask.Visibility = System.Windows.Visibility.Visible;
        }

        public void UnMask()
        {
            mask.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void addNewUser_Click(object sender, RoutedEventArgs e)
        {
            UserManagementWindow userManagementWindow = new UserManagementWindow();
            userManagementWindow.ShowDialog();
        }

        private void newEnquiryControl_OnNavigateTo(Interfaces_And_Enums.OrderManagerTab tab)
        {
            switch (tab)
            {
                case Interfaces_And_Enums.OrderManagerTab.MyTasks:
                    myTasks.Reload();
                    viewOrdersTabControl.SelectedIndex = 1;
                    break;
                case Interfaces_And_Enums.OrderManagerTab.AllOrders:
                    allOrdersControl.Reload();
                    viewOrdersTabControl.SelectedIndex = 2;
                    break;
            }
        }

        private void calculatorBtn_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }
    }
}
