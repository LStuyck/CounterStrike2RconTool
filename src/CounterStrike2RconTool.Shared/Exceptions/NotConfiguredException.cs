using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterStrike2RconTool.Shared.Exceptions
{
    public class NotConfiguredException : Exception
    {
        public NotConfiguredException(string message) : base(message) { }
    }
}
