# MVVM Beginner Level

![Screenshot](assets/Screenshot.png)

## Intro

We are creating an WPF App with the MVVM Pattern. In this  beginner level example we are **not using any third party libs**.
This App contains only a `TextBox`, a `Button` and a `TextBlock`.

When you are pressing `Button` the `TextBlock` will contain the value of the Binding `TextBox`.

First you should have an basic understanding of WPF and MVVM.

- [Microsoft Docs - WPF overview](https://docs.microsoft.com/en-us/dotnet/framework/wpf/introduction-to-wpf)
- [Microsoft Docs - Understanding the basics of MVVM design pattern](https://docs.microsoft.com/en-us/archive/blogs/msgulfcommunity/understanding-the-basics-of-mvvm-design-pattern)
- [Wikipedia on Model–view–viewmodel](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel)
- [HANSELMINUTES - The MVVM Pattern with Laurent Bugnion](https://hanselminutes.com/241/the-mvvm-pattern-with-laurent-bugnion)

## Code

### App.xaml

> The Application object is typically provided in the initial XAML for App.xaml. The default project templates in Visual Studio generate an App class that derives from Application and provides an entry point where you can add initialization code.
> see [docs.microsoft.com](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.application)

> `Application.StartupUri`: a Uri that refers to the UI that automatically opens when an application starts.

```xml
StartupUri="MainWindow.xaml"
```

> A ResourceDictionary object to declare an app-specific resources. The resources you define in the ResourceDictionary that fills the Application.Resources property element are available for retrieval from any page of your app.
> see [docs.microsoft.com](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.application.resources#Windows_UI_Xaml_Application_Resources)

```xml
<Application.Resources>
    <local:ViewModel x:Key="ViewModel" />
</Application.Resources>
```

### MainWindows.xaml

> `FrameworkElement.DataContext`: In this example sets the DataContext directly to an instance of a custom class defined in `Application.Resources`.
> see [docs.microsoft.com](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.frameworkelement.datacontext)

```xml
DataContext="{StaticResource ViewModel}"
```

> `TextBox`: Represents a control that can be used to display and edit plain text (single or multi-line).  
> `Button`: Represents a templated button control that interprets a Click user interaction.  
> `ButtonBase.Command`: Gets or sets the command to invoke when this button is pressed.  
> `TextBlock`: Provides a lightweight control for displaying small amounts of text.

```xml
<TextBox Margin="10" Text="{Binding Name}" />
<Button Margin="10" Content="do greet" Command="{Binding MyCommand}" />
<TextBlock Margin="10" Text="{Binding Greeting}" />
```

### ViewModel.cs

> `INotifyPropertyChanged`: Notifies clients that a property value has changed.

```csharp
public class ViewModel : INotifyPropertyChanged
```

> Property Name of type string.

```csharp
public string Name { get; set; }
```

> Property Greeting of type string with an standard value and `OnPropertyChanged` in the setter.

```csharp
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
```

> `ICommand`: Defines the command behavior of an interactive UI element that performs an action when invoked, such as sending an email, deleting an item, or submitting a form.

> This MyCommand will execute the action which is defined as: `this.Greeting = "Hallo " + this.Name`

```csharp
public ICommand MyCommand { get; set; }

public ViewModel()
{
    MyCommand = new Command(() => this.Greeting = "Hallo " + this.Name);
}
```
