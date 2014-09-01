/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

/**********************************************************
* USING NAMESPACES
**********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantConnect.QCPlugin
{
    /******************************************************** 
    * GLOBAL DEFINITIONS
    *********************************************************/
    /// <summary>
    /// List of the Common API Errors:
    /// </summary>
    public enum APIErrors
    { 
        None,
        NotLoggedIn,
        ProjectIdNotFound,
        CompileTimeout,
        CompileError
    }
}
