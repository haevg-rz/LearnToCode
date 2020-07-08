using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GUI.Helper;
using GUI.Message;
using GUI.Model;

namespace GUI.ViewModel
{
    public class ConfigViewModel : ViewModelBase
    {
        private string dataPathValue;
        private bool isEnabledValue;

        public string DataPathValue
        {
            get => this.dataPathValue;
            set => base.Set(ref this.dataPathValue, value);
        }

        public bool IsEnabledValue
        {
            get => this.isEnabledValue;
            set => base.Set(ref this.isEnabledValue, value);
        }

        public RelayCommand SendDataCommand { get; set; }

        public ConfigViewModel()
        {
            // hier senden
            this.SendDataCommand = new RelayCommand(this.SendDataCommandExecute);

            Messenger.Default.Register<SendDefaultDataMessage>(this, this.ReceiveData);
        }

        private void ReceiveData(SendDefaultDataMessage obj)
        {
            this.DataPathValue = obj.content.DataPath;
            this.IsEnabledValue = obj.content.IsEnabled;
        }

        private void SendDataCommandExecute()
        {
            Messenger.Default.Send(new Config()
            {
                DataPath = this.DataPathValue,
                IsEnabled = this.IsEnabledValue,
            });

            // reset data input fields
            this.DataPathValue = string.Empty;
            this.IsEnabledValue = false;

            WindowManager.CloseConfigWindow();
        }
    }
}
