using System.Collections.ObjectModel;
using SignalRStocks.Contract.Group;
using SignalRStocks.Models;
using Xamarin.Forms;

namespace SignalRStocks.ViewModels
{
    public class GroupViewModel : ViewModel
    {
        readonly GroupService groupService;

        public GroupViewModel(GroupService groupService)
        {
            this.groupService = groupService;
            this.groupService.ReceiveMessage += OnReceiveMessage;
            this.groupService.StartConnectionAsync();
        }

        void OnReceiveMessage(string groupName, string message)
        {
            Messages.Add(new GroupMessage { GroupName = groupName, Message = message });
        }

        #region Bound Properties
        string _groupNameToJoin;
        public string GroupNameToJoin
        {
            get => _groupNameToJoin;
            set
            {
                _groupNameToJoin = value;
                NotifyPropertyChanged();
                JoinGroup.ChangeCanExecute();
            }
        }

        string _groupNameToLeave;
        public string GroupNameToLeave
        {
            get => _groupNameToLeave;
            set
            {
                _groupNameToLeave = value;
                NotifyPropertyChanged();
                LeaveGroup.ChangeCanExecute();
            }
        }

        ObservableCollection<GroupMessage> _messages;
        public ObservableCollection<GroupMessage> Messages
        {
            get
            {
                if (_messages == null)
                    _messages = new ObservableCollection<GroupMessage>();

                return _messages;
            }
            set
            {
                _messages = value;
                NotifyPropertyChanged();
            }
        }

        string _messageTo;
        public string MessageTo
        {
            get { return _messageTo; }
            set
            {
                _messageTo = value;
                NotifyPropertyChanged();
                SendMessage.ChangeCanExecute();
            }
        }

        string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyPropertyChanged();
                SendMessage.ChangeCanExecute();
            }
        }

        Command _joinGroup;
        public Command JoinGroup
        {
            get
            {
                if (_joinGroup == null)
                {
                    _joinGroup = new Command(async () =>
                    {
                        await groupService.JoinGroupAsync(GroupNameToJoin);
                    }, () =>
                    {
                        return !string.IsNullOrWhiteSpace(GroupNameToJoin);
                    });
                }

                return _joinGroup;
            }
        }

        Command _leaveGroup;
        public Command LeaveGroup
        {
            get
            {
                if (_leaveGroup == null)
                {
                    _leaveGroup = new Command(async () =>
                    {
                        await groupService.LeaveGroupAsync(GroupNameToLeave);
                    }, () =>
                    {
                        return !string.IsNullOrWhiteSpace(GroupNameToLeave);
                    });
                }

                return _leaveGroup;
            }
        }

        Command _sendMessage;
        public Command SendMessage
        {
            get
            {
                if (_sendMessage == null)
                {
                    _sendMessage = new Command(async () =>
                    {
                        await groupService.SendMessageToGroupAsync(MessageTo, Message);
                    }, () =>
                    {
                        return !string.IsNullOrWhiteSpace(MessageTo) && !string.IsNullOrWhiteSpace(Message);
                    });
                }

                return _sendMessage;
            }
        }
        #endregion
    }
}
