# The Binary Search

In this example I will show you the theory behind the binary search method (as an alternative to the sequential search) and how to implement it in C#.

### Sequential searching - the method

In order to better understand the benefits of the Binary Search, let's take a look at how you would usually check a data set for an existing value.

Given is the following array of integers and the number we are looking for:

```csharp
int[] array = new[] { 1, 2, 5, 8, 15, 45, 87, 99 };
int numberToLookFor = 87;
```

Let's assume we want to find the value **87**. We would check the first value (using a loop). 1 isn't 87, so we check the next value. 2 isn't 87 either, so, you guessed it, we check the next value and so on until we reach the correct value (or not). In this example, the value can be found at position 6 (7th element) which would be the result we were aiming at.

If we are lucky, the value we're looking is right at the start of the array. If we're unlucky, it's at the end which will take more time.
Maybe the element we're looking for isn't present in the array at all. That means we would need to check **all** existing values and get nothing in return.

## Binary searching - the method

**Important**: The binary search only works with sorted data (direction doesn't matter)!

Now, the binary search might sound complicated - but it really isn't. Let's take a look at this array of integers:

```csharp
int[] array = new[] { 1, 2, 5, 8, 15, 16, 29, 45, 77 };
int numberToLookFor = 2;
```
Suppose we want the position of the value **2**. The first step is determining the element in the middle of the array, which is 5 with the value of 15. If the element is equal to *numberToLookFor* then rust return it. If it's smaller that *numberToLookFor*, we will repeat the process the remaining elements right of the middle element. If larger, we will take a look at the elements left of the middle element. Now we have only 
```csharp
{1, 2, 5, 8}
```
left to check. Again, we take the middle element. Of course, there is no element in the middle of a row with an even number of elements. You can then either round up or down your calculated value. It will work the either way. In this case, let's declare our next middle element to be 2 which holds the value of 5. Now 5 isn't our sought-after value either, so again, we repeat the process which will eventually lead us to the position 1 with our *numberToLookFor*.

You may wonder what the big difference is. Using the binary approach will, given exactly the same data, **take half the time** of a sequential search **at most**.

For a working example, check out the BinarySearch.sln project.