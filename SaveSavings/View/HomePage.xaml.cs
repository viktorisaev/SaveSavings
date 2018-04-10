using SaveSavings.Persistance;
using SaveSavings.ViewModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SaveSavings.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
//        ObservableCollection<ExpenseVM> DB_ContactList = new ObservableCollection<ExpenseVM>();



        public HomePage()
        {
            this.InitializeComponent();
            this.Loaded += ReadContactList_Loaded;

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }



        private void ReadContactList_Loaded(object sender, RoutedEventArgs e)
        {
            ExpensesStorage dbcontacts = new ExpensesStorage();
            ExpensesVM expenses = dbcontacts.GetAllExpenses();//Get all DB expenses

            // everyday expenses
            AllExpensesVM allExpenses = new AllExpensesVM();
            allExpenses.Expenses = expenses;

            // unique expenses
            UniqueExpensesVM uniqueExpenses = dbcontacts.GetAllUniqueExpenses();
            allExpenses.UniqueExpenses = uniqueExpenses;

            this.DataContext = allExpenses;


            // use Binding, not setters!!

//            TotalStatistics stats = new DatabaseHelperClass().GetTotalStatistics();

//            w_TotalExpenses.Text = string.Format("{0:C}", DataConversion.ConvertCentsToCurrency(stats.m_TotalExpenses));

            // set list data - recent dates first
//            listBoxobj.ItemsSource = DB_ContactList.OrderByDescending(i => i.Date).ToList();
        }



        private void DateListItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxobj.SelectedIndex != -1)
            {
                ExpenseVM listitem = listBoxobj.SelectedItem as ExpenseVM;//Get slected listbox item contact ID
                Frame.Navigate(typeof(DailyPage), listitem.Date);
            }
        }



        private void OnAddExpense_Clicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddContact));
        }


        // TODO: pivot?
        private void OnSeeRegulars_Clicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegularsPage));
        }


    

        private void OnAddUniqueExpense_Clicked(object sender, RoutedEventArgs e)
        {
            GotoEditUniqueExpensePage(true, EditUniqueItemVM.NEW_REGULAR_ITEM_ID);
        }


        private void OnUniqueExpense_Selected(object sender, RoutedEventArgs e)
        {
            if (w_UniqueExpensesList.SelectedIndex != -1)
            {
                UniqueExpenseVM uiVM = w_UniqueExpensesList.SelectedItem as UniqueExpenseVM;   //Get selected item from the list
                GotoEditUniqueExpensePage(false, uiVM.Id);
            }
        }

        


        private void GotoEditUniqueExpensePage(bool _IsAdd, int _UniqueItemId)
        {
            // create VM
            EditUniqueItemVM uniqueExpenseItem = new EditUniqueItemVM(_IsAdd, _UniqueItemId);

            // jump to the EditRegular page
            this.Frame.Navigate(typeof(EditUniqueExpense), uniqueExpenseItem);
        }

    }   // class HomePage
}
