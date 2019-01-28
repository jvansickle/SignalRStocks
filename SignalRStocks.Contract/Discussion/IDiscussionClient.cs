using System.Threading.Tasks;

namespace SignalRStocks.Contract.Discussion
{
    public interface IDiscussionClient
    {
        Task ReceiveMessage(string message);
    }
}
