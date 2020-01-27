# Static

> **static (C# Reference)**  
> Use the static modifier to declare a static member, which belongs to the type itself rather than to a specific object. The static modifier can be used to declare static classes. In classes, interfaces, and structs, you may add the static modifier to fields, methods, properties, operators, events, and constructors.
> [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/static](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/static)

> **Static Classes and Static Class Members (C# Programming Guide)**  
> A static class is basically the same as a non-static class, but there is one difference: a static class cannot be instantiated. In other words, you cannot use the new operator to create a variable of the class type. Because there is no instance variable, you access the members of a static class by using the class name itself.
> [https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members)

## Instance Member

```csharp
class Counter
{
    public int counter = 0;

    public void Add(int i)
    {
        counter += i;
    }
}
```

```csharp
var c1 = new Counter();
var c2 = new Counter();

c1.Add(5);
c2.Add(2);
c1.Add(1);

Console.Out.WriteLine("c1: " + c1.counter);
Console.Out.WriteLine("c2: " + c2.counter);
```

```plain
Output:
6
2
```


## Static Member

```csharp
class CounterStatic
{
    public int counter => counterInternal;

    public static int counterInternal = 0;

    public void Add(int i)
    {
        counterInternal += i;
    }
}
```

```csharp
CounterStatic.counterInternal = -1;

var c1 = new CounterStatic();
var c2 = new CounterStatic();

c1.Add(5);
c2.Add(2);
c1.Add(1);

Console.Out.WriteLine("c1: " + c1.counter);
Console.Out.WriteLine("c2: " + c2.counter);
```

```plain
Output:
7
7
```

## Create Instance only to access method

> There was the question, if .NET has instance methods for which I must create an instance only for the purpose to access this method.

```csharp
var passwd = "my secret passwd #1";
var input = Encoding.UTF8.GetBytes(passwd);

var hash = new SHA256Managed().ComputeHash(input);

Console.Out.WriteLine("input: " + passwd);
Console.Out.WriteLine("hash:  " + Convert.ToBase64String(hash));
```

## Possible Compiler Errors

**CS0120** C# An object reference is required for the non-static field, method, or property  
**CS0176** C# Member cannot be accessed with an instance reference; qualify it with a type name instead
