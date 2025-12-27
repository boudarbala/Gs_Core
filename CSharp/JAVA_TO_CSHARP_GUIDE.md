# Java to C# Translation Reference Guide

**Last Updated:** 2025-12-27

This comprehensive guide provides a reference for translating Java code to C# within the Gs_Core project. It covers syntax differences, framework equivalences, and best practices for successful migration.

---

## Table of Contents

1. [Basic Syntax & Language Features](#basic-syntax--language-features)
2. [Data Types & Variables](#data-types--variables)
3. [Object-Oriented Programming](#object-oriented-programming)
4. [Collections & Generics](#collections--generics)
5. [Exception Handling](#exception-handling)
6. [File & I/O Operations](#file--io-operations)
7. [Threading & Async/Await](#threading--asyncawait)
8. [LINQ vs Java Streams](#linq-vs-java-streams)
9. [Functional Programming](#functional-programming)
10. [Common Framework Equivalences](#common-framework-equivalences)
11. [Best Practices for Migration](#best-practices-for-migration)

---

## Basic Syntax & Language Features

### Class Declaration

**Java:**
```java
public class Calculator {
    public int add(int a, int b) {
        return a + b;
    }
}
```

**C#:**
```csharp
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}
```

**Key Differences:**
- Method names use PascalCase in C# (not camelCase)
- Braces placement follows different conventions

### Class Modifiers

| Java | C# | Notes |
|------|----|----|
| `public` | `public` | Same |
| `private` | `private` | Same |
| `protected` | `protected` | Same |
| `protected internal` | N/A | Use `internal protected` or `protected internal` |
| `abstract` | `abstract` | Same |
| `final` | `sealed` | Prevents inheritance |
| `static` | `static` | Same |

### Constants

**Java:**
```java
public static final double PI = 3.14159;
```

**C#:**
```csharp
public const double PI = 3.14159;
// or for compile-time constants
public static readonly double PI = 3.14159;
```

---

## Data Types & Variables

### Primitive Types Mapping

| Java | C# | Equivalent | Size |
|------|----|----|------|
| `byte` | `byte` | 8-bit unsigned integer | 1 byte |
| `short` | `short` | 16-bit signed integer | 2 bytes |
| `int` | `int` | 32-bit signed integer | 4 bytes |
| `long` | `long` | 64-bit signed integer | 8 bytes |
| `float` | `float` | 32-bit floating point | 4 bytes |
| `double` | `double` | 64-bit floating point | 8 bytes |
| `boolean` | `bool` | Boolean value | - |
| `char` | `char` | Unicode character | 2 bytes |

### Nullable Types

**Java:**
```java
Integer value = null;
```

**C#:**
```csharp
int? value = null;  // Nullable value type
string reference = null;  // Reference types are nullable by default
```

### String Operations

**Java:**
```java
String text = "Hello";
String formatted = String.format("Value: %d", 42);
String concatenated = "Hello" + " " + "World";
```

**C#:**
```csharp
string text = "Hello";
string formatted = string.Format("Value: {0}", 42);
// or using string interpolation (preferred)
string formatted = $"Value: {42}";
string concatenated = "Hello" + " " + "World";
// or using string concatenation
string concatenated = string.Concat("Hello", " ", "World");
```

### Variable Declaration

**Java:**
```java
int count = 0;
var items = new ArrayList<>();  // Java 10+
```

**C#:**
```csharp
int count = 0;
var items = new List<string>();  // Implicitly typed
List<string> items = new();  // C# 9.0+
```

---

## Object-Oriented Programming

### Constructors

**Java:**
```java
public class Person {
    private String name;
    private int age;
    
    public Person(String name, int age) {
        this.name = name;
        this.age = age;
    }
}
```

**C#:**
```csharp
public class Person
{
    private string _name;
    private int _age;
    
    public Person(string name, int age)
    {
        _name = name;
        _age = age;
    }
}
```

### Properties

**Java:**
```java
public class Person {
    private String name;
    
    public String getName() {
        return name;
    }
    
    public void setName(String name) {
        this.name = name;
    }
}
```

**C#:**
```csharp
public class Person
{
    private string _name;
    
    // Traditional property
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    
    // Auto-property (preferred)
    public string Name { get; set; }
    
    // Init-only property (C# 9.0+)
    public string Name { get; init; }
}
```

### Inheritance

**Java:**
```java
public class Animal {
    public void makeSound() {
        System.out.println("Some sound");
    }
}

public class Dog extends Animal {
    @Override
    public void makeSound() {
        System.out.println("Woof!");
    }
}
```

**C#:**
```csharp
public class Animal
{
    public virtual void MakeSound()
    {
        Console.WriteLine("Some sound");
    }
}

public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Woof!");
    }
}
```

### Interfaces

**Java:**
```java
public interface Drawable {
    void draw();
}

public class Circle implements Drawable {
    public void draw() {
        System.out.println("Drawing circle");
    }
}
```

**C#:**
```csharp
public interface IDrawable
{
    void Draw();
}

public class Circle : IDrawable
{
    public void Draw()
    {
        Console.WriteLine("Drawing circle");
    }
}
```

**C# Interface Naming Convention:** Use `I` prefix for interfaces (e.g., `IDrawable`, `IComparable`)

### Abstract Classes

**Java:**
```java
public abstract class Shape {
    abstract double getArea();
    
    public void printArea() {
        System.out.println("Area: " + getArea());
    }
}
```

**C#:**
```csharp
public abstract class Shape
{
    abstract double GetArea();
    
    public void PrintArea()
    {
        Console.WriteLine($"Area: {GetArea()}");
    }
}
```

### Static Members

**Java:**
```java
public class MathHelper {
    public static final double PI = 3.14159;
    
    public static int add(int a, int b) {
        return a + b;
    }
}

// Usage
int result = MathHelper.add(5, 3);
```

**C#:**
```csharp
public class MathHelper
{
    public const double PI = 3.14159;
    
    public static int Add(int a, int b)
    {
        return a + b;
    }
}

// Usage
int result = MathHelper.Add(5, 3);
```

---

## Collections & Generics

### List

**Java:**
```java
List<String> fruits = new ArrayList<>();
fruits.add("Apple");
fruits.add("Banana");

for (String fruit : fruits) {
    System.out.println(fruit);
}
```

**C#:**
```csharp
List<string> fruits = new List<string>();
fruits.Add("Apple");
fruits.Add("Banana");

foreach (string fruit in fruits)
{
    Console.WriteLine(fruit);
}

// or with collection initializer
List<string> fruits = new List<string> { "Apple", "Banana" };
```

### HashMap

**Java:**
```java
Map<String, Integer> scores = new HashMap<>();
scores.put("Alice", 95);
scores.put("Bob", 87);

if (scores.containsKey("Alice")) {
    int score = scores.get("Alice");
}
```

**C#:**
```csharp
Dictionary<string, int> scores = new Dictionary<string, int>();
scores["Alice"] = 95;
scores["Bob"] = 87;

if (scores.ContainsKey("Alice"))
{
    int score = scores["Alice"];
}

// or with collection initializer
var scores = new Dictionary<string, int>
{
    { "Alice", 95 },
    { "Bob", 87 }
};
```

### Set

**Java:**
```java
Set<String> colors = new HashSet<>();
colors.add("Red");
colors.add("Blue");
```

**C#:**
```csharp
HashSet<string> colors = new HashSet<string>();
colors.Add("Red");
colors.Add("Blue");

// or
var colors = new HashSet<string> { "Red", "Blue" };
```

### Queue

**Java:**
```java
Queue<Integer> queue = new LinkedList<>();
queue.offer(1);
queue.offer(2);
int first = queue.poll();
```

**C#:**
```csharp
Queue<int> queue = new Queue<int>();
queue.Enqueue(1);
queue.Enqueue(2);
int first = queue.Dequeue();
```

### Generics Constraints

**Java:**
```java
public class Container<T extends Number> {
    private T value;
    
    public T getValue() {
        return value;
    }
}
```

**C#:**
```csharp
public class Container<T> where T : INumber<T>
{
    private T _value;
    
    public T GetValue()
    {
        return _value;
    }
}
```

---

## Exception Handling

### Try-Catch

**Java:**
```java
try {
    int result = 10 / 0;
} catch (ArithmeticException e) {
    System.out.println("Cannot divide by zero");
} catch (Exception e) {
    System.out.println("An error occurred");
} finally {
    System.out.println("Cleanup");
}
```

**C#:**
```csharp
try
{
    int result = 10 / 0;
}
catch (DivideByZeroException e)
{
    Console.WriteLine("Cannot divide by zero");
}
catch (Exception e)
{
    Console.WriteLine("An error occurred");
}
finally
{
    Console.WriteLine("Cleanup");
}
```

### Custom Exceptions

**Java:**
```java
public class InvalidInputException extends Exception {
    public InvalidInputException(String message) {
        super(message);
    }
}

throw new InvalidInputException("Input is invalid");
```

**C#:**
```csharp
public class InvalidInputException : Exception
{
    public InvalidInputException(string message) : base(message) { }
}

throw new InvalidInputException("Input is invalid");
```

### Try-With-Resources (Java) vs Using Statement (C#)

**Java:**
```java
try (FileReader reader = new FileReader("file.txt")) {
    // Use reader
} catch (IOException e) {
    e.printStackTrace();
}
```

**C#:**
```csharp
using (FileStream stream = new FileStream("file.txt", FileMode.Open))
{
    // Use stream
}

// or with declaration
using FileStream stream = new FileStream("file.txt", FileMode.Open);
// stream automatically disposed at end of scope
```

---

## File & I/O Operations

### Reading a File

**Java:**
```java
import java.nio.file.Files;
import java.nio.file.Paths;

String content = new String(Files.readAllBytes(Paths.get("file.txt")));

// or line by line
Files.lines(Paths.get("file.txt")).forEach(System.out::println);
```

**C#:**
```csharp
using System.IO;

string content = File.ReadAllText("file.txt");

// or line by line
foreach (string line in File.ReadLines("file.txt"))
{
    Console.WriteLine(line);
}
```

### Writing to a File

**Java:**
```java
import java.nio.file.Files;
import java.nio.file.Paths;

Files.write(Paths.get("file.txt"), "Content".getBytes());
```

**C#:**
```csharp
using System.IO;

File.WriteAllText("file.txt", "Content");

// or append
File.AppendAllText("file.txt", "More content");
```

### Directory Operations

**Java:**
```java
File dir = new File("path/to/dir");
if (dir.exists() && dir.isDirectory()) {
    File[] files = dir.listFiles();
}
```

**C#:**
```csharp
using System.IO;

string dir = "path/to/dir";
if (Directory.Exists(dir))
{
    string[] files = Directory.GetFiles(dir);
}
```

---

## Threading & Async/Await

### Creating Threads

**Java:**
```java
Thread thread = new Thread(() -> {
    System.out.println("Running in thread");
});
thread.start();
```

**C#:**
```csharp
Thread thread = new Thread(() =>
{
    Console.WriteLine("Running in thread");
});
thread.Start();
```

### Async/Await (C# Advantage)

**Java:**
```java
// Java uses CompletableFuture
CompletableFuture<String> future = CompletableFuture.supplyAsync(() -> {
    return "Result";
});

future.thenAccept(result -> System.out.println(result));
```

**C#:**
```csharp
// C# uses async/await (cleaner)
async Task<string> GetResultAsync()
{
    await Task.Delay(1000);
    return "Result";
}

// Usage
string result = await GetResultAsync();
Console.WriteLine(result);
```

### Thread Synchronization

**Java:**
```java
public synchronized void criticalSection() {
    // Only one thread can access this at a time
}
```

**C#:**
```csharp
private object _lockObject = new object();

public void CriticalSection()
{
    lock (_lockObject)
    {
        // Only one thread can access this at a time
    }
}
```

---

## LINQ vs Java Streams

### Filtering

**Java:**
```java
List<Integer> numbers = Arrays.asList(1, 2, 3, 4, 5);
List<Integer> evens = numbers.stream()
    .filter(n -> n % 2 == 0)
    .collect(Collectors.toList());
```

**C#:**
```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
List<int> evens = numbers
    .Where(n => n % 2 == 0)
    .ToList();
```

### Mapping

**Java:**
```java
List<String> names = Arrays.asList("alice", "bob", "charlie");
List<String> upperNames = names.stream()
    .map(String::toUpperCase)
    .collect(Collectors.toList());
```

**C#:**
```csharp
List<string> names = new List<string> { "alice", "bob", "charlie" };
List<string> upperNames = names
    .Select(n => n.ToUpper())
    .ToList();
```

### Aggregation

**Java:**
```java
List<Integer> numbers = Arrays.asList(1, 2, 3, 4, 5);
int sum = numbers.stream().reduce(0, Integer::sum);
int max = numbers.stream().max(Integer::compare).orElse(0);
```

**C#:**
```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
int sum = numbers.Sum();
int max = numbers.Max();
```

### Grouping

**Java:**
```java
List<Person> people = /* ... */;
Map<String, List<Person>> grouped = people.stream()
    .collect(Collectors.groupingBy(Person::getCity));
```

**C#:**
```csharp
List<Person> people = /* ... */;
var grouped = people
    .GroupBy(p => p.City)
    .ToDictionary(g => g.Key, g => g.ToList());
```

---

## Functional Programming

### Lambda Expressions

**Java:**
```java
// Functional interface
@FunctionalInterface
public interface Calculator {
    int calculate(int a, int b);
}

Calculator adder = (a, b) -> a + b;
int result = adder.calculate(5, 3);
```

**C#:**
```csharp
// Delegate type
public delegate int Calculator(int a, int b);

Calculator adder = (a, b) => a + b;
int result = adder(5, 3);

// or using Func<T, T, TResult>
Func<int, int, int> adder = (a, b) => a + b;
```

### Method References

**Java:**
```java
List<String> names = Arrays.asList("alice", "bob");
names.forEach(System.out::println);

// Lambda equivalent
names.forEach(name -> System.out.println(name));
```

**C#:**
```csharp
List<string> names = new List<string> { "alice", "bob" };
names.ForEach(Console.WriteLine);

// or lambda
names.ForEach(name => Console.WriteLine(name));
```

### Predicate

**Java:**
```java
Predicate<Integer> isEven = n -> n % 2 == 0;
if (isEven.test(4)) {
    System.out.println("Even");
}
```

**C#:**
```csharp
Func<int, bool> isEven = n => n % 2 == 0;
if (isEven(4))
{
    Console.WriteLine("Even");
}
```

---

## Common Framework Equivalences

### Logging

**Java (Log4j/SLF4J):**
```java
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

Logger logger = LoggerFactory.getLogger(MyClass.class);
logger.info("Information message");
logger.error("Error occurred", exception);
```

**C# (NLog/Serilog):**
```csharp
using NLog;

Logger logger = LogManager.GetCurrentClassLogger();
logger.Info("Information message");
logger.Error(exception, "Error occurred");
```

### Serialization

**Java (Jackson):**
```java
import com.fasterxml.jackson.databind.ObjectMapper;

ObjectMapper mapper = new ObjectMapper();
String json = mapper.writeValueAsString(object);
MyObject obj = mapper.readValue(json, MyObject.class);
```

**C# (Newtonsoft.Json/System.Text.Json):**
```csharp
using Newtonsoft.Json;

string json = JsonConvert.SerializeObject(obj);
MyObject obj = JsonConvert.DeserializeObject<MyObject>(json);

// or with System.Text.Json (modern)
using System.Text.Json;
string json = JsonSerializer.Serialize(obj);
MyObject obj = JsonSerializer.Deserialize<MyObject>(json);
```

### HTTP Requests

**Java (HttpClient):**
```java
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;

HttpClient client = HttpClient.newHttpClient();
HttpRequest request = HttpRequest.newBuilder()
    .uri(URI.create("https://api.example.com"))
    .GET()
    .build();
HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
```

**C# (HttpClient):**
```csharp
using System.Net.Http;

using HttpClient client = new HttpClient();
HttpResponseMessage response = await client.GetAsync("https://api.example.com");
string content = await response.Content.ReadAsStringAsync();
```

### Unit Testing

**Java (JUnit):**
```java
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.assertEquals;

public class CalculatorTest {
    @Test
    public void testAdd() {
        assertEquals(4, new Calculator().add(2, 2));
    }
}
```

**C# (xUnit/NUnit):**
```csharp
using Xunit;

public class CalculatorTest
{
    [Fact]
    public void TestAdd()
    {
        Assert.Equal(4, new Calculator().Add(2, 2));
    }
}
```

### Dependency Injection

**Java (Spring):**
```java
@Component
public class MyService {
    @Autowired
    private MyRepository repository;
}
```

**C# (.NET):**
```csharp
public class MyService
{
    private readonly IMyRepository _repository;
    
    public MyService(IMyRepository repository)
    {
        _repository = repository;
    }
}

// Registration
services.AddScoped<IMyRepository, MyRepository>();
services.AddScoped<MyService>();
```

---

## Best Practices for Migration

### 1. **Naming Conventions**

| Element | Java | C# |
|---------|------|-----|
| Class | PascalCase | PascalCase |
| Method | camelCase | PascalCase |
| Constant | UPPER_SNAKE_CASE | PascalCase (const) or UPPER_SNAKE_CASE |
| Field (private) | camelCase | _camelCase (prefix with underscore) |
| Property | camelCase (with getters/setters) | PascalCase |
| Parameter | camelCase | camelCase |
| Local Variable | camelCase | camelCase |
| Interface | InterfaceName | IInterfaceName (prefix with I) |

### 2. **Code Organization**

**Java:**
```java
// One public class per file
public class MyClass { }
```

**C#:**
```csharp
// Can have multiple classes, but one public per file is standard
public class MyClass { }

// Related classes can be nested
public class Outer
{
    public class Inner { }
}
```

### 3. **Access Modifiers**

- Use `private` by default
- Make fields `readonly` when possible
- Use `public` properties with `{ get; private set; }` instead of public fields

### 4. **String Handling**

- Use string interpolation (`$"text {variable}"`) over concatenation
- Use `StringBuilder` for repeated concatenations
- Prefer immutability; strings are immutable in both languages

### 5. **Null Handling**

**Java:**
```java
if (obj != null) {
    obj.doSomething();
}
```

**C# (newer versions):**
```csharp
// Null-conditional operator
obj?.DoSomething();

// Null-coalescing
string value = obj?.Name ?? "default";

// Null-coalescing assignment (C# 8.0+)
obj ??= new Object();
```

### 6. **Async/Await Patterns**

- Always use `async/await` for I/O operations in C#
- Avoid blocking calls like `.Result` or `.Wait()`
- Suffix async methods with "Async" (e.g., `GetDataAsync`)

### 7. **Immutability**

**C# Records (C# 9.0+):**
```csharp
public record Person(string Name, int Age);

// Usage
var person = new Person("Alice", 30);
var olderPerson = person with { Age = 31 };  // Create modified copy
```

### 8. **LINQ Preferred Over Loops**

**Before:**
```csharp
List<int> evens = new List<int>();
foreach (int num in numbers)
{
    if (num % 2 == 0)
        evens.Add(num);
}
```

**After:**
```csharp
List<int> evens = numbers.Where(n => n % 2 == 0).ToList();
```

### 9. **Using Declarations**

**Java:**
```java
try (FileReader reader = new FileReader("file.txt")) {
    // Use reader
}
```

**C# (Modern):**
```csharp
using FileStream stream = new FileStream("file.txt", FileMode.Open);
// Automatically disposed at end of scope
```

### 10. **Documentation**

**Java:**
```java
/**
 * Calculates the sum of two numbers.
 * @param a First number
 * @param b Second number
 * @return The sum of a and b
 */
public int add(int a, int b) {
    return a + b;
}
```

**C#:**
```csharp
/// <summary>
/// Calculates the sum of two numbers.
/// </summary>
/// <param name="a">First number</param>
/// <param name="b">Second number</param>
/// <returns>The sum of a and b</returns>
public int Add(int a, int b)
{
    return a + b;
}
```

---

## Common Pitfalls to Avoid

1. **Forgetting PascalCase for Methods:** C# methods should be PascalCase, not camelCase
2. **Not Using Properties:** Replace getters/setters with C# properties
3. **Null Reference Exceptions:** Use null-conditional operators and null-coalescing
4. **Blocking Async Code:** Avoid `.Result` or `.Wait()` on async methods
5. **Not Using `using` Statements:** Always dispose of resources properly
6. **Ignoring LINQ:** Traditional loops are less efficient than LINQ queries
7. **Creating Large Strings:** Use `StringBuilder` for repeated concatenations
8. **Not Marking Classes as `sealed`:** Mark classes as `sealed` if not meant to be inherited

---

## Resources for Migration

- [Microsoft C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [Java to C# Comparison Guide](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/)
- [LINQ Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)
- [Async/Await Best Practices](https://docs.microsoft.com/en-us/archive/msdn-magazine/2013/march/async-await-best-practices-in-asynchronous-programming)

---

## Version-Specific Features

### C# 8.0 Features
- Nullable reference types
- Async streams
- Default implementations in interfaces

### C# 9.0+ Features
- Records
- Init-only properties
- Top-level statements
- Pattern matching enhancements

---

**Document Maintenance:** This guide should be updated as new C# versions are adopted and migration patterns are refined.
