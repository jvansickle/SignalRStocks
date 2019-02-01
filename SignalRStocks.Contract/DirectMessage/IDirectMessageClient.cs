using System.Threading.Tasks;

namespace SignalRStocks.Contract.DirectMessage
{
    public interface IDirectMessageClient
    {
        Task ReceiveMessage(DirectMessage directMessage);
    }
}
