using SaveSavings.Persistance;
using SaveSavings.ViewModel;
using System;
using System.Linq;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

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

        public class FinancialStuff
        {
            public int IdX { get; set; }
            public float Amount { get; set; }
        }


        private void ReadContactList_Loaded(object sender, RoutedEventArgs e)
        {
            ExpensesStorage dbcontacts = new ExpensesStorage();
            AllExpensesVM allExpenses = new AllExpensesVM();

            // everyday expenses
            ExpensesVM expenses = dbcontacts.GetAllExpenses();//Get all DB expenses
            allExpenses.Expenses = expenses;

            // unique expenses
            UniqueExpensesStorage db = App.GlobalPersistanceService.GetUniqueExpensesStorage();
            UniqueExpensesVM uniqueExpenses = db.GetAllUniqueExpenses();
            allExpenses.UniqueExpenses = uniqueExpenses;

            // overall
            // TODO: use DB
            //UniqueExpensesStorage db = new UniqueExpensesStorage();
            //OverallVM overall = db.GetAllUniqueExpenses();

            OverallVM overall = new OverallVM(new DateTime(2018, 03, 25), DateTime.Now, new DateTime(2018, 06, 22), App.GlobalPersistanceService.GetAverageSavingsPerDay(), 
                new DateTime(2018, 03, 29), 7855);
            allExpenses.Overall = overall;
            

            this.DataContext = allExpenses;


            // use Binding, not setters!!

            //            TotalStatistics stats = new DatabaseHelperClass().GetTotalStatistics();

            //            w_TotalExpenses.Text = string.Format("{0:C}", DataConversion.ConvertCentsToCurrency(stats.m_TotalExpenses));

            // set list data - recent dates first
            //            listBoxobj.ItemsSource = DB_ContactList.OrderByDescending(i => i.Date).ToList();


            Random rand = new Random();
            List<FinancialStuff> financialStuffList = new List<FinancialStuff>();

            int idx = 0; ;
            float sum = 78.55f;

            // sum up unique expenses before first regular date
            if (allExpenses.Expenses.ExpensesList.Count > 0)
            {
                DateTime dateOnlyFrom = allExpenses.Expenses.ExpensesList[allExpenses.Expenses.ExpensesList.Count - 1].GetDateOnly().Date;
                var uniqueExpensesBeforeTheDate = allExpenses.UniqueExpenses.ExpensesList.Where((v) => ((v.Date < dateOnlyFrom))).Sum((v) => v.SignedAmount);
                sum += uniqueExpensesBeforeTheDate;
            }

            DateTime now = DateTime.Now;

            for ( int i = allExpenses.Expenses.ExpensesList.Count - 1, ei = 0 ; i >= ei ; --i )
            {
                var r = allExpenses.Expenses.ExpensesList[i];
                sum += r.Amount;

                // get unique expenses for the date
                DateTime dateOnlyFrom = r.GetDateOnly().Date;
                DateTime dateOnlyTo = dateOnlyFrom.AddDays(1).Date;
                var uniqueExpensesForTheDate = allExpenses.UniqueExpenses.ExpensesList.Where((v) => ((v.Date >= dateOnlyFrom) && (v.Date < dateOnlyTo))).Sum((v)=>v.SignedAmount);

                sum += uniqueExpensesForTheDate;

                // display 30 last days (to limit amount of chart data)
                if ((DateTime.Now - r.GetDateOnly()).Days < 30)
                {
                    // add point to the chart
                    financialStuffList.Add(new FinancialStuff() { IdX = idx, Amount = sum });
                    idx += 1;
                }
            }

            (LineChart.Series[0] as LineSeries).ItemsSource = financialStuffList;


        }



        private void DateListItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxobj.SelectedIndex != -1)
            {
                ExpenseVM listitem = listBoxobj.SelectedItem as ExpenseVM;//Get slected listbox item contact ID
                Frame.Navigate(typeof(DailyPage), listitem.GetDateOnly());
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

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = 1;
        }
    }   // class HomePage
}
