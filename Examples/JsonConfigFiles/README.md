# Using JSON configuration files

## A typical configuration file

This is what a typical JSON configuration file might look like, depending on your needs:
```json
{
	"apikey": "fje0wanb0fnswofnsiod",
	"sessionid": 768,
	"users": [
		{
			"name": "Hood",
			"firstname": "Robin",
			"email": "robin_hood@stealfromtherich.cc",
			"job": "IT Security Expert"
		},
		{
			"name": "Davis",
			"firstname": "Mike",
			"email": "mikedavis@domain.com",
			"job": "Designer"
		},
		{
			"name": "Doe",
			"firstname": "Leo",
			"email": "leo@doe.com",
			"job": "Manager"
		}
    ]
}
```

C# is an object-oriented programming language, hence you need class models to reflect the structure of your JSON file.

## Models

For our example above, we need two different models, e.g. `Settings` and `User`.

```csharp
public class User
{
    public string Name { get; set; }
    public string Firstname { get; set; }
    public string Email { get; set; }
    public string Job { get; set; }
}
```

and

```csharp
public class Settings
{
    public string ApiKey { get; set; }
    public int SessionId { get; set; }
    public List<User> Users { get; set; }
}
```

Notice that we use a `List<User>`. This is necessary, since there are multp0le users in our JSON file. Also, the JSON keys must match your class properties *exactly*, but they are not case-sensitive.

In the next step, please install the `Newtonsoft.Json` NuGet package. This package provides two easy-to-use static methods to serialize objects into JSON string *and* unserialize JSON strings (which JSON files are, essentially) into objects.

### Deserialize example

```csharp
Settings settings = JsonConvert.DeserializeObject<Settings>(jsonString);
```

This will take he contents of the JSON file and deserialize it into a `Settings` object which you can work with.

### Serialize example

```csharp
string jsonString = JsonConvert.SerializeObject(Settings);
```

Here, you serialize the object Settings (naturally of type `Settings`) back into a JSON string which you can then store in a file.


## A typical safe spot for your configuration files

On Windows, there is a special folder where applications can save user-specific configuration.
This is the so-called AppData folder. You can find it here: `C:\Users\<YourUsername>\AppData\Roaming`.
If you open it, you can usually find many subfolders with the names of applications. They contain the configuration files of these applications.

Being a proper developer yourself, you create a folder for your cool application, in this example it is `JsonConfigFiles`, because that is the name of the example project.

There are special constans ready-to-use in C#. For the AppData folder, do this:

```csharp
string appDatapath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
```

If your Windows username is MyUser, then the variable `appDatapath` will contain `C:\Users\MyUser\AppData\Roaming`.

If you have questions, please don't hesitate to open an issue! Only then can we improve our LearnToCode repository.