using SignalRStocks.Contract.Discussion;
using SignalRStocks.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace SignalRStocks.Pages
{
    public partial class DiscussionPage : ContentPage
    {
        public DiscussionPage()
        {
            InitializeComponent();

            // Make sure the page is not up in status bar and not in swipe-up area
            On<iOS>().SetUseSafeArea(true);

            BindingContext = new DiscussionViewModel(new DiscussionService());
        }
    }
}
