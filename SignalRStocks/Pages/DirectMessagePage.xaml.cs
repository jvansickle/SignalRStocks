using SignalRStocks.Contract.DirectMessage;
using SignalRStocks.Contract.User;
using SignalRStocks.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace SignalRStocks.Pages
{
    public partial class DirectMessagePage : ContentPage
    {
        public DirectMessagePage()
        {
            InitializeComponent();

            On<iOS>().SetUseSafeArea(true);

            BindingContext = new DirectMessageViewModel(new UserService());
        }
    }
}
