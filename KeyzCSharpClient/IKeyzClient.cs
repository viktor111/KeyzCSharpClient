using System.Net;

namespace KeyzCSharpClient;

public interface IKeyzClient
{
    public IKeyzClient Connect(string ip, int port);
    public Task<string?> SendMessage(string message);
}