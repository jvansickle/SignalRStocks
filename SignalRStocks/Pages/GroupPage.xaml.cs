using SignalRStocks.Contract.Group;
using SignalRStocks.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace SignalRStocks.Pages
{
    public partial class GroupPage : ContentPage
    {
        public GroupPage()
        {
            InitializeComponent();

            On<iOS>().SetUseSafeArea(true);

            BindingContext = new GroupViewModel(new GroupService());
        }
    }
}
