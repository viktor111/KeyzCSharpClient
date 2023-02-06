using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyzCSharpClient.Exceptions
{
    public class KeyzClientConnectionException : BaseException
    {
        public KeyzClientConnectionException()
        {
        }

        public KeyzClientConnectionException(string error)
        {
            Error = error;
        }
    }
}
