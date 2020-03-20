using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MVVM_Binding
{
    public class ViewModel : INotifyPropertyChanged
    {
        
        public string Content1 { get; set; } = "Hallo";

        private string content2;
        public string Content2
        {
            get => content2;
            set
            {
                content2 = value;
                OnPropertyChanged(nameof(Content2TextBlock));
            }
        }

        public string Content2TextBlock
        {
            get => "#: "+ Content2;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
