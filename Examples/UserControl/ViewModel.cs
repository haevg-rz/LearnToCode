using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace UserControl
{
    public class ViewModel : ViewModelBase
    {
        private readonly DispatcherTimer timer;

        public ViewModel()
        {
            this.Contacts = new ObservableCollection<IContact> {new Employee(), new Customer()};

            this.StartCommand = new RelayCommand(this.StartCommandHandling, () => !this.timer.IsEnabled);
            this.StopCommand = new RelayCommand(this.StopCommandHandling, () => this.timer.IsEnabled);
            this.AddCommand = new RelayCommand<Type>(this.AddCommandHandling);
            this.ClearCommand = new RelayCommand(this.ClearCommandHandling, () => this.Contacts.Any());

            this.timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.5)
            };
            this.timer.Tick += this.Timer_Tick;
        }

        public ObservableCollection<IContact> Contacts { get; set; }

        public RelayCommand StartCommand { get; set; }
        public RelayCommand StopCommand { get; set; }
        public RelayCommand<Type> AddCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }

        private void ClearCommandHandling()
        {
            this.Contacts.Clear();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.Contacts.Any() && Guid.NewGuid().GetHashCode() % 3 == 0)
                this.Contacts.RemoveAt(new Random().Next(0, this.Contacts.Count));

            if (Guid.NewGuid().GetHashCode() % 2 == 0)
                this.Contacts.Add(new Employee());
            else
                this.Contacts.Add(new Customer());

            foreach (var employee in this.Contacts.Where(contact => contact is Employee).Cast<Employee>())
            {
                employee.Salery += new Random().Next(0, (int) (employee.Salery * 0.1));
            }

            foreach (var employee in this.Contacts.Where(contact => contact is Customer).Cast<Customer>())
            {
                employee.LastOrder = employee.LastOrder.AddDays(new Random().Next(0, 14));
            }
        }

        private void AddCommandHandling(Type type)
        {
            if (type == typeof(Employee))
                this.Contacts.Add(new Employee());
            else if (type == typeof(Customer)) this.Contacts.Add(new Customer());
        }

        private void StopCommandHandling()
        {
            this.timer.Stop();
        }

        private void StartCommandHandling()
        {
            this.timer.Start();
        }
    }


    public interface IContact
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class Customer : ContactBase, IContact
    {
        public DateTime LastOrder { get; set; }
    }

    public class Employee : ContactBase, IContact
    {
        public double Salery { get; set; }
    }

    public class ContactBase
    {
        public ContactBase()
        {
            this.Name = Helper.GetRandomName();
            this.BirthDate = DateTime.Now.AddYears(-80).AddYears(new Random().Next(0, 60)).AddDays(new Random().Next(0, 360)).Date;
        }

        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}