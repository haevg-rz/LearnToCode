# CollectionViewSource Example

## Introduction



## Screenshot

![Screenshot](Assets/Screenshot.png)

## Walk trough code

```xml
<Window.Resources>
    <local:ViewModel x:Key="ViewModel" />
</Window.Resources>
```

```xml
<Grid DataContext="{StaticResource ViewModel}">
```


```xml
<TextBox
    Grid.Row="0" Grid.Column="1"
    Margin="10"
    Text="{Binding FilterValue, UpdateSourceTrigger=PropertyChanged, Delay=100}" />
```

```xml
<DataGrid 
    Grid.Row="1" Grid.Column="0" Grid.ColumnSpan="2"
    Margin="10"
    ItemsSource="{Binding Cars}" AutoGenerateColumns="True" IsReadOnly="True" />
```

```csharp
public class Car
{
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public int Wheels { get; set; }
}
```

```csharp
var cars = Enumerable.Range(0, 100).Select(i => Car.CreateRandom());
var collectionView = System.Windows.Data.CollectionViewSource.GetDefaultView(cars);
collectionView.Filter = Filter;

this.Cars = collectionView;
```

```csharp
public string FilterValue
{
    get => filterValue;
    set
    {
        filterValue = value;
        this.Cars.Refresh();
    }
}
```

```csharp
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
```