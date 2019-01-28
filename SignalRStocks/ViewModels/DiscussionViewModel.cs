using System.Collections.ObjectModel;
using SignalRStocks.Contract.Discussion;
using Xamarin.Forms;

namespace SignalRStocks.ViewModels
{
    public class DiscussionViewModel : ViewModel
    {
        ObservableCollection<string> _messages;
        public ObservableCollection<string> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                NotifyPropertyChanged();
            }
        }

        string _messageText;
        public string MessageText
        {
            get => _messageText;
            set
            {
                _messageText = value;
                NotifyPropertyChanged();
                Send.ChangeCanExecute();
            }
        }

        Command _send;
        public Command Send
        {
            get
            {
                if (_send == null)
                    _send = new Command(HandleSend, (object sender) =>
                    {
                        return !string.IsNullOrWhiteSpace(MessageText);
                    });

                return _send;
            }
        }

        private readonly DiscussionService discussionService;

        public DiscussionViewModel(DiscussionService discussionService)
        {
            Messages = new ObservableCollection<string>();

            this.discussionService = discussionService;

            this.discussionService.MessageReceived += HandleMessageReceived;

            this.discussionService.StartConnectionAsync();
        }

        void HandleMessageReceived(string message)
        {
            Messages.Add(message);
        }

        async void HandleSend(object sender)
        {
            await discussionService.SendMessage(MessageText);
            MessageText = string.Empty;
        }
    }
}
