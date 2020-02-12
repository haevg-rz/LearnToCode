# Linq

> We are using the **method-based query syntax**, not the query expression syntax! See example below.

## Introduction

When we are speaking about LINQ we mean a set of technologies based on the integration of query capabilities directly into the C# language.

By using query syntax, you can perform filtering, ordering, and grouping operations on data sources with a minimum of code. You use the same basic query expression patterns to query and transform data in SQL databases, ADO .NET Datasets, XML documents and streams, and .NET collections.

In the namespace `System.Linq` you find a lot [extensions methods](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods) which add query functionality to the existing [System.Collections.IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable).

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

### method-based query syntax

```csharp
IQueryable<Customer> custQuery = db.Customers.Where(cust => cust.City == "London");
```

### query expression syntax

```csharp
IQueryable<Customer> custQuery =  
    from cust in db.Customers  
    where cust.City == "London"  
    select cust;  
```

## History

C# version 3.0 came in late 2007, along with Visual Studio 2008, though the full boat of language features would actually come with .NET Framework version 3.5. This version marked a major change in the growth of C#.

In retrospect, many of these features seem both inevitable and inseparable. They all fit together strategically. It's generally thought that C# version's killer feature was the [query expression](https://docs.microsoft.com/en-us/dotnet/csharp/linq/query-expression-basics), also known as Language-Integrated Query (LINQ).

C# 3.0 had begun to lay the groundwork for turning C# into a hybrid Object-Oriented / Functional language.

Specifically, you could now write SQL-style, declarative queries to perform operations on collections, among other things. Instead of writing a for loop to compute the average of a list of integers, you could now do that as simply as list.Average().

Excerpt from [The history of C#](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history)

## Benefits of LINQ

- A standardized syntax for querying any type of data (nearly every collection type derived from IEnumerable)
- Strongly typed expressions
- LINQ is extensible
- Less coding instead of writing a loop
- Compile time safety of queries
- Readable code
- LINQ queries are evaluated when as late as possible
- transparent process of evaluating the query
- By using EF Core 3 LINQ queries are evaluated on the server

## More information to Linq

- [Standard Query Operators Overview](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/standard-query-operators-overview)
- [Lambda expressions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/lambda-expressions)
- [Enumerable Class](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=netcore-3.1)
