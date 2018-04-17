using SaveSavings.Converters;
using SaveSavings.Helpers;
using SaveSavings.Model;
using System;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace SaveSavings.ViewModel
{
    public class UniqueExpenseVM
    {
        public int Id { get; set; }     //The Id property is marked as the Primary Key  
        public DateTime Date { get; set; }
        public float Amount { get; set; }
        public Brush AmountColor { get; set; }
        public string Name { get; set; }


        public UniqueExpenseVM()
        {
            //empty constructor  
        }


        public UniqueExpenseVM(int _Id, DateTime _Date, int _Amount, string _Name)
        {
            this.Id = _Id;
            this.Date = _Date;
            this.Amount = DataConversion.ConvertCentsToCurrency( Math.Abs(_Amount) );
            this.AmountColor = ViewHelpers.GetColorByAmount( _Amount );
            this.Name = _Name;
        }


        public DateTime GetDateOnly()
        {
            return this.Date.Date;
        }


    }   // class ExpenseVM

}   // namespace SaveSavings.ViewModel
