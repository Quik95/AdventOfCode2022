﻿// See https://aka.ms/new-console-template for more information

foreach (var day in Directory.GetFiles("./inputs", "*.txt"))
{
    var assemblyQualifiedName =
        $"AdventOfCode2022.Solvers.{Path.GetFileNameWithoutExtension(day)}, AdventOfCode2022.Solvers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
    try
    {
        var className = Type.GetType(assemblyQualifiedName);
        var solver = Activator.CreateInstance(className);
        className.GetMethod("Solve").Invoke(solver, null);
    }
    catch (ArgumentNullException e)
    {
        Console.WriteLine($"Failed to load class for {day}");
        throw;
    }
}