# Reimplementation of LINQ To Objects. 

### Table of Contents
- [Introduction](#introduction) - Introduction
- [Where](#where) - The Where method
- [Select](#select) - The Select method

___
### **Introduction**
See [here](https://codeblog.jonskeet.uk/category/edulinq/) for source article.

The general approach is:
* Write unit tests against existing LINQ implementation
* Verify those tests pass
* Remove existing LINQ implementation
* Make the tests pass against the reimplementation

The implementation of each method is contained in one class which will be split into separate files with each operator's methods defined in a partial class.

VS2015 seems to be awkward for running tests in the absence of ReSharper so I've fallen back to using the GUI with NUnit 2.8.4.

___
### **Where**
The MSDN reference is [here](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where)
It has two overloads. The method signatures are:
```csharp
// Returns a sequence of the TSource type
// Uses a sequence of TSource as the source type.
// Apply the predicate using each element's index in the predicate
Where<TSource>(IEnumerable<TSource>, Func<TSource,Int32,Boolean>) 	

// As above but the element index is NOT used
Where<TSource>(IEnumerable<TSource>, Func<TSource,Boolean>)
```

Each method is [generic](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-methods) and defined as an [extension method](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods).

Some points about the behaviour of the operator can be used as a basis for the unit tests:
* The input sequence is not modified
* The method uses deferred execution
* Null parameters will be validated immediately
* Results will be streamed

```csharp
// Initial implementation
// Each operator's collection of methods will be declared in one big static 
// class split up into appropriately-named partial classes.
public static partial class Enumerable
{
	// Declare a static extension method that returns a generic enumerable collection of whatever type TSource represents.
	// The extension method operates on a generic enumerable collection of whatever type TSource represents.
	// It takes a generic delegate which itself takes an instance of TSource and returns a bool
	public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
	{
		// Use an iterator block to loop through each element in the collection
		foreach (TSource item in source)
		{
			// If the predicate function passes then return the item.
			// The predicate/generic delegate is the part of the expression 
			// that says "return all elements where the value is less than 4" .Where(x => x < 4)
			if (predicate(item))
			{
				yield return item;
			}
		}
	}
}

```

An issue arises when trying to add immediate execution e.g. for argument validation in the same method as an iterator block which uses deferred execution. The whole method is treated as a deferred block. The execution type of a given block of code is determined at compile time. The way around this is to split the implementation
```csharp
public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
{
	if (source == null)
	{
		throw new ArgumentNullException("source cannot be null");
	}

	if (predicate == null)
	{
		throw new ArgumentNullException("predicate cannot be null");
	}

	return WhereImpl(source, predicate);
}

private static IEnumerable<TSource> WhereImpl<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
{
	foreach (TSource item in source)
	{
		if (predicate(item))
		{
			yield return item;
		}
	}
}
```

The version with the index is the same except there is an index variable defined outside the foreach() which is passed to the predicate and incremented at the end of each loop iteration.
```csharp
	var numbers = Enumerable.Range(1, 10);
	// Will contain 1, 3, 5 etc.
	// Array indexing starts at zero
	var output = numbers.Where((n, index) => index % 2 == 0);
```
___
### **Select**
The MSDN reference is [here](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.select)
It has two overloads. The method signatures are:
```csharp
// Project each element of a sequence into a new form by incorporating the element's index
Select<TSource, TResult>(IEnumerable<TSource>, Func<TSource, Int32, TResult>)

// As above but the element index is NOT used
Select<TSource, TResult>(IEnumerable<TSource>, Func<TSource, TResult>)
```
The selector delegate is applied to each input element in turn to yield an output element.
The tests for `Select` are similar to `Where` except the filtering has now become projecting.