using SaveSavings.Converters;
using SaveSavings.Model;
using System;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace SaveSavings.ViewModel
{
    public class ExpenseVM
    {
        public int Id { get; set; }     //The Id property is marked as the Primary Key  
        public DateTime Date { get; set; }
        public float Amount { get; set; }
        public Brush AmountColor { get; set; }


        public ExpenseVM()
        {
            //empty constructor  
        }


        public ExpenseVM(int _Id, DateTime _Date, int _Amount)
        {
            this.Id = _Id;
            this.Date = _Date;
            this.Amount = DataConversion.ConvertCentsToCurrency( _Amount );
            this.AmountColor = GetColorByAmount( _Amount );
        }


        private Brush GetColorByAmount(int amount)
        {
            // TODO: create and reference single 
            return amount < 0 ? m_NegativeAmountBrush : m_PositiveAmountBrush;
        }


        public DateTime GetDateOnly()
        {
            return this.Date.Date;
        }


        private static Brush m_PositiveAmountBrush = new SolidColorBrush(Colors.Green);
        private static Brush m_NegativeAmountBrush = new SolidColorBrush(Colors.Red);


    }   // class ExpenseVM

}   // namespace SaveSavings.ViewModel
