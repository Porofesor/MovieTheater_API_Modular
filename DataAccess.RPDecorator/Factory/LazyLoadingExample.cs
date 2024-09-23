using System.Linq.Expressions;
using System;

public interface IA
{
    void MethodA();
}

public interface IB
{
    void MethodB();
}

public interface IC
{
    void MethodC();
}

// Implementing classes for each interface
public class ClassA : IA
{
    public void MethodA()
    {
        Console.WriteLine("ClassA's MethodA");
    }
}

public class ClassB : IB
{
    public void MethodB()
    {
        Console.WriteLine("ClassB's MethodB");
    }
}

public class ClassC : IC
{
    public void MethodC()
    {
        Console.WriteLine("ClassC's MethodC");
    }
}

/// <summary>
/// Explanation:

// Null - coalescing operator (??): This operator checks if the left-hand side is null. If it is,
// it assigns and returns the right-hand side expression.Otherwise, it returns the left-hand side as is.
//    ??= Operator: This is a shorthand for lazy initialization. If _classA is null, it will be assigned a new instance of ClassA(_classA ??= new ClassA()).

// This approach ensures that each class (ClassA, ClassB, ClassC) is only instantiated when it is accessed for the first time.
// Subsequent accesses will return the already instantiated object.
/// </summary>

// MainClass that lazily initializes IA, IB, and IC when accessed
public class MainClass
{
    private IA? _classA;
    private IB? _classB;
    private IC? _classC;

    // Constructor without any instantiation (lazy-loaded when accessed)
    public MainClass()
    {
    }

    // Lazily initialize IA using the null-coalescing operator (??)
    public IA ClassA => _classA ??= new ClassA();

    // Lazily initialize IB using the null-coalescing operator (??)
    public IB ClassB => _classB ??= new ClassB();

    // Lazily initialize IC using the null-coalescing operator (??)
    public IC ClassC => _classC ??= new ClassC();
}

// Example usage
//public class Program
//{
//    public static void Main()
//    {
//        var main = new MainClass();

//        // Lazy loading happens when you access ClassA, ClassB, and ClassC for the first time
//        main.ClassA.MethodA(); // Output: ClassA's MethodA
//        main.ClassB.MethodB(); // Output: ClassB's MethodB
//        main.ClassC.MethodC(); // Output: ClassC's MethodC
//    }
//}