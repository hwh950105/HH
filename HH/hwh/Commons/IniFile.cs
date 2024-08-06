using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HH.Commons
{
    public class IniFile
    {
        private string filePath;

        public IniFile(string path)
        {
            filePath = path;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetPrivateProfileString(
            string section, string key, string defaultValue,
            StringBuilder returnedString, int size, string filePath);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int WritePrivateProfileString(
            string section, string key, string value, string filePath);

        public string Read(string section, string key, string defaultValue = "")
        {
            var returnedString = new StringBuilder(255);
            int bytesRead = GetPrivateProfileString(section, key, defaultValue, returnedString, returnedString.Capacity, filePath);

            if (bytesRead == 0 && !string.IsNullOrEmpty(defaultValue))
            {
                return defaultValue;
            }

            return returnedString.ToString();
        }

        public void Write(string section, string key, string value)
        {
            int result = WritePrivateProfileString(section, key, value, filePath);
            if (result == 0)
            {
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            }
        }
    }
}
