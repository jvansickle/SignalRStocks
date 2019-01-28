using System.Threading.Tasks;

namespace SignalRStocks.Contract.Discussion
{
    public interface IDiscussionHub
    {
        Task SendMessage(string message);
    }
}
