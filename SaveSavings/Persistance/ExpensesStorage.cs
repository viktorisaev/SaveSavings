using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using SaveSavings.Model;
using SaveSavings.ViewModel;


namespace SaveSavings.Persistance
{
    public class ExpensesStorage
    {
        DatabaseHelperClass Db_Helper = new DatabaseHelperClass();


        //public ObservableCollection<Spends> GetAllContacts()
        //{
        //    return Db_Helper.ReadAllContacts();
        //}


        internal ObservableCollection<ExpenseVM> GetDateContacts(DateTime date)
        {
            List<ExpenseItem> spends = Db_Helper.GetAmountsForDate(date);

            ObservableCollection<ExpenseVM> expenses = new ObservableCollection<ExpenseVM>();

            foreach(ExpenseItem spend in spends)
            {
                expenses.Add(new ExpenseVM(spend));
            }

            return expenses;

        }



        internal ObservableCollection<ExpenseVM> GetAllExpenses()
        {
            List<ExpenseItem> spends = Db_Helper.GetAllExpenses();

            ObservableCollection<ExpenseVM> expenses = new ObservableCollection<ExpenseVM>();

            foreach (ExpenseItem spend in spends)
            {
                expenses.Add(new ExpenseVM(spend));
            }

            return expenses;

        }




        //string[] StubNames =
        //{
        //    "Зарплата",
        //    "BahnCard",
        //    "ARD",
        //    "DJH Deutschland",
        //    "Sicherung",
        //    "Хлебушек"
        //};



        //internal RegularsVM GetRegulars()
        //{
        //    RegularsVM regularsVM = Db_Helper.GetRegulars();

        //    // TODO: this is stub data
        //    regularsVM.SetTotalIncomeAndExpense(6564, 2545);

        //    Random rnd = new Random();

        //    List<RegularItemVM> lst = new List<RegularItemVM>();
        //    for (int i = 0, ei = rnd.Next(3,10); i < ei; ++i)
        //    {
        //        lst.Add(new RegularItemVM(StubNames[rnd.Next(0, StubNames.Length)], rnd.Next(20, 200000), rnd.Next(0, 2) == 0 ));
        //    }
        //    regularsVM.SetIncomes(lst);

        //    lst = new List<RegularItemVM>();
        //    for (int i = 0, ei = rnd.Next(3, 10); i < ei; ++i)
        //    {
        //        lst.Add(new RegularItemVM(StubNames[rnd.Next(0, StubNames.Length)], rnd.Next(20, 10000), rnd.Next(0, 2) == 0));
        //    }
        //    regularsVM.SetSpends(lst);


        //    return regularsVM;
        //}


    }   // class DatabaseStorage

}   // namespace SaveSavings
