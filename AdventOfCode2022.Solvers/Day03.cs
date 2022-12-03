using static MoreLinq.Extensions.BatchExtension;

namespace AdventOfCode2022.Solvers;

public sealed class Day03 : BaseDay
{
    private readonly string[] _input;

    public Day03()
    {
        _input = ParseInput();
    }

    public Day03(string rawInput) : base(rawInput)
    {
        _input = ParseInput();
    }

    private static int CalculateBadgeWeight(char badge)
    {
        return char.IsAsciiLetterUpper(badge) ? badge - 'A' + 27 : badge - 'a' + 1;
    }

    public override int SolvePart1()
    {
        return _input
            .Select(
                backpack =>
                    backpack[..(backpack.Length / 2)].ToCharArray().Intersect(
                        backpack[(backpack.Length / 2)..].ToCharArray()
                    )
            ).Select(enumerable => enumerable.First())
            .Aggregate(0, (cumulative, next) =>
                cumulative + CalculateBadgeWeight(next)
            );
    }

    public override int SolvePart2()
    {
        return _input
            .Batch(3)
            .Select(group => group.Select(elf => elf.ToCharArray()))
            .Select(group => group.ToArray())
            .Select(group => group[0].Intersect(group[1].Intersect(group[2])))
            .Select(enumerable => enumerable.First())
            .Aggregate(0, (cumulative, next) => cumulative + CalculateBadgeWeight(next));
    }
}