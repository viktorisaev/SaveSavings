using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using SaveSavings.Model;
using SaveSavings.ViewModel;


namespace SaveSavings.Persistance
{
    public class ExpensesStorage
    {

        public ExpensesStorage()
        {
            int averageExpensePerDay = GetAverageExpensePerDay();

            AverageExpense = averageExpensePerDay;
        }


        // publlic persistance data
        // should be updated with any changes
        public int AverageExpense { get; internal set; }


        //public ObservableCollection<Spends> GetAllContacts()
        //{
        //    return Db_Helper.ReadAllContacts();
        //}


        internal ObservableCollection<ExpenseVM> GetExpensesByDate(DateTime date)
        {
            List<ExpenseItem> expensesList = Db_Helper.GetAmountsForDate(date);

            ObservableCollection<ExpenseVM> expenses = new ObservableCollection<ExpenseVM>();

            foreach(ExpenseItem expenseItem in expensesList)
            {
                expenses.Add(new ExpenseVM(expenseItem.Id, expenseItem.Date, expenseItem.Amount));
            }

            return expenses;

        }



        // TODO: use stored cached data to create VM
        internal ExpensesVM GetAllExpenses()
        {
            ExpensesVM outputExpenses = new ExpensesVM();

            var listOfExpenses = Db_Helper.GetAllExpenses();    // refactor: do not use temp List<>

            ObservableCollection<ExpenseVM> expenses = new ObservableCollection<ExpenseVM>();

            // 1) list

            DateTime dateOnlyFrom = DateTime.Now.ToLocalTime().Date;    // latch first date. Local time?

            int averageIncomePerDay = App.GlobalPersistanceService.GetAverageIncomePerDay();    // should be alread there

            foreach (ExpenseItem expenseItem in listOfExpenses)
            {
                // skip to next day in DB
                while (dateOnlyFrom > expenseItem.Date)
                {
                    outputExpenses.AddExpense(new ExpenseVM(0, dateOnlyFrom, averageIncomePerDay));
                    dateOnlyFrom = dateOnlyFrom.AddDays(-1);
                }
                // eventually, store the current DB value
                outputExpenses.AddExpense(new ExpenseVM(expenseItem.Id, expenseItem.Date, averageIncomePerDay - expenseItem.Amount));
                dateOnlyFrom = dateOnlyFrom.AddDays(-1);
            }

            // 2) average
            outputExpenses.SetAverages(AverageExpense, averageIncomePerDay, averageIncomePerDay - AverageExpense);   // use stored value

            // return built result
            return outputExpenses;
        }




        // TODO: use stored cached data to create VM
        internal UniqueExpensesVM GetAllUniqueExpenses()
        {
            UniqueExpensesVM outputUniqueExpenses = new UniqueExpensesVM();

            // TODO: call DB
            //var listOfExpenses = Db_Helper.GetAllExpenses();    // refactor: do not use temp List<>

            //ObservableCollection<ExpenseVM> expenses = new ObservableCollection<ExpenseVM>();

            //// 1) list

            //DateTime dateOnlyFrom = DateTime.Now.ToLocalTime().Date;    // latch first date. Local time?

            //int averageIncomePerDay = App.GlobalPersistanceService.GetAverageIncomePerDay();    // should be alread there

            //foreach (ExpenseItem expenseItem in listOfExpenses)
            //{
            //    // skip to next day in DB
            //    while (dateOnlyFrom > expenseItem.Date)
            //    {
            //        outputExpenses.AddExpense(new ExpenseVM(0, dateOnlyFrom, averageIncomePerDay));
            //        dateOnlyFrom = dateOnlyFrom.AddDays(-1);
            //    }
            //    // eventually, store the current DB value
            //    outputExpenses.AddExpense(new ExpenseVM(expenseItem.Id, expenseItem.Date, averageIncomePerDay - expenseItem.Amount));
            //    dateOnlyFrom = dateOnlyFrom.AddDays(-1);
            //}

            //// 2) average
            //outputExpenses.SetAverages(AverageExpense, averageIncomePerDay, averageIncomePerDay - AverageExpense);   // use stored value


            // MOCK data
            for (int i = 0, ei = 10; i < ei; ++i)
            {
                outputUniqueExpenses.AddExpense(new UniqueExpenseVM(0, DateTime.Now, 123 + i * 100, "Data " + i.ToString()));
            }


            outputUniqueExpenses.SetTotal(12300);


            // return built result
            return outputUniqueExpenses;
        }





        // TODO: optimize out by storing all session data and update it when something changed only
        private int GetAverageExpensePerDay()
        {
            ExpensesVM outputExpenses = new ExpensesVM();

            var listOfExpenses = Db_Helper.GetAllExpenses();    // refactor: do not use temp List<>

            ObservableCollection<ExpenseVM> expenses = new ObservableCollection<ExpenseVM>();

            // 1) list
            DateTime dateOnlyFrom = DateTime.Now.ToLocalTime().Date;    // latch first date

            int sum = 0;
            int daysCount = 0;

            foreach (ExpenseItem expenseItem in listOfExpenses)
            {
                while (dateOnlyFrom > expenseItem.Date)
                {
                    sum += 0;
                    daysCount += 1;
                    dateOnlyFrom = dateOnlyFrom.AddDays(-1);
                }
                sum += expenseItem.Amount;
                daysCount += 1;
                dateOnlyFrom = dateOnlyFrom.AddDays(-1);
            }

            // 2) average
            int avg = (int)Math.Truncate((float)sum / (float)daysCount);

            return avg;
        }



        DatabaseHelperClass Db_Helper = new DatabaseHelperClass();


    }   // class DatabaseStorage

}   // namespace SaveSavings
