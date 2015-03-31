using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartcoin.Entities
{
    public class SmartcoinException : Exception
    {
        public int Code { get; private set; }

        public SmartcoinException(int code)
            : this(code, null)
        {

        }

        public SmartcoinException(int code, string message)
            : base(message)
        {
            Code = code;
        }

        public override string ToString()
        {
            return this.Message + "\n" + this.StackTrace;
        }
    }
}
