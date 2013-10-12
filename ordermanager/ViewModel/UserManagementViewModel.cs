using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel
{
    public class UserManagementViewModel : INotifyPropertyChanged 
    {
        public UserManagementViewModel()
        {

        }

        private User m_NewUser = null;
        public User NewUser
        {
            get
            {
                if (m_NewUser == null)
                {
                    m_NewUser = new User();
                }
                return m_NewUser;
            }
            set
            {
                m_NewUser = value;
                OnPropertyChanged("NewUser");
            }
        }

        private ObservableCollection<User> m_existingUsers = null;
        public ObservableCollection<User> ExistingUsers
        {
            get
            {
                if (m_existingUsers == null)
                {
                    m_existingUsers = new ObservableCollection<User>(DBResources.Instance.Context.Users.ToList());
                }
                return m_existingUsers;
            }
            set
            {
                m_existingUsers = value;
            }
        }

        private User m_SelectedExistingUser = null;
        public User SelectedExistingUser
        {
            get
            {
                return m_SelectedExistingUser;
            }
            set
            {
                m_SelectedExistingUser = value;
                OnPropertyChanged("SelectedExistingUser");
            }
        }

        public void AddNewUserToDataBase()
        {
            try
            {
                NewUser.Validate();
                if (!NewUser.HasErrors)
                {
                    DBResources.Instance.AddNewUser(NewUser);
                    ExistingUsers.Add(NewUser);
                    NewUser = null; //Resetting 
                }
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
