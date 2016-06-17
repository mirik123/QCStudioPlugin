/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/
using System;
using System.IO;
using System.Security;
using Microsoft.Win32;
using System.Windows.Forms;
using QuantConnect.QCStudioPlugin.Properties;
using QuantConnect.QCStudioPlugin;

namespace QuantConnect.Views
{
    public enum BrowserEmulationVersion
    {
        Error = -1,
        Default = 0,
        Version7 = 7000,
        Version8 = 8000,
        Version8Standards = 8888,
        Version9 = 9000,
        Version9Standards = 9999,
        Version10 = 10000,
        Version10Standards = 10001,
        Version11 = 11000,
        Version11Edge = 11001
    }

    /// <summary>
    /// Helper class for setting registry configuration for the web browser control
    /// </summary>
    public static class WBEmulator
    {
        private const string InternetExplorerRootKey = @"Software\Microsoft\Internet Explorer";
        private const string BrowserEmulationKey = InternetExplorerRootKey + @"\Main\FeatureControl\FEATURE_BROWSER_EMULATION";

        /// <summary>
        /// Get the version of internet explorer from the registry
        /// </summary>
        /// <returns>int version of IE.</returns>
        public static int GetInternetExplorerMajorVersion()
        {
            var result = 0;
            try
            {
                var key = Registry.LocalMachine.OpenSubKey(InternetExplorerRootKey);

                if (key != null)
                {
                    var value = key.GetValue("svcVersion", null) ?? key.GetValue("Version", null);

                    if (value != null)
                    {
                        var version = value.ToString();
                        var separator = version.IndexOf('.');
                        if (separator != -1)
                        {
                            int.TryParse(version.Substring(0, separator), out result);
                        }
                    }
                }
            }
            catch (SecurityException ex)
            {
                QCPluginUtilities.OutputCommandString("The user does not have the permissions required to read from the registry key: " + ex.ToString(), QCPluginUtilities.Severity.Error);
                return -1;
            }
            catch (UnauthorizedAccessException ex)
            {
                QCPluginUtilities.OutputCommandString("The user does not have the necessary registry rights: " + ex.ToString(), QCPluginUtilities.Severity.Error);
                return -1;
            }

            return result;
        }

        public static BrowserEmulationVersion GetBrowserEmulationVersion()
        {
            var result = BrowserEmulationVersion.Default;
            try
            {
                var key = Registry.CurrentUser.OpenSubKey(BrowserEmulationKey, true);
                if (key != null)
                {
                    var programName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
                    var value = key.GetValue(programName, null);

                    if (value != null)
                    {
                        result = (BrowserEmulationVersion)Convert.ToInt32(value);
                    }
                }
            }
            catch (SecurityException ex)
            {
                QCPluginUtilities.OutputCommandString("The user does not have the permissions required to read from the registry key: " + ex.ToString(), QCPluginUtilities.Severity.Error);
                result = BrowserEmulationVersion.Error;
            }
            catch (UnauthorizedAccessException ex)
            {
                QCPluginUtilities.OutputCommandString("The user does not have the necessary registry rights: " + ex.ToString(), QCPluginUtilities.Severity.Error);
                result = BrowserEmulationVersion.Error;
            }

            return result;
        }

        /// <summary>
        /// Set the browser's IE version in registry
        /// </summary>
        /// <param name="browserEmulationVersion"></param>
        /// <returns></returns>
        public static bool SetBrowserEmulationVersion(BrowserEmulationVersion browserEmulationVersion)
        {
            
            try
            {
                var result = false;
                var key = Registry.CurrentUser.OpenSubKey(BrowserEmulationKey, true);
                if (key != null)
                {
                    var programName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
                    if (browserEmulationVersion != BrowserEmulationVersion.Default)
                    {
                        // if it's a valid value, update or create the value
                        key.SetValue(programName, (int)browserEmulationVersion, RegistryValueKind.DWord);
                    }
                    else
                    {
                        // otherwise, remove the existing value
                        key.DeleteValue(programName, false);
                    }

                    result = true;
                }

                return result;
            }
            catch (SecurityException ex)
            {
                QCPluginUtilities.OutputCommandString("The user does not have the permissions required to read from the registry key: " + ex.ToString(), QCPluginUtilities.Severity.Error);
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                QCPluginUtilities.OutputCommandString("The user does not have the necessary registry rights: " + ex.ToString(), QCPluginUtilities.Severity.Error);
                return false;
            }
        }

        public static BrowserEmulationVersion GetBrowserEmulationForInternetExplorer(int ieVersion)
        {
            BrowserEmulationVersion emulationCode;
            if (ieVersion >= 11)
            {
                emulationCode = BrowserEmulationVersion.Version11;
            }
            else if (ieVersion < 0)
            {
                emulationCode = BrowserEmulationVersion.Error;
            }
            else
            {
                switch (ieVersion)
                {
                    case 10:
                        emulationCode = BrowserEmulationVersion.Version10;
                        break;
                    case 9:
                        emulationCode = BrowserEmulationVersion.Version9;
                        break;
                    case 8:
                        emulationCode = BrowserEmulationVersion.Version8;
                        break;
                    default:
                        emulationCode = BrowserEmulationVersion.Version7;
                        break;
                }
            }

            return emulationCode;
        }

        public static void ValidateAndUpdateBrowserEmulation()
        {
            var ieVersion = GetInternetExplorerMajorVersion();
            var emulationIECode = GetBrowserEmulationForInternetExplorer(ieVersion);
            if (ieVersion < 0) return;

            var emulationRegCode = GetBrowserEmulationVersion();
            if (emulationRegCode == BrowserEmulationVersion.Error) return;

            string msg = "";

            if (emulationRegCode == BrowserEmulationVersion.Default)
                msg = "No Visual Studio Browser emulation is set in the registry. Do you want to set it to the latest Internet Explorer version: " + ieVersion + 
                        "? The results will be enabled after restarting VisualStudio.";
            else if (emulationRegCode != emulationIECode)
                msg = "The Visual Studio Browser emulation is set to the version: " + emulationRegCode.ToString().Replace("Version", "") +
                        ". Do you want to set it to the latest Internet Explorer version: " + ieVersion +
                        "? The results will be enabled after restarting VisualStudio.";
            else return;

            if (DialogResult.Yes == MessageBox.Show(msg, Resources.ToolWindowTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                SetBrowserEmulationVersion(emulationIECode);
        }

        /// <summary>
        /// Global Flag :: Operating System
        /// </summary>
        public static bool IsLinux
        {
            get
            {
                var p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }

        /// <summary>
        /// Global Flag :: Operating System
        /// </summary>
        public static bool IsWindows
        {
            get
            {
                return !IsLinux;
            }
        }
    }
}
