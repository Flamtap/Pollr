using System.Threading.Tasks;

namespace Pollr.Server.Hubs
{
    public interface ICountHub
    {
        Task UserJoinedAsync(string username);

        Task UpdateCountAsync(int count);
    }
}
