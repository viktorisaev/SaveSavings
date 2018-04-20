using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SaveSavings.Converters;
using SaveSavings.Model;
using SaveSavings.ViewModel;


namespace SaveSavings.Persistance
{

    public class TotalStatistics
    {
        public int m_TotalExpenses;
    }

    public class ExpensesStorage
    {

        public ExpensesStorage()
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                UpdateStatistics(conn);
            }
        }


        private void UpdateStatistics(SQLite.Net.SQLiteConnection conn)
        {
            AverageExpense = GetAverageExpensePerDay(conn);
        }


        // publlic persistance data
        // should be updated with any changes
        public int AverageExpense { get; internal set; }


        internal ObservableCollection<ExpenseVM> GetExpensesByDate(DateTime date)
        {
            ObservableCollection<ExpenseVM> expenses = new ObservableCollection<ExpenseVM>();

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                DateTime dateOnlyFrom = date.Date;
                DateTime dateOnlyTo = date.AddDays(1).Date;

                var listOfItemsPerDate = conn.Table<ExpenseItem>().Where((v) => ((v.Date >= dateOnlyFrom) && (v.Date < dateOnlyTo)));

                foreach (ExpenseItem r in listOfItemsPerDate)
                {
                    expenses.Add(new ExpenseVM(r.Id, r.Date.ToLocalTime().Date, r.Amount));
                }

                return expenses;
            }
        }




        // TODO: use stored cached data to create VM
        internal ExpensesVM GetAllExpenses()
        {
            ExpensesVM outputExpenses = new ExpensesVM();

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var listOfExpenseItemsByDate = (
                    from r in conn.Table<ExpenseItem>()
                    orderby r.Date descending
                    group r by r.Date into g
                    select new ExpenseItem { Id = 0, Date = g.Key.ToLocalTime().Date, Amount = g.Sum((t) => (t.Amount)) }
                );

                ObservableCollection<ExpenseVM> expenses = new ObservableCollection<ExpenseVM>();

                // 1) list
                DateTime dateOnlyFrom = DateTime.Now.ToLocalTime().Date;    // latch first date. Local time?

                int averageIncomePerDay = App.GlobalPersistanceService.GetAverageIncomePerDay();    // should be alread there

                foreach (ExpenseItem r in listOfExpenseItemsByDate)
                {
                    // skip to next day in DB
                    while (dateOnlyFrom > r.Date)
                    {
                        outputExpenses.AddExpense(new ExpenseVM(0, dateOnlyFrom, averageIncomePerDay));
                        dateOnlyFrom = dateOnlyFrom.AddDays(-1);
                    }
                    // eventually, store the current DB value
                    outputExpenses.AddExpense(new ExpenseVM(r.Id, r.Date, averageIncomePerDay - r.Amount));
                    dateOnlyFrom = dateOnlyFrom.AddDays(-1);
                }

                // 2) average
                outputExpenses.SetAverages(AverageExpense, averageIncomePerDay, averageIncomePerDay - AverageExpense);   // use stored value
            }

            // return built result
            return outputExpenses;
        }



        // TODO: optimize out by storing all session data and update it when something changed only
        private int GetAverageExpensePerDay(SQLite.Net.SQLiteConnection conn)
        {
            var listOfExpenseItemsByDate = (
                from r in conn.Table<ExpenseItem>()
                orderby r.Date descending
                group r by r.Date into g
                select new ExpenseItem { Id = 0, Date = g.Key.ToLocalTime().Date, Amount = g.Sum((t) => (t.Amount)) }
            );

            // 1) list
            DateTime dateOnlyFrom = DateTime.Now.ToLocalTime().Date;    // latch first date

            int sum = 0;
            int daysCount = 0;

            foreach (ExpenseItem r in listOfExpenseItemsByDate)
            {
                while (dateOnlyFrom > r.Date)
                {
                    sum += 0;
                    daysCount += 1;
                    dateOnlyFrom = dateOnlyFrom.AddDays(-1);
                }
                sum += r.Amount;
                daysCount += 1;
                dateOnlyFrom = dateOnlyFrom.AddDays(-1);
            }

            // 2) average
            int avg = (int)Math.Truncate((float)sum / (float)daysCount);

            return avg;
        }



        // Insert the new contact in the Contacts table.   
        public void Insert(ExpenseItem objContact)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(objContact);
                });

                // update average
                UpdateStatistics(conn);
            }
        }




        //Update existing conatct   
        public void UpdateDetails(ExpenseItem ObjContact)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Update(ObjContact);
                });

                // update average
                UpdateStatistics(conn);
            }
        }


        //Delete specific contact     
        public void DeleteContact(int Id)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    ExpenseItem ri = new ExpenseItem(Id, DateTime.Now, 0);

                    conn.Delete(ri);
                });

                // update average
                UpdateStatistics(conn);
            }
        }



        public int GetAverageExpense()
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    var allExpenses = conn.Table<ExpenseItem>();

                    var expenses = (from r in allExpenses
                                    group r by r.Date into g
                                    select new { Amount = g.Sum((t) => (t.Amount)) }
                                    );

                    float avgCurrency = (float)expenses.Average(o => o.Amount) / 100.0f;

                    int avg = DataConversion.ConvertCurrencyToCents(avgCurrency);

                    return avg;
                }
            }
            catch
            {
                return 0;   // error!!
            }
        }




        public TotalStatistics GetTotalStatistics()
        {
            TotalStatistics stats = new TotalStatistics();

            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    // total spent
                    stats.m_TotalExpenses = conn.Table<ExpenseItem>().Sum(v => v.Amount);
                }
            }
            catch
            {
            }

            return stats;
        }



        internal List<ExpenseItem> GetAmountsForDate(DateTime date)
        {
            // extract records for 'date'
            //            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    DateTime dateOnlyFrom = date.Date;
                    DateTime dateOnlyTo = date.AddDays(1).Date;

                    //                    Expression<Func<Spends, bool>> myExpression = ;

                    List<ExpenseItem> myCollection = conn.Table<ExpenseItem>().Where((v) => ((v.Date >= dateOnlyFrom) && (v.Date < dateOnlyTo))).ToList<ExpenseItem>();

                    foreach (ExpenseItem s in myCollection)
                    {
                        s.Date = s.Date.ToLocalTime().Date;
                    }

                    return myCollection;
                }
            }
            //catch
            //{
            //    return null;
            //}
        }











        DatabaseHelperClass Db_Helper = new DatabaseHelperClass();


    }   // class DatabaseStorage

}   // namespace SaveSavings
