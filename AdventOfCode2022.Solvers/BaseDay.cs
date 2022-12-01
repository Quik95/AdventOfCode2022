using System.Diagnostics;

namespace AdventOfCode2022.Solvers;

public abstract class BaseDay
{
    protected readonly string RawInput;

    public BaseDay()
    {
        RawInput = File.ReadAllText($"inputs/{GetType().Name}.txt");
    }

    public BaseDay(string rawInput)
    {
        RawInput = rawInput;
    }

    public virtual void Solve()
    {
        Console.WriteLine($"Solution for {GetType().Name}:");
        var partOneTime = Stopwatch.StartNew();
        var part1 = SolvePart1();
        partOneTime.Stop();
        var partTwoTime = Stopwatch.StartNew();
        var part2 = SolvePart2();
        partTwoTime.Stop();

        Console.WriteLine($"\tPart 1: {part1} ({partOneTime.ElapsedMilliseconds}ms)");
        Console.WriteLine($"\tPart 2: {part2} ({partTwoTime.ElapsedMilliseconds}ms)");
        Console.WriteLine();
    }

    public abstract int SolvePart1();
    public abstract int SolvePart2();

    protected virtual string[] ParseInput()
    {
        return RawInput.Split(Environment.NewLine);
    }
}