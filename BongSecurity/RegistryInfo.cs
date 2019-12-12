using System;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Data;

namespace BongSecurity
{
    public class RegistryInfo
    {
        public static bool findLinkRegistry()
        {
            try
            {
                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey("Directory\\shell\\BongSecurity"))
                {
                    if (key != null)
                    {
                        key.Close();
                        return true;
                    }
                }
            }

            catch (Exception ex)  //just for demonstration...it's always best to handle specific exceptions
            {
                //react appropriately
                Program.AddLog("Error : " + ex.Message.ToString());
            }
            return false;
        }

        public static string addLinkRegistry()
        {
            if (!findLinkRegistry())
            {
                try
                {
                    using (RegistryKey key = Registry.ClassesRoot.CreateSubKey("Directory\\shell\\BongSecurity"))
                    {
                        if (key != null)
                        {
                            key.SetValue("Icon", "\"" + Application.ExecutablePath + "\", 1");
                            key.Close();
                            using (RegistryKey cmdkey = Registry.ClassesRoot.CreateSubKey("Directory\\shell\\BongSecurity\\command"))
                            {
                                if (cmdkey != null)
                                {
                                    cmdkey.SetValue("", "\""+ Application.ExecutablePath +"\"" +"%1");
                                    cmdkey.Close();
                                }
                            }
                        }
                    }
                }

                catch (Exception ex)  //just for demonstration...it's always best to handle specific exceptions
                {
                    //react appropriately
                    Program.AddLog("Error : " + ex.Message.ToString());
                    return ex.Message.ToString();
                }
            }
            return "";
        }

        public static string deleteLinkRegistry()
        {
            if (findLinkRegistry())
            {
                try
                {
                    Registry.ClassesRoot.DeleteSubKeyTree("Directory\\shell\\BongSecurity");
                }

                catch (Exception ex)  //just for demonstration...it's always best to handle specific exceptions
                {
                    //react appropriately
                    Program.AddLog("Error : " + ex.Message.ToString());
                    return ex.Message.ToString();
                }
            }
            return "";
        }
    }
}
