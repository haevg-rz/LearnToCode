using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Car> Cars { get; set; }
		
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Cars = (new Car()).GetDesignData();
        }
    }
    
    public class Car
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int PS { get; set; }

        public ObservableCollection<Car> GetDesignData()
        {
            return new ObservableCollection<Car>()
            {
                new Car() {Manufacturer = "BMW", Model = "i3", PS = 220},
                new Car() {Manufacturer = "Audi", Model = "Q7", PS = 260},
                new Car() {Manufacturer = "VW", Model = "Polo", PS = 65}
            };
        }
    }
}
