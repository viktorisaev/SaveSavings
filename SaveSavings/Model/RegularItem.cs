namespace SaveSavings.Model
{

    public enum REGULARS_PERIOD
    {
        DAYLY = 0,
        MONTHLY,
        YEARLY
    }


    public class RegularItem
    {

        // Table data (columns of SQL table)

        //The Id property is marked as the Primary Key  
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }     // positive = income, negative = expense, 0 = impossible

        public REGULARS_PERIOD Period { get; set; }     // daily, monthly, yearly




        public RegularItem()
        {
            //empty constructor  
        }


        public RegularItem(string _Name, REGULARS_PERIOD _Period, int _Amount)
        {
            this.Name = _Name;
            this.Period = _Period;
            this.Amount = _Amount;
        }

    }
}
