using FlexMold.Core;
using FlexMold.MVVM.View.Accounts;
using FlexMold.MVVM.View.Home;

namespace FlexMold.MVVM.ViewModel
{
    public class AccountingViewModel : ObservableObject, IAccountingViewModel
    {
        /// <summary>
        /// Relay Commands
        /// </summary>
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

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public AccountingViewModel(
            DefaultAccountScreenView defaultAccountScreenView,
            BankPaymentView bankPaymentView,
            BankReceiptView bankReceiptView,
            BankReconciliationView bankReconciliationView,
            CashPaymentView cashPaymentView,
            CashReceiptView cashReceiptView,
            CreditNoteView creditNoteView,
            DebitNoteView debitNoteView,
            PaymentAndReceiptView paymentAndReceiptView,
            PostDatedChequePaymentView postDatedChequePayment,
            PostDatedChequeReceiptView postDatedChequeReceipt,
            ReceiptView receiptView
            )
        {
            //set currentview from here!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            CurrentView = debitNoteView;



            BankPaymentCommand = new RelayCommand(o => { CurrentView = bankPaymentView; });

            BankReceiptCommand = new RelayCommand(o => { CurrentView = bankReceiptView; });

            BankReconciliationCommand = new RelayCommand(o => { CurrentView = bankReconciliationView; });

            CashPaymentCommand = new RelayCommand(o => { CurrentView = cashPaymentView; });

            CashReceiptCommand = new RelayCommand(o => { CurrentView = cashReceiptView; });

            CreditNoteCommand = new RelayCommand(o => { CurrentView = creditNoteView; });

            DebitNoteCommand = new RelayCommand(o => { CurrentView = debitNoteView; });
             
            PaymentAndReceiptCommand = new RelayCommand(o => { CurrentView = paymentAndReceiptView; });
            
            PostDatedChequePaymentCommand = new RelayCommand(o => { CurrentView = postDatedChequePayment; });

            PostDatedChequeReceiptCommand = new RelayCommand(o => { CurrentView = postDatedChequeReceipt; });

            ReceiptCommand = new RelayCommand(o => { CurrentView = receiptView; });


            //MachineDetailCommand = new RelayCommand(o => { CurrentView = machineDetailView; });
            //MotorStatusCommand = new RelayCommand(o => { CurrentView = motorStatusView; });
        }
    }
}