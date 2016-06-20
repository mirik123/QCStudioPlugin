using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuantConnect.RestAPI.Models
{
    public abstract class ChartControl : UserControl
    {
        public Action<string> Logger;
        public virtual void Initialize(params string[] args) { }
        public virtual void Run(PacketBacktestResult _results) { }
    }
}
