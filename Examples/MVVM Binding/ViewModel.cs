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
            this.AddCommand = new Command(() =>
            {
                Items.Add(new Item() {Value = NewItem});
                Items = new List<Item>(Items);
                
                OnPropertyChanged(nameof(Items));
            });

            this.Items = new List<Item>()
            {
                new Item(){Value = "Erster!"},
                new Item(){Value = "Zweiter!"},
            };
        }
        public string Content1 { get; set; } = "Hallo";

        private string content2;
        private string content3TextBlock = "How is there?";
        private List<Item> items;

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

        public string Content3TextBlock
        {
            get => content3TextBlock;
            set
            {
                content3TextBlock = value;
                OnPropertyChanged(nameof(Content3TextBlock));
            }
        }

        public string NewItem { get; set; }

        public ICommand GoCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public List<Item> Items
        {
            get => items;
            set => items = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Item
    {
        public string Value { get; set; }
        public string Timestamp { get; set; } = DateTime.Now.ToString();

        public override string ToString()
        {
            return "MyToString: " + Value + ", " + Timestamp;
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
