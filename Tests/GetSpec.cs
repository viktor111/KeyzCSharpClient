using KeyzClient;
using KeyzClient.Exceptions;
using System.Net.Sockets;

namespace Tests
{
    public class GetSpec
    {
        private const string IP = "127.0.0.1";
        private const int PORT = 7667;

        [Fact]
        public async Task Should_Get_Value()
        {
            // Arrange
            using var keyz = new Keyz(new TcpClient());
            var connection = keyz.Connect(IP, PORT);
            var key = "user:4";
            var value = "testUserSet";

            // Act
            await connection.Set(key, value);
            var getResult = await connection.Get(key);

            // Assert
            Assert.Equal(getResult, value);
        }

        [Fact]
        public async Task Should_Be_Null_Because_Expired()
        {
            // Arrange
            using var keyz = new Keyz(new TcpClient());
            var connection = keyz.Connect(IP, PORT);
            var key = "user:5";
            var value = "testUserSet";

            // Act
            await connection.Set(key, value, 4);
            Thread.Sleep(5000);
            var getResult = await connection.Get(key);

            // Assert
            Assert.Null(getResult);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task Should_Throw_Correct_Exception_Invalid_Key(string? key)
        {
            using var keyz = new Keyz(new TcpClient());
            var connection = keyz.Connect(IP, PORT);

            await Assert.ThrowsAsync<KeyzClientGetException>(async () => await connection.Get(key));
        }
    }
}
