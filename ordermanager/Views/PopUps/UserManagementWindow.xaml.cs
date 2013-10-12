using ordermanager.DatabaseModel;
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

namespace ordermanager.Views.PopUps
{
    /// <summary>
    /// Interaction logic for AddNewUserWindow.xaml
    /// </summary>
    public partial class UserManagementWindow
    {
        public UserManagementWindow()
        {
            InitializeComponent();
            this.Loaded += UserManagementWindow_Loaded;
        }

        private UserManagementViewModel userManagementViewModel = null;
        void UserManagementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            userManagementViewModel = new UserManagementViewModel();
            this.DataContext = userManagementViewModel;
        }

        private void existingUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userManagementViewModel.SelectedExistingUser = existingUsersList.SelectedItem as User;
        }

        private void addNewuserBtn_Click(object sender, RoutedEventArgs e)
        {
            userManagementViewModel.AddNewUserToDataBase();
        }

        private void clearFieldsBtn_Click(object sender, RoutedEventArgs e)
        {
            userManagementViewModel.NewUser = new DatabaseModel.User();
        }

        private void SaveChangesToExistingUserBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetPasswordBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteUser_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
