namespace KeyzClient.Exceptions
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
