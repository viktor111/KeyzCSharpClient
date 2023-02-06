namespace KeyzClient.Exceptions
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
