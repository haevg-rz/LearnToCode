using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace MVVM_Binding
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            this.GoCommand = new Command(() => Content3TextBlock = Content3);
        }
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

        public string Content3 { get; set; } = "Knock Knock";
        public string Content3TextBlock { get; set; } = "How is there?";

        public ICommand GoCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Command : ICommand
    {
        private Action action;

        public Command(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.action?.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
