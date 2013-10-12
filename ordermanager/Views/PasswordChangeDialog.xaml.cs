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

namespace ordermanager.Views
{
    /// <summary>
    /// Interaction logic for PasswordChangeDialog.xaml
    /// </summary>
    public partial class PasswordChangeDialog 
    {
        public PasswordChangeDialog()
        {
            InitializeComponent();
        }
        private void btnResetPassword_Click(object sender, RoutedEventArgs e)
        {
            if (DBResources.Instance.ChangePassword(tbPassword.Password.Trim()))
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to change your password");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
