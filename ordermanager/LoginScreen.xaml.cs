﻿using ordermanager.ViewModel;
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
using ordermanager.Views;
namespace ordermanager
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen
    {
        public LoginScreen()
        {
            InitializeComponent();           
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string userName = tbUserName.Text.Trim();
            string password = tbPassword.Password.Trim();
            
            LoginResult res = DBResources.Instance.AuthenticateUser(userName, password);
            
            if (!res.Authenticated)
            {
                MessageBox.Show(res.Message,"Authentication Failed",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            if (res.NeedPasswordReset)
            {
                MessageBox.Show("Reset your password!!!", "Change Password", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Hide();
                PasswordChangeDialog dialog = new PasswordChangeDialog();
                dialog.ShowDialog();
                if (dialog.DialogResult != true)
                    Application.Current.Shutdown();                
            }
            
            MainWindow mainWindow = new MainWindow(this);
            mainWindow.ShowDialog();
        }   
    }
}
