using AccountsLibrary;
using AccountsPro.MVVM.View.PropertyManagement;
using AccountsPRO.Core;
using AccountsPRO.MVVM.View;
using AccountsPRO.MVVM.View.Accounts;
//using AccountsPRO.MVVM.View.Home;

namespace AccountsPRO.MVVM.ViewModel
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
        public RelayCommand UploadTenantsCommand { get; set; }

        //property management
        public RelayCommand PropertyManagementCommand { get; set; }
        public RelayCommand ArrearsAgingAnalysisCommand { get; set; }
        public RelayCommand PrepaymentTrackingCommand { get; set; }
        public RelayCommand UtilityCostAllocationCommand { get; set; }
        public RelayCommand HistoricalBalanceCommand { get; set; }

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
            ReceiptView receiptView,
            UploadTenantsUserControl uploadTenantsUserControl,
            //propertymanagement
            PropertyManagement propertyManagement,
            ArrearsAgingAnalysis arrearsAgingAnalysis,
            PrepaymentTracking prepaymentTracking,
            UtilityCostAllocation utilityCostAllocation,
            HistoricalBalance historicalBalance

            )
        {

            CurrentView = uploadTenantsUserControl;


            DefaultAccountScreenCommand = new RelayCommand(o => { CurrentView = defaultAccountScreenView; });

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

            UploadTenantsCommand = new RelayCommand(o => { CurrentView = uploadTenantsUserControl; });

            //propertymanagement
            PropertyManagementCommand = new RelayCommand(o => { CurrentView = propertyManagement; });

            ArrearsAgingAnalysisCommand = new RelayCommand(o => { CurrentView = arrearsAgingAnalysis; });
            
            PrepaymentTrackingCommand = new RelayCommand(o => { CurrentView = prepaymentTracking; });

            UtilityCostAllocationCommand = new RelayCommand(o => { CurrentView = utilityCostAllocation; });

            HistoricalBalanceCommand = new RelayCommand(o => { CurrentView = historicalBalance; });

        }
    }
}