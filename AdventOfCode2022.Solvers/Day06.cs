using MoreLinq.Extensions;

namespace AdventOfCode2022.Solvers;

public sealed class Day06 : BaseDay
{
    private readonly string _buffer;

    public Day06(string buffer) : base(buffer)
    {
        _buffer = RawInput;
    }

    public Day06()
    {
        _buffer = RawInput;
    }

    public override int SolvePart1()
    {
        const int windowSize = 4;
        return _buffer
            .Window(windowSize)
            .Select((window, i) => (window, i))
            .First(pair =>
                new HashSet<char>(pair.window).Count == pair.window.Count)
            .i + windowSize;
    }

    public override int SolvePart2()
    {
        const int windowSize = 14;
        return _buffer
            .Window(windowSize)
            .Select((window, i) => (window, i))
            .First(pair =>
                new HashSet<char>(pair.window).Count == pair.window.Count)
            .i + windowSize;
    }
}