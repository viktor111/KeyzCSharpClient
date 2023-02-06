using KeyzClient;
using KeyzClient.Exceptions;
using System.Net.Sockets;

namespace Tests
{
    public class SetSpec
    {
        private const string IP = "127.0.0.1";
        private const int PORT = 7667;

        [Fact]
        public async Task Should_Set_Value()
        {
            // Arrange
            using var keyz = new Keyz(new TcpClient());
            var connection = keyz.Connect(IP, PORT);
            var key = "user:2";
            var value = "testUserSet";

            // Act
            var set = await connection.Set(key, value);
            var result = await connection.Get(key);

            // Assert
            Assert.Equal(result, value);
            Assert.True(set);
        }

        [Fact]
        public async Task Should_Set_Value_Expire_4_Seconds()
        {
            // Arrange
            using var keyz = new Keyz(new TcpClient());
            var connection = keyz.Connect(IP, PORT);
            var key = "user:2";
            var value = "testUserSet";

            // Act
            var set = await connection.Set(key, value, 4);
            Thread.Sleep(5000);
            var result = await connection.Get(key);

            // Assert
            Assert.Null(result);
            Assert.True(set);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task Should_Throw_Correct_Exception_Invalid_Key(string? key)
        {
            using var keyz = new Keyz(new TcpClient());
            var connection = keyz.Connect(IP, PORT);
            var value = "testUserSet";

            await Assert.ThrowsAsync<KeyzClientSetException>(async () => await connection.Set(key, value));
        }
    }
}
