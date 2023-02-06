namespace KeyzCSharpClient.Exceptions;

public class KeyzClientException : BaseException
{
    public KeyzClientException()
    {
    }

    public KeyzClientException(string error)
    {
        Error = error;
    }
}