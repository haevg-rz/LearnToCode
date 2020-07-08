using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GUI.Helper;
using GUI.Message;
using GUI.Model;

namespace GUI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Config currentConfig;

        public Config CurrentConfig
        {
            get => this.currentConfig;
            set => base.Set(ref this.currentConfig, value);
        }

        public RelayCommand ShowConfigCommand { get; set; }

        public MainViewModel()
        {
            this.ShowConfigCommand = new RelayCommand(this.ShowConfigCommandExecute);

            Messenger.Default.Register<Config>(this, this.ReceiveData);
        }

        private void ReceiveData(Config obj)
        {
            if (obj == null)
                return;
            this.CurrentConfig = obj;
        }

        //private void ReceiveData<MyGenericMessage<Config>>(MyGenericMessage<Config> obj)
        //{
        //    var m = obj as MyGenericMessage<Config>;
        //    if (m == null)
        //        return;
        //    var c = m.Content;
        //}

        private void ShowConfigCommandExecute()
        {
            WindowManager.OpenConfigWindow();
            // daten senden!
            Messenger.Default.Send<SendDefaultDataMessage>(new SendDefaultDataMessage(new Config()
            {
                DataPath = @"C:\Windows\System32",
                IsEnabled = true,
            }));
        }
    }
}
