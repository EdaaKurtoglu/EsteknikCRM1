using System.ComponentModel;

namespace EsteknikCRM.Entities
{
    public class Device
    {
        private string _id;
        private string _serialNumber;
        private string _deviceCode;
        private string _deviceName;
        private DateTime? _commissionDate;
        private string? _brand;
        private string? _topGroup;
        private string? _subGroup;
        private string? _specialGroup;
        private string? _status;

        public string Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string SerialNumber
        {
            get => _serialNumber;
            set { _serialNumber = value; OnPropertyChanged(nameof(SerialNumber)); }
        }

        public string DeviceCode
        {
            get => _deviceCode;
            set { _deviceCode = value; OnPropertyChanged(nameof(DeviceCode)); }
        }

        public string DeviceName
        {
            get => _deviceName;
            set { _deviceName = value; OnPropertyChanged(nameof(DeviceName)); }
        }

        public DateTime? CommissionDate
        {
            get => _commissionDate;
            set { _commissionDate = value; OnPropertyChanged(nameof(CommissionDate)); OnPropertyChanged(nameof(CommissionDateFormatted)); }
        }

        // DataGrid’de string tarih göstermek için
        public string CommissionDateFormatted =>
            CommissionDate.HasValue ? CommissionDate.Value.ToString("dd/MM/yyyy") : "";

        public string Brand
        {
            get => _brand;
            set { _brand = value; OnPropertyChanged(nameof(Brand)); }
        }

        public string TopGroup
        {
            get => _topGroup;
            set { _topGroup = value; OnPropertyChanged(nameof(TopGroup)); }
        }

        public string SubGroup
        {
            get => _subGroup;
            set { _subGroup = value; OnPropertyChanged(nameof(SubGroup)); }
        }

        public string SpecialGroup
        {
            get => _specialGroup;
            set { _specialGroup = value; OnPropertyChanged(nameof(SpecialGroup)); }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }

        #endregion
    }

}
