namespace KeyzClient.Exceptions
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
