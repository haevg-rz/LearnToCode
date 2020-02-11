# Linq

> We are using the **method-based query syntax**, not the query expression syntax!

## Introduction

When we are speaking about LINQ we mean a set of technologies based on the integration of query capabilities directly into the C# language.

By using query syntax, you can perform filtering, ordering, and grouping operations on data sources with a minimum of code. You use the same basic query expression patterns to query and transform data in SQL databases, ADO .NET Datasets, XML documents and streams, and .NET collections.

In the namespace System.Linq you find a lot [extensions methods](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods) which add query functionality to the existing [System.Collections.IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable).

For example: [/System/Linq/Sum.cs](https://github.com/dotnet/runtime/blob/71d2911c64058434a5db6b324c7efead93dfde4c/src/libraries/System.Linq/src/System/Linq/Sum.cs#L11)

```csharp
public static int Sum(this IEnumerable<int> source)
{
    if (source == null)
    {
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
    }

    int sum = 0;
    checked
    {
        foreach (int v in source)
        {
            sum += v;
        }
    }

    return sum;
}
```




## More information to Linq

- [Standard Query Operators Overview](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/standard-query-operators-overview)
- [Lambda expressions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/lambda-expressions)
- [Enumerable Class](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=netcore-3.1)

