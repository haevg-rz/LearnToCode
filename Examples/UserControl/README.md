# UserControl

This example solution covers the usage of `UserControl`s.  

> Controls in Windows Presentation Foundation (WPF) support rich content, styles, triggers, and templates. In many cases, these features allow you to create custom and consistent experiences without having to create a new control. For more information, see [Styling and Templating](https://docs.microsoft.com/en-us/dotnet/desktop-wpf/fundamentals/styles-templates-overview?view=netframework-4.8).
> If you do need to create a new control, the simplest way is to create a class that derives from **UserControl**.

For more Information go to [UserControl Class](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.usercontrol?view=netframework-4.8).

## Intro

This solution contains a simple WPF .NET Core application with a MVVM-Pattern.

The View `MainWindow.xaml` contains a few UserControl from basic to a little more advanced.

Just start the application with `dotnet run` or with your IDE and notice the UserControls highlighted in beige.

In your IDE you see in every view design data.

## Topics

- [UserControl](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.usercontrol?view=netframework-4.8)
- [Dependency properties](https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/dependency-properties-overview)
- [Data binding](https://docs.microsoft.com/en-us/dotnet/desktop-wpf/data/data-binding-overview)
- [Sample data](https://docs.microsoft.com/en-us/windows/uwp/data-binding/displaying-data-in-the-designer)

## Explain the code

### App.xaml

A `ViewModelLocator` and a `DesignData.xaml` is added the `ResourceDictionary`.

```xml
<Application [...] StartupUri="MainWindow.xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary>
                    <local:ViewModelLocator x:Key="ViewModelLocator" />
                </ResourceDictionary>
                <ResourceDictionary Source="UserControls/DesignData/DesignData.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

### ContactUserControl.xaml

A very trivial UserControl with DesignData from the StaticResource DesignDataEmployee.

```xml
<UserControl [...] Background="Beige">

    <Border BorderBrush="Gray" BorderThickness="0.5" Margin="5" Padding="2.5"
            d:DataContext="{StaticResource DesignDataEmployee}">
        <StackPanel>
            <TextBlock Text="{Binding Name}" />
            <TextBlock Text="{Binding BirthDate}" />
        </StackPanel>
    </Border>
</UserControl>
```

### WrapPanelUserControl.xaml

A more complex example of a UserControl, see the dependency property in the code behind.

```xml
<UserControl [...]>
    <ItemsControl ItemsSource="{Binding WrapItems}"
                  DataContext="{Binding RelativeSource={RelativeSource Mode=FindAncestor, AncestorType=UserControl}}"
                  d:DataContext="{StaticResource DesignDataContacts}">
        <ItemsControl.ItemsPanel>
            <ItemsPanelTemplate>
                <WrapPanel />
            </ItemsPanelTemplate>
        </ItemsControl.ItemsPanel>

        <ItemsControl.Resources>
            <DataTemplate DataType="{x:Type userControl:Employee}">
                <Border BorderBrush="Blue" BorderThickness="0.5" Margin="5" Padding="5">
                    <StackPanel>
                        <TextBlock Text="{Binding Name, StringFormat=Employee: {0}}" />
                        <TextBlock Text="{Binding Salery, StringFormat=Salery: {0:N0}€}" />
                    </StackPanel>
                </Border>
            </DataTemplate>
            <DataTemplate DataType="{x:Type userControl:Customer}">
                <Border BorderBrush="Red" BorderThickness="0.5" Margin="5" Padding="5">
                    <StackPanel>
                        <TextBlock Text="{Binding Name, StringFormat=Customer: {0}}" />
                        <TextBlock Text="{Binding LastOrder, StringFormat=LastOrder: {0:dd.MM.yyyy}}" />
                    </StackPanel>
                </Border>
            </DataTemplate>
        </ItemsControl.Resources>

    </ItemsControl>
</UserControl>
```

```csharp
public partial class WrapPanelUserControl : System.Windows.Controls.UserControl, IWrapPanelUserControlProperties
{
    public static readonly DependencyProperty WrapItemsProperty = DependencyProperty.Register(
        "WrapItems", typeof(IEnumerable<IContact>), typeof(WrapPanelUserControl), new PropertyMetadata(default(IEnumerable<IContact>)));

    public WrapPanelUserControl()
    {
        this.InitializeComponent();
    }

    public IEnumerable<IContact> WrapItems
    {
        get => (IEnumerable<IContact>) this.GetValue(WrapItemsProperty);
        set => this.SetValue(WrapItemsProperty, value);
    }
}
```
