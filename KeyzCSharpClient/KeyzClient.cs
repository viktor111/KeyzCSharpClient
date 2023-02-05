using System.Net.Sockets;

namespace KeyzCSharpClient;

public class KeyzClient : IKeyzClient, IDisposable
{
    private TcpClient _tcpClient;


    public KeyzClient(TcpClient tcpClient)
    {
        _tcpClient = tcpClient;
    }

    public IKeyzClient Connect(string ip, int port)
    {
        _tcpClient = new TcpClient(ip, port);
        return this;
    }

    public async Task<string?> SendMessage(string message)
    {
        var stream = _tcpClient.GetStream();

        await TcpHelper.WriteMessage(message, stream);

        var response = await TcpHelper.ReadMessage(stream);

        if (response.Equals("null"))
            return null;

        return response;
    }

    private async ValueTask DisposeAsync()
    {
        var stream = _tcpClient.GetStream();

        await TcpHelper.WriteMessage(CommandsConstants.CLOSE, stream);
        
        var response = await TcpHelper.ReadMessage(stream);
        
        stream.Close();
        _tcpClient.Dispose();
    }

    public void Dispose()
    {
        DisposeAsync().GetAwaiter().GetResult();
    }
}