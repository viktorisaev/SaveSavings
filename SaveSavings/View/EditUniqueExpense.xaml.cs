using SaveSavings.ViewModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;



namespace SaveSavings.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditUniqueExpense : Page
    {
        public EditUniqueExpense()
        {
            this.InitializeComponent();

            SystemNavigationManager.GetForCurrentView().BackRequested += BackSpaceInvoked;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // VM passed from previous page
            EditUniqueItemVM editUniqueExpenseItemVM = e.Parameter as EditUniqueItemVM;

            // set data (ad execute bindings)
            this.DataContext = editUniqueExpenseItemVM;
        }



        private void BackSpaceInvoked(object sender, BackRequestedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
            e.Handled = true;
        }


    }
}
