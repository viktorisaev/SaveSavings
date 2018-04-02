using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SaveSavings
{
    public class Spends
    {
        //The Id property is marked as the Primary Key  
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [SQLite.Net.Attributes.Ignore]
        public DateTime DateOnly
        {
            get
            {
                return Date.Date;
            }
            set
            {

            }
        }

        public int Amount { get; set; }


        public Spends()
        {
            //empty constructor  
        }


        public Spends(DateTime _Date, int _Amount)
        {
            this.Date = _Date;
            this.Amount = _Amount;
        }

        public DateTime GetDateOnly()
        {
            return this.Date.Date;
        }

    }   // class Spends

}   // namespace SaveSavings




