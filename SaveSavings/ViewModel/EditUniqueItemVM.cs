
using SaveSavings.Converters;
using SaveSavings.Helpers;
using SaveSavings.Model;
using SaveSavings.Persistance;
using System;
using Windows.UI.Xaml;

namespace SaveSavings.ViewModel
{


    public class EditUniqueItemVM
    {
        public string UpdateButtonText { get; set; }
        public string UpdateButtonColor { get; set; }

        public int UniqueExpenseItemId { get; set; }

        public DateTimeOffset Date { get; set; }

        public string Name { get { return m_Name; }
            set {
                m_Name = value;
                AddCommand.RaiseExecuteChanged();
                UpdateCommand.RaiseExecuteChanged();
            }
        }
        public string Amount { get { return m_Amount; }
            set {
                m_Amount = value;
                AddCommand.RaiseExecuteChanged();
                UpdateCommand.RaiseExecuteChanged();
            }
        }

        public Visibility ShowAddButton { get { return ViewHelpers.GetVisibility(UniqueExpenseItemId == NEW_REGULAR_ITEM_ID); } }
        public Visibility ShowUpdateButton { get { return ViewHelpers.GetVisibility(UniqueExpenseItemId != NEW_REGULAR_ITEM_ID); } }

        private string m_Name;
        private string m_Amount;

        private bool m_ShowAddButton;
        private bool m_IsIncome;


        public const int NEW_REGULAR_ITEM_ID = -1;  // use it to add new value


        public EditUniqueItemVM()
        {

        }


        // TODO: use database ID for edit
        public EditUniqueItemVM(bool _IsAdd, int _ItemId)   // _RegularItemId could be NEW_REGULAR_ITEM_ID
        {
            m_ShowAddButton = _IsAdd;

            if (_ItemId != NEW_REGULAR_ITEM_ID)
            {
                UniqueExpenseItemId = _ItemId;

                UniqueExpensesStorage db = new UniqueExpensesStorage();
                UniqueExpenseItem ri = db.GetUniqueExpense(_ItemId);

                this.Date = ri.Date.ToLocalTime();
                this.Name = ri.Name;
                int am = ri.Amount;
                m_IsIncome = am > 0; // save "income" or "expense"
                this.Amount = DataConversion.ConvertCentsToCurrency(Math.Abs(am)).ToString();    // display abs amount
            }
            else
            {
                UniqueExpenseItemId = NEW_REGULAR_ITEM_ID;

                Name = "";
                Amount = "";
                Date = DateTimeOffset.Now.Date;
            }

            UpdateButtonColor = m_IsIncome ? "ForestGreen" : "DarkRed";

            UpdateButtonText = m_IsIncome ? "Update Income" : "Update Expense";

        }




        public DelegateCommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new DelegateCommand( 
                        p => {

                            UniqueExpensesStorage db = new UniqueExpensesStorage();

                            int amount = DataConversion.ConvertCurrencyStringToIntegerCents(Amount);   // positive

                            // p='i' or 'e'
                            if ((string)p == "i")
                            {
                                // income
                                // positive
                            }
                            else
                            {
                                // expense
                                amount = -amount;   // negative
                            }

                            UniqueExpenseItem uei = new UniqueExpenseItem(NEW_REGULAR_ITEM_ID, Date.DateTime, amount, Name);
                            db.InsertUniqueExpense(uei);

                            BackSpaceInvoked();

                        }, 
                        p => ((Name.Length > 0) && ( (Amount.Length > 0) && DataConversion.CanConvertCurrencyStringToIntegerCents(Amount) ))
                    );
                }
                return addCommand;
            }
        }




        public DelegateCommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                {
                    updateCommand = new DelegateCommand(
                        p => {
                            UniqueExpensesStorage db = new UniqueExpensesStorage();

                            int amount = DataConversion.ConvertCurrencyStringToIntegerCents(Amount);
                            if (m_IsIncome == false)
                            {
                                amount = -amount;   // expense is negative
                            }

                            UniqueExpenseItem ri = new UniqueExpenseItem(UniqueExpenseItemId, Date.DateTime, amount, Name);

                            // update
                            db.UpdateUniqueExpense(ri);

                            BackSpaceInvoked();
                        },
                        p => ((Name.Length > 0) && ((Amount.Length > 0) && DataConversion.CanConvertCurrencyStringToIntegerCents(Amount)))
                    );
                }
                return updateCommand;
            }
        }




        public DelegateCommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new DelegateCommand(
                        p => {

                            UniqueExpensesStorage db = new UniqueExpensesStorage();

                            if (UniqueExpenseItemId != NEW_REGULAR_ITEM_ID)
                            {
                                // update
                                db.DeleteUniqueExpense(UniqueExpenseItemId);
                            }

                            BackSpaceInvoked();

                        },
                        p => (UniqueExpenseItemId != NEW_REGULAR_ITEM_ID)
                    );
                }
                return deleteCommand;
            }
        }





        private void BackSpaceInvoked()
        {
            App.NavigationService.GoBack();
        }



        private DelegateCommand addCommand;
        private DelegateCommand updateCommand;
        private DelegateCommand deleteCommand;


    }
}
