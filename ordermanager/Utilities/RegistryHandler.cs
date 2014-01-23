using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.Utilities
{
    public class RegistryHandler
    {
        public string ProductName { get; set; }
        public RegistryHandler(string productName)
        {
            ProductName = productName;
        }

        public string Read(string KeyName)
        {
            // Opening the registry key
            RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software");
            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(ProductName);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return null;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    return (string)sk1.GetValue(KeyName.ToUpper());
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    return null;
                }
            }
        }

        public bool Write(string KeyName, object Value)
        {
            try
            {
                // Setting
                RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);
                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                RegistryKey sk1 = rk.CreateSubKey(ProductName, RegistryKeyPermissionCheck.ReadWriteSubTree);
                // Save the value
                sk1.SetValue(KeyName.ToUpper(), Value);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                return false;
            }
        }
    }
}
