using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyzCSharpClient.Exceptions
{
    public class KeyzClientGetException : BaseException
    {
        public KeyzClientGetException()
        {
        }

        public KeyzClientGetException(string error)
        {
            Error = error;
        }
    }
}
