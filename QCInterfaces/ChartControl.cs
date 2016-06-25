using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QCInterfaces
{
    public abstract class ChartControl : UserControl
    {
        public Action<string> Logger;
        public virtual void Initialize(params string[] args) { }
        public virtual void Run(string json) { }
    }
}
