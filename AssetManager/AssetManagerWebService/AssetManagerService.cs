using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AssetManagerWebService.DatabaseModel;

namespace AssetManagerWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class AssetManagerService : IAssetManagerService
    {

        public LoginResult Authenticate(string userName, string password)
        {
            AssetManagerDBEntities dbContext = new AssetManagerDBEntities();
            List<UserName> users = dbContext.UserNames.ToList();
            if (users != null && users.Count > 0)
            {
                UserName user = dbContext.UserNames.Where(u => u.Name == userName).Select(u => u)
                                                         .FirstOrDefault();
                if (user != null)
                {
                    string passwordValueInDatabase = user.Password;
                    if (user.Password == password)
                        return new LoginResult() { Authenticated = true, NeedPasswordReset = (passwordValueInDatabase == user.Name) };
                    else
                        return new LoginResult() { Authenticated = false, Message = "Authentication failed." };
                }
                return new LoginResult() { Authenticated = false, Message = "User not found" };
            }
            return new LoginResult() { Authenticated = false, Message = "User not found" };
        }
    }
}
