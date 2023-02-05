using System.Net.Sockets;
using KeyzCSharpClient;

namespace Tests;

public class SendMessageSpec
{
    private const string IP = "127.0.0.1";
    private const int PORT = 7667;
    
    [Fact]
    public async Task Should_Set_Value()
    {
        // Arrange
        using var keyz = new KeyzClient(new TcpClient());
        var connection = keyz.Connect(IP, PORT);
        var value = "testUserSet";
        
        // Act
        await connection.SendMessage($"SET user:1 {value}");
        var result = await connection.SendMessage("GET user:1");

        // Assert
        Assert.Equal(result, value);
    }
    
    [Fact]
    public async Task Should_Set_Value_Expire_4_Seconds()
    {
        // Arrange
        using var keyz = new KeyzClient(new TcpClient());
        var connection = keyz.Connect(IP, PORT);
        var value = "testUserSet";
        
        // Act
        await connection.SendMessage($"SET user:1 {value} EX 4");
        Thread.Sleep(5000);
        var result = await connection.SendMessage("GET user:1");

        // Assert
        Assert.Equal(result, null);
    }
}