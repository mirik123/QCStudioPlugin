using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantConnect.QCPlugin
{
    public enum APIErrors
    { 
        None,
        NotLoggedIn,
        ProjectIdNotFound,
        CompileTimeout,
        CompileError
    }
}
