using System.Threading.Tasks;

namespace SignalRStocks.Contract.Group
{
    public interface IGroupClient
    {
        Task ReceiveMessage(string groupName, string message);
    }
}
