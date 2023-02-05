namespace KeyzCSharpClient;

internal class KeyzClientException : BaseException
{
    public KeyzClientException()
    {
    }

    public KeyzClientException(string error)
    {
        Error = error;
    }
}