namespace KeyzCSharpClient;

internal abstract class BaseException : Exception
{
    private string? error;

    internal string Error
    {
        get => this.error ?? base.Message;
        set => this.error = value;
    }
}