using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ordermanager.Utilities;
namespace ordermanager.DatabaseModel
{
    public partial class User : EntityBase
    {
        #region PropertyWrappers

        public string UserNameWrapper
        {
            get
            {
                return UserName;
            }
            set
            {
                UserName = value;
                Password = value;  //Password is same as user name for default
                ValidateUserNameWrapper();
                OnPropertyChanged("UserNamewrapper");
            }
        }

        public string EmailIDWrapper
        {
            get
            {
                return EmailID;
            }
            set
            {
                EmailID = value;
                ValidateEmailIDWrapper();
                OnPropertyChanged("EmailIDWrapper");
            }
        }

        public UserRole UserRoleWrapper
        {
            get
            {
                return UserRole;
            }
            set
            {
                UserRole = value;
                ValidateUserRoleWrapper();
                OnPropertyChanged("UserRoleWrapper");
            }
        }

        public void ValidateUserRoleWrapper()
        {
            if (UserRole == null)
            {
                AddError("UserRoleWrapper", "Choose a role for the user", false);
            }
            else
            {
                RemoveError("UserRoleWrapper", "Choose a role for the user");
            }
        }


        public void ValidateEmailIDWrapper()
        {
            if (EmailIDWrapper == null || !EmailIDWrapper.IsValidEmailAddress())
            {
                AddError("EmailIDWrapper", "Enter a valid e-mail address", false);
            }
            else
            {
                RemoveError("EmailIDWrapper", "Enter a valid e-mail address");
            }
        }


        public void ValidateUserNameWrapper()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                AddError("UserNameWrapper", "Provide an user name", false);
                return;
            }
            else
            {
                RemoveError("UserNameWrapper", "Provide an user name");
            }

            bool userAlreadyExists = false;
            string lowerCaseUserNameStringForCompare = UserName.ToLower();
            foreach (User user in DBResources.Instance.Context.Users)
            {
                if (user.UserName.ToLower() == lowerCaseUserNameStringForCompare)
                {
                    userAlreadyExists = true;
                    break;
                }
            }
            

            if (userAlreadyExists)
            {
                AddError("UserNameWrapper", "An user with this name has been registered already", false);
            }
            else
            {
                RemoveError("UserNameWrapper", "An user with this name has been registered already");
            }
        }

        public void Validate()
        {
            ValidateUserNameWrapper();
            ValidateEmailIDWrapper();
            ValidateUserRoleWrapper();
        }

        #endregion 
    }
}
