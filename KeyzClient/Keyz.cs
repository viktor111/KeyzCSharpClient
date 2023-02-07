using KeyzClient.Exceptions;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace KeyzClient
{
    public class Keyz : IKeyz, IDisposable
    {
        private TcpClient _tcpClient;
        
        public Keyz(string ip, int port)
        {
            ValidateIp(ip);
            ValidatePort(port);

            _tcpClient = new TcpClient(ip, port);
        }

        public IKeyz Connect(string ip, int port)
        {
            ValidateIp(ip);
            ValidatePort(port);

            _tcpClient = new TcpClient(ip, port);
            return this;
        }

        public async Task<string?> Get(string key)
        {
            ValidateKey<KeyzClientGetException>(key);

            var stream = _tcpClient.GetStream();

            var message = $"{CommandsConstants.GET} {key}";

            await TcpHelper.WriteMessage(message, stream);

            var response = await TcpHelper.ReadMessage(stream);

            if (response.Equals("null"))
                return null;

            return response;
        }

        public async Task<bool> Set(string key, string value, long? expireAt = null)
        {
            ValidateKey<KeyzClientSetException>(key);

            var stream = _tcpClient.GetStream();

            var message = $"{CommandsConstants.SET} {key} {value}";

            if (expireAt != null)
            {
                message += $" {CommandsConstants.EX} {expireAt}";
            }

            await TcpHelper.WriteMessage(message, stream);

            var response = await TcpHelper.ReadMessage(stream);

            if (response.Equals("ok"))
                return true;

            return false;
        }


        public async Task<string?> Delete(string key)
        {
            ValidateKey<KeyzClientDeleteException>(key);

            var stream = _tcpClient.GetStream();

            var message = $"{CommandsConstants.DEL} {key}";

            await TcpHelper.WriteMessage(message, stream);

            var response = await TcpHelper.ReadMessage(stream);

            if (response.Equals("null"))
                return null;

            return response;
        }

        public async Task<long?> ExpiresIn(string key)
        {
            ValidateKey<KeyzClientExpiresInException>(key);

            var stream = _tcpClient.GetStream();
            var message = $"{CommandsConstants.EXIN} {key}";

            await TcpHelper.WriteMessage(message, stream);

            var response = await TcpHelper.ReadMessage(stream);

            if (response.Equals("null"))
                return null;

            return long.Parse(response);
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

        private void ValidateKey<TException>(string key)
            where TException : BaseException, new()
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                var exception = new TException { Error = "Key cannot be null, empty or white space" };
                throw exception;
            }
        }

        private void ValidateIp(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip)) throw new KeyzClientConnectionException("Invalid IP. Cannot be null, empty or white space");
        }

        private void ValidatePort(int port)
        {
            if (port < 1) throw new KeyzClientConnectionException("Port cannot be below 1");
            if (port > 65535) throw new KeyzClientConnectionException("Port cannot be above 65535");
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
}