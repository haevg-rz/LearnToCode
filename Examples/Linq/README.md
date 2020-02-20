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

```csharp
var collection = List<int>(){12, 2};
var sum = collection.Sum();
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

## Examples

See [LinqExample.Cli/Program.cs](LinqExample.Cli/Program.cs)

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

## Basic of enumerators

[Implementation of generic collection](https://github.com/dotnet/runtime/tree/4f9ae42d861fcb4be2fcd5d3d55d5f227d30e723/src/libraries/System.Private.CoreLib/src/System/Collections/Generic)

### How does foreach work?

The foreach statement executes a statement or a block of statements for each element in an instance of the type that implements the `System.Collections.IEnumerable` or `System.Collections.Generic.IEnumerable<T>` interface. The foreach statement is not limited to those types and can be applied to an instance of any type that satisfies the following conditions:

- has the public parameterless GetEnumerator method whose return type is either class, struct, or interface type,
- the return type of the GetEnumerator method has the public Current property and the public parameterless MoveNext method whose return type is Boolean.

Source:  
- [foreach, in (C# reference)](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/foreach-in)
- [The foreach statement - language specification](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/statements#the-foreach-statement)

### What is the IEnumerator Interface

`IEnumerator` is the base interface for all non-generic enumerators.  
`IEnumerator<T>` is the base interface for all generic enumerators.

The foreach statement of the C# language (for each in C++, For Each in Visual Basic) hides the complexity of the enumerators. Therefore, using foreach is recommended, instead of directly manipulating the enumerator.

Enumerators can be used to read the data in the collection, but they cannot be used to modify the underlying collection.

Initially, the enumerator is positioned before the first element in the collection. At this position, Current is undefined. Therefore, you must call MoveNext to advance the enumerator to the first element of the collection before reading the value of Current.

Current returns the same object until MoveNext is called. MoveNext sets Current to the next element.

If MoveNext passes the end of the collection, the enumerator is positioned after the last element in the collection and MoveNext returns false. When the enumerator is at this position, subsequent calls to MoveNext also return false. If the last call to MoveNext returned false, Current is undefined. You cannot set Current to the first element of the collection again; you must create a new enumerator instance instead.

If changes are made to the collection, such as adding, modifying, or deleting elements, the behavior of the enumerator is undefined.

Source:  
- [`IEnumerator<T>` Interface](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerator-1)
- [`IEnumerator` Interface](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerator)

#### Properties

- Current - Gets the element in the collection at the current position of the enumerator.

##### Methods

- MoveNext() - Advances the enumerator to the next element of the collection.
- Reset() - Sets the enumerator to its initial position, which is before the first element in the collection.

## Classification of Standard Query Operators by Manner of Execution

- Immediate  
  Immediate execution means that the **data source is read** and the **operation is performed** at the point in the code where the query is declared.
- Deferred
  - Streaming  
  Streaming operators **do not have to read all** the source data before they yield elements.
  - Non-Streaming  
  Non-streaming operators **must read all** the source data before they can yield a result element.

[Classification of Standard Query Operators by Manner of Execution (C#) @ docs.microsoft.com ](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/classification-of-standard-query-operators-by-manner-of-execution)

Take should take a look at the [Classification Table](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/classification-of-standard-query-operators-by-manner-of-execution#classification-table)

## More information to Linq

- [Standard Query Operators Overview](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/standard-query-operators-overview)
- [Lambda expressions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/lambda-expressions)
- [Enumerable Class](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=netcore-3.1)
