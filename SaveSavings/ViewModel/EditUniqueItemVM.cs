
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
        public EditUniqueItemVM(bool _IsAdd, int _UniqueItemId)   // _RegularItemId could be NEW_REGULAR_ITEM_ID
        {
            m_ShowAddButton = _IsAdd;

            if (_UniqueItemId != RegularItemVM.NEW_REGULAR_ITEM_ID)
            {
                UniqueExpenseItemId = _UniqueItemId;

                //RegularStorage db = new RegularStorage();
                //RegularItem ri = db.GetRegular(_UniqueItemId);

                // TODO: use DB here
                this.Date = DateTimeOffset.Now.Date;
                this.Name = "fhdsjk fds";//ri.Name;
                int am = -486;
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
                if (editCommand == null)
                {
                    editCommand = new DelegateCommand( 
                        p => {

                            // p='i' or 'e'

                            //RegularStorage db = new RegularStorage();

                            //int amount = DataConversion.ConvertCurrencyStringToIntegerCents(Amount);
                            //if (m_IsIncome == false)
                            //{
                            //    amount = -amount;   // expense is negative
                            //}

                            //REGULARS_PERIOD period = GetPeriod();

                            //RegularItem ri = new RegularItem(RegularItemId, Name, period, amount);

                            //if (RegularItemId != RegularItemVM.NEW_REGULAR_ITEM_ID)
                            //{
                            //    // update
                            //    db.UpdateRegular(ri);
                            //}
                            //else
                            //{
                            //    // add
                            //    db.InsertRegular(ri);
                            //}

                            //BackSpaceInvoked();

                        } , 
                        p => ((Name.Length > 0) && ( (Amount.Length > 0) && DataConversion.CanConvertCurrencyStringToIntegerCents(Amount) ))
                    );
                }
                return editCommand;
            }
        }




        public DelegateCommand UpdateCommand
        {
            get
            {
                if (editCommand == null)
                {
                    editCommand = new DelegateCommand(
                        p => {

                            // m_IsIncome is the value

                            //RegularStorage db = new RegularStorage();

                            //int amount = DataConversion.ConvertCurrencyStringToIntegerCents(Amount);
                            //if (m_IsIncome == false)
                            //{
                            //    amount = -amount;   // expense is negative
                            //}

                            //REGULARS_PERIOD period = GetPeriod();

                            //RegularItem ri = new RegularItem(RegularItemId, Name, period, amount);

                            //if (RegularItemId != RegularItemVM.NEW_REGULAR_ITEM_ID)
                            //{
                            //    // update
                            //    db.UpdateRegular(ri);
                            //}
                            //else
                            //{
                            //    // add
                            //    db.InsertRegular(ri);
                            //}

                            //BackSpaceInvoked();

                        },
                        p => ((Name.Length > 0) && ((Amount.Length > 0) && DataConversion.CanConvertCurrencyStringToIntegerCents(Amount)))
                    );
                }
                return editCommand;
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

                            //RegularStorage db = new RegularStorage();

                            //if (RegularItemId != RegularItemVM.NEW_REGULAR_ITEM_ID)
                            //{
                            //    // update
                            //    db.DeleteRegular(RegularItemId);
                            //}

                            //BackSpaceInvoked();

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



        private DelegateCommand editCommand;
        private DelegateCommand deleteCommand;


    }
}
