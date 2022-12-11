// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

var sw = Stopwatch.StartNew();
foreach (var day in Directory.GetFiles("./inputs", "*.txt"))
{
    var assemblyQualifiedName =
        $"AdventOfCode2022.Solvers.{Path.GetFileNameWithoutExtension(day)}, AdventOfCode2022.Solvers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
    try
    {
        var className = Type.GetType(assemblyQualifiedName);
        var solver = Activator.CreateInstance(className!);
        className?.GetMethod("Solve")!.Invoke(solver, null);
    }
    catch (ArgumentNullException)
    {
        Console.WriteLine($"Failed to load class for {day}");
        throw;
    }
}

sw.Stop();
Console.WriteLine($"Cumulative time: {sw.ElapsedMilliseconds}ms");