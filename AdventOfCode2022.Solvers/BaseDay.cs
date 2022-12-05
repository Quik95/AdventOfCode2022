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
        var dayName = GetType().Name;

        Console.WriteLine($"Solution for {GetType().Name}:");
        if (dayName == "Day05")
        {
            var partOneTime = Stopwatch.StartNew();
            var part1 = SolvePart1String();
            partOneTime.Stop();
            var partTwoTime = Stopwatch.StartNew();
            var part2 = SolvePart2String();
            partTwoTime.Stop();
            Console.WriteLine($"\tPart 1: {part1} ({partOneTime.ElapsedMilliseconds}ms)");
            Console.WriteLine($"\tPart 2: {part2} ({partTwoTime.ElapsedMilliseconds}ms)");
        }
        else
        {
            var partOneTime = Stopwatch.StartNew();
            var part1 = SolvePart1();
            partOneTime.Stop();
            var partTwoTime = Stopwatch.StartNew();
            var part2 = SolvePart2();
            partTwoTime.Stop();
            Console.WriteLine($"\tPart 1: {part1} ({partOneTime.ElapsedMilliseconds}ms)");
            Console.WriteLine($"\tPart 2: {part2} ({partTwoTime.ElapsedMilliseconds}ms)");
        }

        Console.WriteLine();
    }

    public virtual int SolvePart1()
    {
        return 42;
    }

    public virtual int SolvePart2()
    {
        return 2137;
    }

    public virtual string SolvePart1String()
    {
        return "42";
    }

    public virtual string SolvePart2String()
    {
        return "2137";
    }

    protected virtual string[] ParseInput()
    {
        return RawInput.Split(Environment.NewLine);
    }
}