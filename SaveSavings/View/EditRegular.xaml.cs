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

            // DO NOT NEED THIS - WE USE MVVM!!

            //            listBoxobj.ItemsSource = DB_ContactList.OrderByDescending(i => i.Date).ToList();

            //            CultureInfo myCI = CultureInfo.CurrentCulture;
            //            w_DailyCurrentDate.Text = myCI.NumberFormat.CurrencyDecimalSeparator + myCI.NumberFormat.NumberDecimalSeparator + myCI.NumberFormat.CurrencySymbol;

            // THIS IS PROPER CODE!!!
            //            w_DailyCurrentDate.Text = string.Format("{0:D}", m_CurrentDate);
        }

    }
}
