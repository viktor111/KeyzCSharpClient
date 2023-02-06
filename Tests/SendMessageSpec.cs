using System.Net.Sockets;
using KeyzClient;

namespace Tests;

public class SendMessageSpec
{
    private const string IP = "127.0.0.1";
    private const int PORT = 7667;
    
    [Fact]
    public async Task Should_Set_Value()
    {
        // Arrange
        using var keyz = new Keyz(new TcpClient());
        var connection = keyz.Connect(IP, PORT);
        var key = "user:1";
        var value = "testUserSet";
        
        // Act
        await connection.SendMessage($"SET {key} {value}");
        var result = await connection.SendMessage($"GET {key}");

        // Assert
        Assert.Equal(result, value);
    }
    
    [Fact]
    public async Task Should_Set_Value_Expire_4_Seconds()
    {
        // Arrange
        using var keyz = new Keyz(new TcpClient());
        var connection = keyz.Connect(IP, PORT);
        var key = "user:1";
        var value = "testUserSet";
        
        // Act
        await connection.SendMessage($"SET {key} {value} EX 4");
        Thread.Sleep(5000);
        var result = await connection.SendMessage($"GET {key}");

        // Assert
        Assert.Null(result);
    }
}