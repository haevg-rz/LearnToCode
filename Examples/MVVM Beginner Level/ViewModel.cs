using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MVVM_Beginner_Level
{
    public class ViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }

        private string greeting = "No greeting";

        public string Greeting
        {
            get => this.greeting;
            set
            {
                this.greeting = value;
                this.OnPropertyChanged(nameof(this.Greeting));
            }
        }

        public ICommand MyCommand { get; set; }

        public ViewModel()
        {
            MyCommand = new Command(() => this.Greeting = "Hallo " + this.Name);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class Command : ICommand
    {
        private readonly Action action;

        public Command(Action action)
        {
            this.action = action;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.action?.Invoke();
        }

        #endregion
    }
}