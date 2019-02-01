using System;
using System.Threading.Tasks;

namespace SignalRStocks.Contract.DirectMessage
{
    public interface IDirectMessageHub
    {
        Task SendDirectMessage(DirectMessageRequest request);
    }
}
