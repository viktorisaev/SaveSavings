using System;


namespace SaveSavings.Model
{
    public class UniqueExpenseItem
    {
        // Table data (columns of SQL table)

        //The Id property is marked as the Primary Key  
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Amount { get; set; }

        public string Name { get; set; }


        public UniqueExpenseItem()
        {
            //empty constructor  
        }


        public UniqueExpenseItem(int _Id, DateTime _Date, int _Amount, string _Name)
        {
            this.Id = _Id;
            this.Date = _Date;
            this.Amount = _Amount;
            this.Name = _Name;
        }

    }
}
