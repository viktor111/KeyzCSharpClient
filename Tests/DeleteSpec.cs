using KeyzClient;
using KeyzClient.Exceptions;
using System.Net.Sockets;

namespace Tests
{
    public class DeleteSpec
    {
        private const string IP = "127.0.0.1";
        private const int PORT = 7667;

        [Fact]
        public async Task Should_Delete_Key()
        {
            // Arrange
            using var keyz = new Keyz(new TcpClient());
            var connection = keyz.Connect(IP, PORT);
            var key = "user:5";
            var value = "testUserSet";

            // Act
            await connection.Set(key, value);
            var deletedKey = await connection.Delete(key);
            var getResult = await connection.Get(key);

            // Assert
            Assert.Equal(key, deletedKey);
            Assert.Null(getResult);
        }

        [Fact]
        public async Task Should_Be_Null_Key_Expired()
        {
            // Arrange
            using var keyz = new Keyz(new TcpClient());
            var connection = keyz.Connect(IP, PORT);
            var key = "user:5";
            var value = "testUserSet";

            // Act
            await connection.Set(key, value, 4);
            Thread.Sleep(5000);
            var deletedKey = await connection.Delete(key);

            // Assert
            Assert.Null(deletedKey);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task Should_Throw_Correct_Exception_Invalid_Key(string? key)
        {
            using var keyz = new Keyz(new TcpClient());
            var connection = keyz.Connect(IP, PORT);

            await Assert.ThrowsAsync<KeyzClientDeleteException>(async () => await connection.Delete(key));
        }
    }
}
