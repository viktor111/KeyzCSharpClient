namespace KeyzClient.Exceptions
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
