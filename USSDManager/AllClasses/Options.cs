using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.IsolatedStorage;

namespace USSDManager.AllClasses
{
    enum nTypes
    {
        PhoneType,
        SummaType,
        NumberType,
        NumberType4,
        NumberType14,
        All
    }

    class Options
    {

        public static string Operator
        {
            get
            {
                try
                {
                    var options = IsolatedStorageSettings.ApplicationSettings;
                    if (options.Contains("Operator"))
                        return (string)options["Operator"];
                    else
                        return String.Empty;
                }
                catch
                {
                    return String.Empty;
                }
            }
            set
            {
                try
                {
                    var options = IsolatedStorageSettings.ApplicationSettings;
                    if (!options.Contains("Operator"))
                    {
                        options.Add("Operator", value);
                        options.Save();
                        return;
                    }
                    options["Operator"] = value;
                    options.Save();
                }
                catch { }
            }
        }

        public static bool phonenumberhelp
        {
            get
            {
                try
                {
                    var options = IsolatedStorageSettings.ApplicationSettings;
                    if (options.Contains("phonenumberhelp"))
                        return (bool)options["phonenumberhelp"];
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    var options = IsolatedStorageSettings.ApplicationSettings;
                    if (!options.Contains("phonenumberhelp"))
                    {
                        options.Add("phonenumberhelp", value);
                        options.Save();
                        return;
                    }
                    options["phonenumberhelp"] = value;
                    options.Save();
                }
                catch { }
            }
        }

        public static String phnumber
        {
            get
            {
                try
                {
                    var options = IsolatedStorageSettings.ApplicationSettings;
                    if (options.Contains("phnumber"))
                        return (String)options["phnumber"];
                    else
                        return String.Empty;
                }
                catch
                {
                    return String.Empty;
                }
            }
            set
            {
                try
                {
                    var options = IsolatedStorageSettings.ApplicationSettings;
                    if (!options.Contains("phnumber"))
                    {
                        options.Add("phnumber", value);
                        options.Save();
                        return;
                    }
                    options["phnumber"] = value;
                    options.Save();
                }
                catch { }
            }
        }

        public static String shablon
        {
            get
            {
                try
                {
                    var options = IsolatedStorageSettings.ApplicationSettings;
                    if (options.Contains("shablon"))
                        return (String)options["shablon"];
                    else
                        return String.Empty;
                }
                catch
                {
                    return String.Empty;
                }
            }
            set
            {
                try
                {
                    var options = IsolatedStorageSettings.ApplicationSettings;
                    if (!options.Contains("shablon"))
                    {
                        options.Add("shablon", value);
                        options.Save();
                        return;
                    }
                    options["shablon"] = value;
                    options.Save();
                }
                catch { }
            }
        }        

    }
}
