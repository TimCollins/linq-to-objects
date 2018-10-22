# Reimplementation of LINQ To Objects. 

### Table of Contents
- [Introduction](#introduction) - Introduction
- [Where](#where) - The Where method
- [Chapter 3](#chapter-3) - How to Build with .NET Core
- [Chapter 4](#chapter-4) - Unit Testing with xUnit
- [Chapter 5](#chapter-5) - Working with Relational Databases

___
### **Introduction**
See [here](https://codeblog.jonskeet.uk/category/edulinq/) for source article.

The general approach is:
* Write unit tests against existing LINQ implementation
* Verify those tests pass
* Remove existing LINQ implementation
* Make the tests pass against the reimplementation

The implementation of each method is contained in one class which will be split into separate files with each operator's methods defined in a partial class.
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

An issue arises when trying to add immediate execution e.g. for argument validation in the same method as an iterator block which uses deferred execution. The whole method is treated as a deferred block. The execution type of a given block of code is determined at compile time.

