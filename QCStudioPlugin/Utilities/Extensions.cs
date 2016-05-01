/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

/**********************************************************
* USING NAMESPACES
**********************************************************/
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuantConnect.QCPlugin
{
    /// <summary>
    /// Common Static Class for Program Extensions:
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Cross thread invokation
        /// </summary>
        public static TResult SafeInvoke<T, TResult>(this T isi, Func<T, TResult> call) where T : ISynchronizeInvoke
        {
            if (isi.InvokeRequired)
            {
                IAsyncResult result = isi.BeginInvoke(call, new object[] { isi });
                object endResult = isi.EndInvoke(result); return (TResult)endResult;
            }
            else
                return call(isi);
        }

        /// <summary>
        /// Cross thread invokation
        /// </summary>
        public static void SafeInvoke<T>(this T isi, Action<T> call) where T : ISynchronizeInvoke
        {
            if (isi.InvokeRequired) isi.BeginInvoke(call, new object[] { isi });
            else
                call(isi);
        }

        /// <summary>
        /// Convert a date time object to unix:
        /// </summary>
        public static double DateTimeToUnixTimestamp(this DateTime dateTime)
        {
            return (dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
        }

        public static async Task<DialogResult> ShowDialogAsync(this Form @this)
        {
            await Task.Yield();
            if (@this.IsDisposed) return DialogResult.OK;

            return @this.ShowDialog();
        } 
    }
}
