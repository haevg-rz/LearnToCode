using System;
using System.ComponentModel;
using System.Linq;

namespace CollectionViewSource
{
    public class ViewModel
    {
        public ViewModel()
        {
            var cars = Enumerable.Range(0, 100).Select(i => Car.CreateRandom());
            var collectionView = System.Windows.Data.CollectionViewSource.GetDefaultView(cars);
            collectionView.Filter = Filter;

            this.Cars = collectionView;
        }

        public ICollectionView Cars { get; set; }

        private bool Filter(object obj)
        {
            var car = obj as Car;
            if (car == null)
                return false;

            if (String.IsNullOrWhiteSpace(this.FilterValue))
                return true;

            if (car.Manufacturer.Contains(FilterValue, StringComparison.InvariantCultureIgnoreCase))
                return true;
            if (car.Model.Contains(FilterValue, StringComparison.InvariantCultureIgnoreCase))
                return true;
            if (car.Color.Contains(FilterValue, StringComparison.InvariantCultureIgnoreCase))
                return true;
            if (car.Wheels.ToString() == this.FilterValue)
                return true;

            return false;
        }

        private string filterValue;
        public string FilterValue
        {
            get => filterValue;
            set
            {
                filterValue = value;
                this.Cars.Refresh();
            }
        }
    }

    public class Car
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Wheels { get; set; }

        public static Car CreateRandom()
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            var colors = new[] {"Red", "Blue", "Green"};
            var model = new[] {"Roadster", "SUV"};
            var manufacturer = new[] {"Ford", "Tesla", "BMW"};
            var wheels = new[] {3, 4, 5};

            return new Car
            {
                Color = colors.OrderBy(s => rnd.Next()).First(),
                Wheels = wheels.OrderBy(s => rnd.Next()).First(),
                Model = model.OrderBy(s => rnd.Next()).First(),
                Manufacturer = manufacturer.OrderBy(s => rnd.Next()).First(),
            };
        }
    }
}