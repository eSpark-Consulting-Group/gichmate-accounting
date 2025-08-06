using FlexMold.Core;

namespace FlexMold.MVVM.ViewModel
{
    public interface IAccountingViewModel
    {
        object CurrentView { get; set; }
       
        public RelayCommand DefaultAccountScreenCommand { get; set; }
        public RelayCommand BankPaymentCommand { get; set; }
        public RelayCommand BankReceiptCommand { get; set; }
        public RelayCommand BankReconciliationCommand { get; set; }
        public RelayCommand CashPaymentCommand { get; set; }
        public RelayCommand CashReceiptCommand { get; set; }
        public RelayCommand CreditNoteCommand { get; set; }
        public RelayCommand DebitNoteCommand { get; set; }
        public RelayCommand PaymentAndReceiptCommand { get; set; }
        public RelayCommand PostDatedChequePaymentCommand { get; set; }
        public RelayCommand PostDatedChequeReceiptCommand { get; set; }
        public RelayCommand ReceiptCommand { get; set; }
    }
}