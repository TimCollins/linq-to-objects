# Reimplementation of LINQ To Objects. 

### Table of Contents
- [Introduction](#introduction) - Introduction
- [Code Coverage](#code-coverage) - Code coverage details
- [Where](#where) - The Where method
- [Select](#select) - The Select method
- [Range](#range) - The Range method
- [Empty](#empty) - The Empty method
- [Repeat](#repeat) - The Repeat method
- [Count](#count) - The Count method
- [Concat](#concat) - The Concat method
- [SelectMany](#selectmany) - The SelectMany method
- [Any](#any) - The Any method
- [All](#all) - The All method
- [First](#first) - The First method
- [FirstOrDefault](#firstordefault) - The FirstOrDefault method
- [Last](#Last) - The Last method
- [LastOrDefault](#lastordefault) - The LastOrDefault method
- [Single](#Single) - The Single method
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

To debug unit tests with the NUnit Test Runner, from within Visual Studo: choose `Debug` -> `Attach to Process...` and select `nunit-agent.exe` from the list. Then stick a breakpoint in a unit test and hit F5 from within the test runner. The Visual Studio debugger should take over once the breakpoint is hit.

___
### **Code coverage**
Code coverage is generated by [NUnit.Runners](https://www.nuget.org/packages/NUnit.Runners/), [OpenCover](https://github.com/OpenCover/opencover) and [ReportGenerator](https://github.com/danielpalme/ReportGenerator).
* Run `GenerateResults.bat` to create `results.xml` from the assemblies specified in `GenerateTestResults.bat`.
* Assuming `results.xml` gets generated successfully, run `GenerateCoverage.bat` to create a coverage report in `UnitTests/coverage` and open `coverage/index.htm` in a browser.

**Only the versions in package.config should be used. Newer versions tend to break things**
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

### **Range**
The MSDN reference is [here](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.range)
There is only one method signature:
```csharp
// Return an object which can be iterated over. The object will contain "count" integers starting at "start"
public static IEnumerable<int> Range (int start, int count);
```
In this case it's just a static method, not an extension method.
Input can't be streamed or buffered. Another important implementation point is that it should be a low-overhead method. An array of "count" elements should not be generated and returned for example. Instead values should be yield returned (lazily).
___
### **Empty**
The MSDN reference is [here](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.empty)
It has one overload. The method signature is:
```csharp
public static System.Collections.Generic.IEnumerable<TResult> Empty<TResult> ();
```
This function returns an empty sequence of the specified type.

The [post](https://codeblog.jonskeet.uk/2010/12/24/reimplementing-linq-to-objects-part-5-empty/) mentions caching and provides a different implementation but there is no test to cover that scenario.
___
### **Repeat**
The MSDN reference is [here](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.repeat)
It has one overload. The method signature is:
```csharp
public static IEnumerable<TResult> Repeat<TResult> (TResult element, int count);
```
This function returns a sequence which contains the specified element repeated a specified number of times.
___
### **Count**
The MSDN reference is [here](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.count)
It has two overloads. The method signature is:
```csharp
// Returns the number of elements in the sequence
public static int Count<TSource> (this System.Collections.Generic.IEnumerable<TSource> source);
// Returns the number of elements in the sequence that satisfy the specified condition
public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);
```
There is no deferred execution here.
The tests should verify two things. A count with a predicate and a count without a predicate. The article also discusses the following:
* A source which implements both ICollection<T> and ICollection (easy: use List<T>)
* A source which implements ICollection<T> but not ICollection (reasonably easy, after a little work finding a suitable type: use HashSet<T>)
* A source which implements ICollection but not ICollection<T> but still implements IEnumerable<T> (so that we can extend it) – tricky…
* A source which doesn’t implement ICollection or ICollection<T> (easy: use Enumerable.Range which we’ve already implemented)

The sources are:
* List<T>
* HashSet<T>
* See the UnitTests.TestSupport.SemiGenericCollection class
* Enumerable.Range()
___
### **Concat**
The MSDN reference is [here](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.concat). It has one overload. The method signature is:

```csharp
// Concatenates two sequences
IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
```
The concatenation just returns all of the items in the first collection and then all of the items in the second one.

___
### **SelectMany**
The article refers to this as the most important operator in the whole of LINQ. Most other operators that returns sequences can be implemented via SelectMany. The MSDN reference is [here](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.selectmany). It has four overloads. The method signatures are:

```csharp
// Projects each element of a sequence to an IEnumerable<T>, flattens the resulting sequences into one sequence, and invokes a result selector function on each element therein.
SelectMany<TSource,TCollection,TResult>(IEnumerable<TSource>, Func<TSource,IEnumerable<TCollection>>, Func<TSource,TCollection,TResult>)
// Projects each element of a sequence to an IEnumerable<T>, flattens the resulting sequences into one sequence, and invokes a result selector function on each element therein. The index of each source element is used in the intermediate projected form of that element.
SelectMany<TSource,TCollection,TResult>(IEnumerable<TSource>, Func<TSource,Int32,IEnumerable<TCollection>>, Func<TSource,TCollection,TResult>)
// Projects each element of a sequence to an IEnumerable<T> and flattens the resulting sequences into one sequence.
SelectMany<TSource,TResult>(IEnumerable<TSource>, Func<TSource,IEnumerable<TResult>>)
// Projects each element of a sequence to an IEnumerable<T>, and flattens the resulting sequences into one sequence. The index of each source element is used in the projected form of that element.
SelectMany<TSource,TResult>(IEnumerable<TSource>, Func<TSource,Int32,IEnumerable<TResult>>)
```
In each case, a subsequence is generated from each element of the input sequence using a delegate which can optionally take a parameter with the index of the element within the original collection.

Each element from each subsequence is returned directly or another delegate is applied which takes the original element in the input sequence and the element within the subsequence.

___
### **Any**
Determines whether any element of a sequence exists or satisfies a condition. The MSDN reference is [here](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.any). It has two overloads
```csharp
// Determine whether the given sequence contains any elements 
bool Any<TSource> (this IEnumerable<TSource> source);
// Determine whether the given sequence contains any elements that match the specified predicate
bool Any<TSource> (this IEnumerable<TSource> source, Func<TSource,bool> predicate);
```
It uses immediate execution so the validation and implementation blocks don't have to be separated.

___
### **All**
Determines whether all elements of a sequence satisfy a condition. The MSDN reference is [here](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.all). It has one overload
```csharp
// Determine whether the given sequence contains any elements 
bool All<TSource> (this IEnumerable<TSource> source, Func<TSource,bool> predicate);
```
It uses immediate execution so the validation and implementation blocks don't have to be separated.

___
### **First**
Returns the first element of a sequence. The MSDN reference is [here](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.first). It has two overloads
```csharp
// Returns the first element of a sequence.
TSource First<TSource> (this IEnumerable<TSource> source);
// Returns the first element in a sequence that satisfies a specified condition.
TSource First<TSource> (this IEnumerable<TSource> source, Func<TSource,bool> predicate);
```

Enumerator.Current will throw an InvalidOperationException if unavailable (e.g. an empty sequence) so no need to check for it explicitly.

___
### **FirstOrDefault**
Returns the first element of a sequence, or a default value if no element is found.
 The MSDN reference is [here](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault). It has two overloads
```csharp
// Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.
TSource FirstOrDefault<TSource> (this IEnumerable<TSource> source, Func<TSource,bool> predicate);
// Returns the first element of a sequence, or a default value if the sequence contains no elements.
TSource FirstOrDefault<TSource> (this IEnumerable<TSource> source);
```

This works the same as First() except that instead of throwing an exception when a matching element is not found, the default value for the type is returned instead.

___
### **Last**
Returns the last element of a sequence. The MSDN reference is [here](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.last). It has two overloads
```csharp
// Returns the last element of a sequence.
TSource Last<TSource> (this IEnumerable<TSource> source);
// Returns the last element in a sequence that satisfies a specified condition.
TSource Last<TSource> (this IEnumerable<TSource> source, Func<TSource,bool> predicate);
```

___
### **LastOrDefault**
Returns the last element of a sequence, or a default value if no element is found. The MSDN reference is [here](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault). It has two overloads
```csharp
// Returns the last element of the sequence that satisfies a condition or a default value if no such element is found.
TSource LastOrDefault<TSource> (this IEnumerable<TSource> source, Func<TSource,bool> predicate);
// Returns the last element of a sequence, or a default value if the sequence contains no elements.
TSource LastOrDefault<TSource> (this IEnumerable<TSource> source);
```
___
### **Single**
Returns a single, specific element of a sequence. The MSDN reference is [here](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.single).
It has two overloads
```csharp
// Returns the only element of a sequence that satisfies a specified condition, and throws an exception if more than one such element exists.
TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
Returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.
TSource Single<TSource>(this IEnumerable<TSource> source)
```