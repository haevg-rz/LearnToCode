# Template Switching

## Prerequisites or "What you need":

* MainWindow.xaml
* MainWindow.xaml.cs

### Define a custom class (can be whatever you want, just to have something to display)

```csharp
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
```

Generally, when designing a new application, being able to fetch Design Data is good practice, since it can be used to show how the app will look like when it is running, but in design mode.

### Define an ObservableCollection, set the DataContext to the MainWindow Class

```csharp
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
```

### Now, in your MainWindow.xaml, define two different DataTemplates as static resources. Make sure you can visually distinguish them.

```xml
<Window.Resources>
    <DataTemplate x:Key="DefaultTemplate">
        <StackPanel Orientation="Horizontal">
            <TextBlock Text="{Binding Manufacturer}" FontSize="16"></TextBlock>
            <TextBlock Text="{Binding Model}" Margin="10"></TextBlock>
            <TextBlock Text="{Binding PS}" Foreground="YellowGreen"></TextBlock>
        </StackPanel>
    </DataTemplate>
    <DataTemplate x:Key="AlternativeTemplate">
        <StackPanel Orientation="Horizontal">
            <TextBlock Text="{Binding Model}"></TextBlock>
        </StackPanel>
    </DataTemplate>
</Window.Resources>
```

### Next, define an element you can bind an ObservableCollection to, e.g. a ListBox, DataGrid or ItemsControl. Here, we use a simple ListBox and bind our `Cars` public property to it's ItemsSource:

```xml
<ListBox Grid.Column="0" ItemsSource="{Binding Cars}">
    <ListBox.Style>
        <Style>
            <Style.Triggers>
                <DataTrigger Binding="{Binding ElementName=TemplateCheckBox, Path=IsChecked}" Value="true">
                    <Setter Property="ListBox.ItemTemplate" Value="{StaticResource AlternativeTemplate}"></Setter>
                </DataTrigger>
                <DataTrigger Binding="{Binding ElementName=TemplateCheckBox, Path=IsChecked}" Value="false">
                    <Setter Property="ListBox.ItemTemplate" Value="{StaticResource DefaultTemplate}"></Setter>
                </DataTrigger>
            </Style.Triggers>
        </Style>
    </ListBox.Style>
</ListBox>
```

### What you will also need is an element or property which determines a boolean state, that means whether the default or the alternative template should be displayed. In our example, we use a checkbox (don't forget to supply a name!):

```xml
<CheckBox Grid.Column="1" Name="TemplateCheckBox" Content="Switch to alternative Template" />
```

## The Default View

![The Default View](Images/DefaultView.png)

## The Alternative View

![The Default View](Images/AlternativeView.png)


## Explanation

We are binding two predefined `DataTemplate`s to our `ListBox`. Whether any of those will be displayed depends on the `IsChecked` property of the `CheckBox` named `TemplateCheckBox`. All we need for this is two DataTriggers under the `ListBox`' Style/Style.Triggers property.
Since the `CheckBox` cannot ever be checked *and* unchecked at the same time, only one of the `DataTemplate`s will ever be displayed. Hence, a simple template switch.

Hint: the full code can be found in the `Presentation` folder.