using System;
using System.ComponentModel;

namespace FlexMold.MVVM.Model
{
    public class DebitNote : INotifyPropertyChanged
    {
        private bool _sl;
        private string _voucherNo;
        private DateTime _date;
        private string _partyAccount;
        private string _supplierName;
        private decimal _amount;
        private string _narration;

        public bool SL
        {
            get => _sl;
            set { _sl = value; OnPropertyChanged(nameof(SL)); }
        }

        public string VoucherNo
        {
            get => _voucherNo;
            set { _voucherNo = value; OnPropertyChanged(nameof(VoucherNo)); }
        }

        public DateTime Date
        {
            get => _date;
            set { _date = value; OnPropertyChanged(nameof(Date)); }
        }

        public string PartyAccount
        {
            get => _partyAccount;
            set { _partyAccount = value; OnPropertyChanged(nameof(PartyAccount)); }
        }

        public string SupplierName
        {
            get => _supplierName;
            set { _supplierName = value; OnPropertyChanged(nameof(SupplierName)); }
        }

        public decimal Amount
        {
            get => _amount;
            set { _amount = value; OnPropertyChanged(nameof(Amount)); }
        }

        public string Narration
        {
            get => _narration;
            set { _narration = value; OnPropertyChanged(nameof(Narration)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
