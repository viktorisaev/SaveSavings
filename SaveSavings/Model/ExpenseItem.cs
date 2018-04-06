using System;

namespace SaveSavings.Model
{
    public class ExpenseItem
    {

        // Table data (columns of SQL table)

        //The Id property is marked as the Primary Key  
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Amount { get; set; }


        public ExpenseItem()
        {
            //empty constructor  
        }


        public ExpenseItem(int _Id, DateTime _Date, int _Amount)
        {
            this.Id = _Id;
            this.Date = _Date;
            this.Amount = _Amount;
        }

    }   // class Spends

}   // namespace SaveSavings




