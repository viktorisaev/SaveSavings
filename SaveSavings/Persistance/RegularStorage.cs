using SaveSavings.Converters;
using SaveSavings.Model;
using SaveSavings.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaveSavings.Persistance   // this is MODEL, not ViewModel
{
    public class RegularStorage
    {

        // create (generate, load, calculate) RegularsVM for the current global regular incomes/expenses
        public RegularsVM GetRegulars()
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    // 1) request SQL table data
                    // RegularItem = model
                    var regulars = conn.Table<RegularItem>();   // of type TableQuery<T>, BaseTableQuery, IEnumerable<T>, IEnumerable

                    // 2) process each record

                    float totalIncome = 0;
                    float totalExpense = 0;

                    RegularsVM regularsVM = new RegularsVM();
                    foreach (RegularItem r in regulars)
                    {
                        int amount = r.Amount;
                        if (amount > 0)
                        {
                            // positive = income
                            RegularItemVM regularItemVM = new RegularItemVM(r.Id, r.Name, amount, r.Period);
                            totalIncome += regularItemVM.GetDaily();
                            regularsVM.AddIncome(regularItemVM);

                        }
                        else
                        {
                            // negative = expense
                            amount = -amount;   // use absolute value
                            RegularItemVM regularItemVM = new RegularItemVM(r.Id, r.Name, amount, r.Period);
                            totalExpense += regularItemVM.GetDaily();
                            regularsVM.AddExpense(regularItemVM);
                        }
                    }

                    // 3) fill total values
                    regularsVM.SetTotalIncomeAndExpense((int)Math.Truncate(totalIncome * 100.0f), (int)Math.Truncate(totalExpense*100.0f));

                    // 4) return output VM
                    return regularsVM;
                }
            }
            catch
            {
                return null;
            }
        }




        // Insert the new contact in the Contacts table.
        public void InsertRegular(RegularItem _RegularItem)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(_RegularItem);
                });
            }
        }



        // Insert the new contact in the Contacts table.
        public RegularItem GetRegular(int _RegularItemID)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                RegularItem regularItem = conn.Table<RegularItem>().Where((v) => (v.Id ==_RegularItemID)).FirstOrDefault();

                return regularItem;
            }
        }



        //Update existing conatct   
        public void UpdateRegular(RegularItem _RegularItem)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Update(_RegularItem);
                });
            }
        }


        //Delete specific contact     
        public void DeleteRegular(int _RegularItemID)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                conn.RunInTransaction(() =>
                {
                    RegularItem ri = new RegularItem(_RegularItemID, "", REGULARS_PERIOD.DAYLY, 0);

                    conn.Delete(ri);
                });
            }
        }


    }
}
