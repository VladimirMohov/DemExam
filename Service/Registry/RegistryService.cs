using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemExam.Service.Registry
{
    public class RegistryService
    {
        private const string subkey = "SOFTWARE\\DemExam";

        public RegistryService()
        {
            if (Microsoft.Win32.Registry.CurrentUser.OpenSubKey(subkey) == null)
            {
                Microsoft.Win32.Registry.CurrentUser.CreateSubKey(subkey);
            }
        }

        public string? GetRegistryData(string dataName = "userToken")
        {
            Microsoft.Win32.RegistryKey rootLevel = Microsoft.Win32.Registry.CurrentUser;

            try
            {
                return rootLevel.OpenSubKey(subkey).GetValue(dataName).ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SetRegistryData(string value, string dataName = "userToken")
        {
            Microsoft.Win32.RegistryKey rootLevel = Microsoft.Win32.Registry.CurrentUser;

            using (var key = rootLevel.OpenSubKey(subkey, writable: true))
            {
                if (key != null)
                {
                    key.SetValue(dataName, value);
                    key.Close();
                    return true;
                }
            }

            return true;
        }

        public void ClearRegistryData(string dataName = "userToken", int level = 1)
        {
            Microsoft.Win32.RegistryKey rootLevel = Microsoft.Win32.Registry.CurrentUser;

            using (var key = rootLevel.OpenSubKey(subkey, writable: true))
            {
                if (key != null)
                {
                    key.DeleteValue(dataName);
                    key.Close();
                }
            }
        }
    }
}
