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


        public ExpenseVM(Spends _Spends)
        {
            this.Id = _Spends.Id;
            this.Date = _Spends.Date;
            this.Amount = _Spends.Amount / 100.0f;
        }

        public DateTime GetDateOnly()
        {
            return this.Date.Date;
        }

    }
}
