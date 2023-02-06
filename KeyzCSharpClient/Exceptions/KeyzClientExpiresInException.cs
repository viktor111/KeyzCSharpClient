using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyzCSharpClient.Exceptions
{
    public class KeyzClientExpiresInException : BaseException
    {
        public KeyzClientExpiresInException()
        {
        }

        public KeyzClientExpiresInException(string error)
        {
            Error = error;
        }
    }
}
