namespace AdventOfCode2022.Solvers;

public sealed class Day01 : BaseDay
{
    private readonly int[][] _input;

    public Day01()
    {
        _input = ParseInput(RawInput);
    }

    private static int[][] ParseInput(string inp)
    {
        return inp.Split("\n\n")
            .Select(elf =>
                elf.Split(Environment.NewLine)
                    .Select(int.Parse)
                    .ToArray())
            .ToArray();
    }

    public override int SolvePart1()
    {
        return _input.Select(elf => elf.Sum()).Max();
    }

    public override int SolvePart2()
    {
        return _input.Select(elf => elf.Sum()).OrderDescending().Take(3).Sum();
    }
}