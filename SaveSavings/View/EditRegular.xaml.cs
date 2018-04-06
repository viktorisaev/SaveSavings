using SaveSavings.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SaveSavings.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditRegular : Page
    {
        public EditRegular()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // VM passed from previous page
            EditRegularItemVM editRegularItemVM = e.Parameter as EditRegularItemVM;

            // set data (ad execute bindings)
            this.DataContext = editRegularItemVM;
        }

    }
}
