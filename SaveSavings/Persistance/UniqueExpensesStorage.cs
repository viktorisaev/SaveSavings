using SaveSavings.Model;
using SaveSavings.ViewModel;
using System;
using System.Linq;

namespace SaveSavings.Persistance
{
    public class UniqueExpensesStorage
    {
//        public int RegularIncomePerDay { get; set; }

        public int TotalUniqueAmountSinceFixedDate { get; internal set; }


        public UniqueExpensesStorage(DateTimeOffset _DateFixedAmount)
        {

            // calculate total unique
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var regulars = conn.Table<UniqueExpenseItem>();   // of type TableQuery<T>, BaseTableQuery, IEnumerable<T>, IEnumerable

                int totalAmount = (from r in regulars
                                   where r.Date >= _DateFixedAmount
                                   select r.Amount).Sum();

                TotalUniqueAmountSinceFixedDate = -totalAmount;
            }
        }


        // TODO: use stored cached data to create VM (possibly ?)
        internal UniqueExpensesVM GetAllUniqueExpenses()
        {
            UniqueExpensesVM outputUniqueExpenses = new UniqueExpensesVM();

            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    var dbTable = conn.Table<UniqueExpenseItem>();

                    var listOfUniqueExpenses = (from r in dbTable
                              orderby r.Date descending
                              select new UniqueExpenseItem(r.Id, r.Date, r.Amount, r.Name)
                              );

                    // build VM
                    int totalAmount = 0;

                    // 1) list
                    foreach (UniqueExpenseItem uei in listOfUniqueExpenses)
                    {
                        // eventually, store the current DB value
                        outputUniqueExpenses.AddExpense(new UniqueExpenseVM(uei.Id, uei.Date.ToLocalTime(), uei.Amount, uei.Name));

                        totalAmount += uei.Amount;
                    }

                    // 2) total
                    outputUniqueExpenses.SetTotal(totalAmount);


                }
            }
            catch
            {
            }

            // return built result
            return outputUniqueExpenses;
        }



        // Insert the new contact in the Contacts table.
        public void InsertUniqueExpense(UniqueExpenseItem _Item)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(_Item);
                });
            }
        }



        // Insert the new contact in the Contacts table.
        public UniqueExpenseItem GetUniqueExpense(int _ItemID)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                UniqueExpenseItem regularItem = conn.Table<UniqueExpenseItem>().Where((v) => (v.Id == _ItemID)).FirstOrDefault();

                return regularItem;
            }
        }



        //Update existing conatct   
        public void UpdateUniqueExpense(UniqueExpenseItem _Item)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Update(_Item);
                });
            }
        }


        //Delete specific contact     
        public void DeleteUniqueExpense(int _ItemID)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                conn.RunInTransaction(() =>
                {
                    UniqueExpenseItem ri = new UniqueExpenseItem(_ItemID, DateTime.Now, 0, "");

                    conn.Delete(ri);
                });
            }
        }


        DatabaseHelperClass m_DBHelper = new DatabaseHelperClass();


    }
}
