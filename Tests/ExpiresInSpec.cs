using KeyzClient;
using KeyzClient.Exceptions;
using System.Net.Sockets;

namespace Tests
{
    public class ExpiresInSpec
    {
        private const string IP = "127.0.0.1";
        private const int PORT = 7667;

        [Fact]
        public async Task Should_Have_ExpiresIn_Value_Less_Than_Original()
        {
            // Arrange
            using var keyz = new Keyz(new TcpClient());
            var connection = keyz.Connect(IP, PORT);
            var key = "user:6";
            var value = "testUserSet";
            long expiresInValue = 30;
            
            // Act
            await connection.Set(key, value, 30);
            Thread.Sleep(1000);
            var expiresIn = await connection.ExpiresIn(key);
            

            // Assert
            Assert.NotNull(expiresIn);

            var isValueLessThanOriginal = expiresIn < expiresInValue;
            Assert.True(isValueLessThanOriginal);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task Should_Throw_Correct_Exception_Invalid_Key(string? key)
        {
            using var keyz = new Keyz(new TcpClient());
            var connection = keyz.Connect(IP, PORT);

            await Assert.ThrowsAsync<KeyzClientExpiresInException>(async () => await connection.ExpiresIn(key));
        }
    }
}
