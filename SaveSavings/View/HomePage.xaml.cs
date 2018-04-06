using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using SaveSavings.Model;
using SaveSavings.ViewModel;
using SaveSavings.Converters;
using SaveSavings.Persistance;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SaveSavings.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        ObservableCollection<ExpenseVM> DB_ContactList = new ObservableCollection<ExpenseVM>();



        public HomePage()
        {
            this.InitializeComponent();
            this.Loaded += ReadContactList_Loaded;

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }



        private void ReadContactList_Loaded(object sender, RoutedEventArgs e)
        {
            ExpensesStorage dbcontacts = new ExpensesStorage();
            DB_ContactList = dbcontacts.GetAllExpenses();//Get all DB expenses

            TotalStatistics stats = new DatabaseHelperClass().GetTotalStatistics();

            w_TotalExpenses.Text = string.Format("{0:C}", DataConversion.ConvertCentsToCurrency(stats.m_TotalExpenses));

            // set list data - recent dates first
            listBoxobj.ItemsSource = DB_ContactList.OrderByDescending(i => i.Date).ToList();
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

    }   // class HomePage
}
