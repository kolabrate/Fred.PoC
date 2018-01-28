using System.Collections.Generic;
using System.Threading.Tasks;

namespace Audit.Core.Channels
{
    public interface IChannel
    {
       Task WriteAsync<T>(T data);
        Task WriteAsync<T>(IEnumerable<T> data);

    }
}
