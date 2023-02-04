using System.Net.Sockets;
using System.Text;

namespace KeyzCSharpClient;

public class TcpHelper
{
    internal async Task WriteMessage(string message, NetworkStream stream)
    {
        var messageLen = message.Length;
        var messageLenBytes = IntToBeBytes(messageLen);
        var messageBytes = Encoding.UTF8.GetBytes(message);

        await stream.WriteAsync(messageLenBytes);
        await stream.WriteAsync(messageBytes);
    }
    
    internal async Task<string> ReadMessage(NetworkStream stream)
    {
        var messageBytesLen = new byte[4];
        var bytesReadMessageLen = await stream.ReadAsync(messageBytesLen);
        
        var lenOfMessage = BitConverter.ToInt32(messageBytesLen);

        var messageBytes = new byte[lenOfMessage];

        var bytesReadMessage = await stream.ReadAsync(messageBytes);

        var message = Encoding.UTF8.GetString(messageBytes);

        return message;
    }
    
    private static byte[] IntToBeBytes(int number)
    {
        var bytes = BitConverter.GetBytes(number);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }

        return bytes;
    }
}