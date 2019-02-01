using System.Collections.ObjectModel;
using SignalRStocks.Contract.DirectMessage;
using SignalRStocks.Contract.User;
using SignalRStocks.Models;
using Xamarin.Forms;

namespace SignalRStocks.ViewModels
{
    public class DirectMessageViewModel : ViewModel
    {
        string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; NotifyPropertyChanged(); }
        }

        ObservableCollection<ReceivedMessage> _receivedMessages;
        public ObservableCollection<ReceivedMessage> ReceivedMessages
        {
            get { return _receivedMessages; }
            set { _receivedMessages = value; NotifyPropertyChanged(); }
        }

        string _to;
        public string To
        {
            get { return _to; }
            set { _to = value; NotifyPropertyChanged(); }
        }

        string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; NotifyPropertyChanged(); }
        }

        Command _sendMessageCommand;
        public Command SendMessage
        {
            get
            {
                if(_sendMessageCommand == null)
                {
                    _sendMessageCommand = new Command(async () =>
                    {
                        await directMessageService.SendMessageAsync(To, Message);
                    });
                }

                return _sendMessageCommand;
            }
        }

        readonly UserService userService;
        readonly DirectMessageService directMessageService;

        public DirectMessageViewModel(UserService userService)
        {
            this.userService = userService;
            FullName = this.userService.GetUserIdentifier();

            directMessageService = new DirectMessageService(FullName);

            directMessageService.DirectMessageReceived += HandleDirectMessageReceived;
            directMessageService.StartConnectionAsync();
        }

        void HandleDirectMessageReceived(DirectMessage dm)
        {
            ReceivedMessages.Add(new ReceivedMessage { From = dm.From, Message = dm.Message });
        }
    }
}
