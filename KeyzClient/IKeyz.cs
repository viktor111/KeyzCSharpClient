using System.Net;
using System.Threading.Tasks;

namespace KeyzClient
{
    public interface IKeyz
    {
        public IKeyz Connect(string ip, int port);

        public Task<string?> Get(string key);

        public Task<bool> Set(string key, string value, long? expireAt = null);

        public Task<string?> Delete(string key);

        public Task<long?> ExpiresIn(string key);

        public Task<string?> SendMessage(string message);
    }
}