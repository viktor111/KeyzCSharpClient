namespace KeyzCSharpClient.Exceptions;

public abstract class BaseException : Exception
{
    private string? error;

    internal string Error
    {
        get => error ?? base.Message;
        set => error = value;
    }
}