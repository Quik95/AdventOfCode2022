using System.Diagnostics;

namespace AdventOfCode2022.Solvers;

public sealed class Day02 : BaseDay
{
    private static readonly Dictionary<string, int> LookupTable = new()
    {
        {"A X", 1 + 3},
        {"A Y", 2 + 6},
        {"A Z", 3 + 0},
        {"B X", 1 + 0},
        {"B Y", 2 + 3},
        {"B Z", 3 + 6},
        {"C X", 1 + 6},
        {"C Y", 2 + 0},
        {"C Z", 3 + 3}
    };

    private static readonly Dictionary<string, int> LookupTable2 = new()
    {
        {"A X", 3 + 0},
        {"A Y", 1 + 3},
        {"A Z", 2 + 6},
        {"B X", 1 + 0},
        {"B Y", 2 + 3},
        {"B Z", 3 + 6},
        {"C X", 2 + 0},
        {"C Y", 3 + 3},
        {"C Z", 1 + 6}
    };

    private readonly string[] _input;

    public Day02()
    {
        var sw = Stopwatch.StartNew();
        _input = ParseInput();
        sw.Stop();
        ParsingTime = sw.ElapsedMilliseconds;
    }

    public Day02(string input) : base(input)
    {
        _input = ParseInput();
    }

    protected override long ParsingTime { get; }


    public override string SolvePart1()
    {
        return _input
            .Aggregate(0, (cumulative, next) => cumulative + LookupTable[next])
            .ToString();
    }

    public override string SolvePart2()
    {
        return _input
            .Aggregate(0, (cumulative, next) => cumulative + LookupTable2[next])
            .ToString();
    }
}