# WPF applications using multiple Windows and ViewModels

When you move beyond simple WPF apps you will ask yourself how to handle multiple windows. Here is how you __can__ do it.

## Part I: The Window Manager

To open a new windows programmatically, you could just do this:

    var w = new MyWindow();
    w.Show();


But there is a problem. You cannot close the window from another viewmodel.
Here is a simple solution: create WindowManager class.

    public static class WindowManager
    {
        private static MyWindow mywindow;

        public static void OpenMyWindow()
        {
            myWindow = new MyWindow();
            myWindow.Show();
        }

        public static void CloseMyWindow()
        {
            if (myWindow != null)
                myWindow.Close();
        }
    }

Utilizing the WindowManager, you can have an __Open__ and a __Close__ method for every window you want to implement.
You can open a certain window from *Viewmodel A* and close it from *Viewmodel B* without having to know about the actual window implementations.

## Part II: The Messenger

Your TextBlocks, TextBoxes and other elements in your window are bound to the properties of the respective viewmodel. Now we will make your viewmodels communicate among each other with the help of the **Messenger Pattern**, also known as the **Publisher/Subscriber (or PubSub) Pattern**.
Basically, viewmodels register themselves with a method for messages of certain types. If a message (from a different viewmodel) is sent, the registered method will get executed. 

If you are using MvvmLight, you can use the `Messenger.Default` Instance, as shown below.

To be able to successfully "send" messages, a registrant is required. That means the Register() method should be called in the viewmodel's constructor. The Send() method can be called wherever you want.

In this example, we have MainViewModel and a ConfigViewModel. Let's say you changed the configuration in the ConfigWindow and you want to display the entered values
in the MainWindow. That means the ConfigViewModel has to send its data to the MainViewModel. We use a `Config` class with some simple properties here:

    public class Config
    {
        public string DataPath { get; set; }
        public bool IsEnabled { get; set; }
    }

 The MainViewModel registers in the constructor for messages of type `Config` and the ConfigViewModel then sends out messages of type `Config`.

 MainViewModel Constructor:

    public MainViewModel()
    {
        // other stuff ...

        Messenger.Default.Register<Config>(this, this.ReceiveData);
    }

Here, `ReceiveData` is a method with the signature `private void ReceiveData(Config obj)`, according to the type of message you registered for.
Anywhere in another viewmodel, you can send messages by doing the following:

    private void SendDataCommandExecute()
    {
        Messenger.Default.Send(new Config()
        {
            DataPath = this.DataPathValue,
            IsEnabled = this.IsEnabledValue,
        });

        // reset data input fields
        this.DataPathValue = string.Empty;
        this.IsEnabledValue = false;

        WindowManager.CloseConfigWindow();
    }

What you are doing here is to take the values from the bound properties, put it into a new `Config` object and send it to the MainViewModel. Easy as that!

## Advanced stuff

A custom Messenger class

    public class Messenger
    {
        private static Dictionary<Type, Action<object>> registrants = 
            new Dictionary<Type, Action<object>>();

        public static void Register<T>(Action<object> act) where T : class
        {
            registrants.Add(typeof(T), act);
        }

        public static void Send<T>(T obj) where T : class
        {
            foreach (var item in registrants)
            {
                if (item.Key == typeof(T))
                {
                    item.Value(obj);
                    //break; // remove this to allow multiple recipients
                }
            }
        }
    }
