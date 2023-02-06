namespace KeyzClient.Exceptions
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
