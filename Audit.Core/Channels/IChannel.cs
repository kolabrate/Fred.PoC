using System.Threading.Tasks;

namespace Audit.Core.Channels
{
    public interface IChannel
    {
        Task<bool> WriteAsync(Models.Audit data);

    }
}
