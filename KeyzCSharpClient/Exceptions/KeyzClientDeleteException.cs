using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KeyzCSharpClient.Exceptions
{
    public class KeyzClientDeleteException : BaseException
    {
        public KeyzClientDeleteException()
        {
        }

        public KeyzClientDeleteException(string error)
        {
            Error = error;
        }
    }
}
