using SaveSavings.Converters;
using SaveSavings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveSavings.ViewModel
{
    public class ExpenseVM
    {
        public int Id { get; set; }     //The Id property is marked as the Primary Key  

        public DateTime Date { get; set; }

        public float Amount { get; set; }


        public ExpenseVM()
        {
            //empty constructor  
        }


        public ExpenseVM(int _Id, DateTime _Date, int _Amount)
        {
            this.Id = _Id;
            this.Date = _Date;
            this.Amount = DataConversion.ConvertCentsToCurrency(_Amount);
        }


        public ExpenseVM(ExpenseItem _Expense)
        {
            this.Id = _Expense.Id;
            this.Date = _Expense.Date;
            this.Amount = DataConversion.ConvertCentsToCurrency( _Expense.Amount );
        }

        public DateTime GetDateOnly()
        {
            return this.Date.Date;
        }

    }
}
