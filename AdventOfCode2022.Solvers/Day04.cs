using System.Diagnostics;

namespace AdventOfCode2022.Solvers;

public sealed class Day04 : BaseDay
{
    private readonly Range[] _input;

    public Day04()
    {
        var sw = Stopwatch.StartNew();
        _input = ParseInput();
        sw.Stop();
        ParsingTime = sw.ElapsedMilliseconds;
    }

    public Day04(string input) : base(input)
    {
        _input = ParseInput();
    }

    protected override long ParsingTime { get; }

    private new Range[] ParseInput()
    {
        return RawInput
            .Split(Environment.NewLine)
            .Select(pair => pair.Split(','))
            .SelectMany(pair => new[]
            {
                new Range(pair[0].Split('-')),
                new Range(pair[1].Split('-'))
            })
            .ToArray();
    }


    public override int SolvePart1()
    {
        return _input.Chunk(2).Count(pair => pair[0].Contains(pair[1]) || pair[1].Contains(pair[0]));
    }

    public override int SolvePart2()
    {
        return _input.Chunk(2).Count(pair => pair[0].Overlaps(pair[1]));
    }

    private readonly record struct Range(int Start, int End)
    {
        public Range(IReadOnlyList<string> pair) : this(int.Parse(pair[0]), int.Parse(pair[1]))
        {
        }

        public bool Contains(Range other)
        {
            return other.Start >= Start && other.End <= End;
        }

        public bool Overlaps(Range other)
        {
            return other.Start <= End && other.End >= Start;
        }
    }
}