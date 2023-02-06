using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyzCSharpClient.Exceptions
{
    public class KeyzClientSetException : BaseException
    {
        public KeyzClientSetException()
        {
        }

        public KeyzClientSetException(string error)
        {
            Error = error;
        }
    }
}
