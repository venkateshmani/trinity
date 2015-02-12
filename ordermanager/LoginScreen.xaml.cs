using ordermanager.ViewModel;
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
using System.Reflection;
using System.Diagnostics;
using System.Configuration;
using ordermanager.Utilities;
using Reports;

namespace ordermanager
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen
    {
        RegistryHandler registryHandler = null;
        public LoginScreen()
        {
            InitializeComponent();
            this.IsVisibleChanged += LoginScreen_IsVisibleChanged;
            this.Loaded += LoginScreen_Loaded;
            registryHandler = new RegistryHandler("OrderManager");
        }

        void LoginScreen_Loaded(object sender, RoutedEventArgs e)
        {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                versionTxt.Text = "V" + fvi.FileVersion;

                string userName = registryHandler.Read("UserName");
                if (userName != null)
                {
                    tbUserName.Text = userName;
                }

                tbPassword.Password = "v1";
                //LogIn();
        }

        Task dbInitializingTask = null;
        void LoginScreen_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                dbInitializingTask = Task.Factory.StartNew(() => DBResources.Instance.ReInstanceDbContext());
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            dbInitializingTask.Wait();
            LogIn();
        }

        public void Reset()
        {
            tbUserName.Clear();
            tbPassword.Clear();
        }
        public void ClearPassword()
        {            
            tbPassword.Clear();
        }

        private void tbPassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                LogIn();
            }
        }

        private void LogIn()
        {
            try
            {
                string userName = tbUserName.Text.Trim();
                string password = tbPassword.Password.Trim();

                LoginResult res = DBResources.Instance.AuthenticateUser(userName, password);

                if (!res.Authenticated)
                {
                    MessageBox.Show(res.Message, "Authentication Failed", MessageBoxButton.OK, MessageBoxImage.Error);
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

                registryHandler.Write("UserName", userName);
                
                MainWindow mainWindow = new MainWindow(this);
                mainWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
