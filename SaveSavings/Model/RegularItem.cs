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


    class RegularItem
    {
        //The Id property is marked as the Primary Key  
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int Id { get; set; }

        public int Amount { get; set; }     // positive = income, negative = expense, 0 = impossible

        public REGULARS_PERIOD Period { get; set; }     // monthly or yearly

        public string Name { get; set; }



        public RegularItem()
        {
            //empty constructor  
        }


        public RegularItem(REGULARS_PERIOD _Period, int _Amount, string _Name)
        {
            this.Period = _Period;
            this.Amount = _Amount;
            this.Name = _Name;
        }

    }
}
