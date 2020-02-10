# Mock

> In object-oriented programming, mock objects are simulated objects that mimic the behavior of real objects in controlled ways, most often as part of a software testing initiative. A programmer typically creates a mock object to test the behavior of some other object, in much the same way that a car designer uses a crash test dummy to simulate the dynamic behavior of a human in vehicle impacts. The technique is also applicable in generic programming. [https://en.wikipedia.org/wiki/Mock_object](https://en.wikipedia.org/wiki/Mock_object)

## Introduction

When we are writing unit tests it it desirable to test only our implementation and not e.g. the infrastructure or other systems.
Here is an example how to override implementation which would call such systems but will now behave as we need it for our unit test.

## Trivial Example

### Origin

The `DbReader` is a [facade](https://en.wikipedia.org/wiki/Facade_pattern) to call `DbAccess`, in this Impementation the property `DbAccess` can only be from type `DbAccess`.

```csharp
public class DbReader
{
    public DbAccess DbAccess { get; set; } = new DbAccess();

    public IEnumerable<Entity> GetLastEntities()
    {
        return DbAccess.GetLastEntities();
    }

    public string GetConnectionString()
    {
        return DbAccess.GetConnectionString();
    }
}

public class DbAccess
{
    public IEnumerable<Entity> GetLastEntities()
    {
        // Sql Commands etc.
        throw new NotImplementedException();
    }

    public string GetConnectionString()
    {
        return "PROD";
    }
}
```

### Make DbAccess interchangeable

We want to interchange DbAccess to simulate access to the database. For this purpose we are introducing the interface `IDbAccess`.

```csharp
public class DbReader
{
    public IDbAccess DbAccess { get; set; } = new DbAccess();

    public IEnumerable<Entity> GetLastEntities()
    {
        return DbAccess.GetLastEntities();
    }

    public string GetConnectionString()
    {
        return DbAccess.GetConnectionString();
    }
}

public interface IDbAccess
{
    IEnumerable<Entity> GetLastEntities();
    string GetConnectionString();
}

public class DbAccess : IDbAccess
{
    public IEnumerable<Entity> GetLastEntities()
    {
        // Sql Commands etc.
        throw new NotImplementedException();
    }

    public string GetConnectionString()
    {
        return "PROD";
    }
}
```

### Method 1: Create a implementation for testing

We can create our own implementation just for testing.

```csharp
public class DbAccessTest : IDbAccess
{
    public IEnumerable<Entity> GetLastEntities()
    {
        return Enumerable.Range(0, 100).Select(i => new Entity {Id = i});
    }

    public string GetConnectionString()
    {
        return "TEST";
    }
}
```

And set this implementation in our test context.

```csharp
DbReader dbReaderTest = new DbReader();
dbReaderTest.DbAccess = new DbAccessTest();
var lastEntriesTest = dbReaderTest.GetLastEntities();
```

### Method 2: Create and configure a mock

Or we can create and configure a mock, see [Moq](https://www.nuget.org/packages/Moq/).

```csharp
DbReader dbReaderTest = new DbReader();
var mock = new Moq.Mock<IDbAccess>();
mock.Setup(m => m.GetConnectionString()).Returns("TEST");
mock.Setup(m => m.GetLastEntities()).Returns(Enumerable.Range(0, 100).Select(i => new Entity {Id = i}));

dbReaderTest.DbAccess = mock.Object;
var lastEntriesTest = dbReaderTest.GetLastEntities();
```
