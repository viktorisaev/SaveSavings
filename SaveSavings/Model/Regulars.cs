using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveSavings.Model
{

    enum REGULARS_PERIOD
    {
        MONTHLY = 0,
        YEARLY
    }


    class Regulars
    {
        //The Id property is marked as the Primary Key  
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int Id { get; set; }

        public int Amount { get; set; }

        public REGULARS_PERIOD Period { get; set; }

        public Regulars()
        {
            //empty constructor  
        }


        public Regulars(REGULARS_PERIOD _Period, int _Amount)
        {
            this.Period = _Period;
            this.Amount = _Amount;
        }

    }
}
