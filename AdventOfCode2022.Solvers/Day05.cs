using System.Diagnostics;
using System.Text.RegularExpressions;
using MoreLinq.Extensions;

namespace AdventOfCode2022.Solvers;

public sealed class Day05 : BaseDay
{
    private readonly Regex _moveRegex = new(@"move (\d+) from (\d+) to (\d+)");
    private readonly Move[] _moves;

    private Stack<char>[] _stacks;

    public Day05()
    {
        var sw = Stopwatch.StartNew();
        (_stacks, _moves) = ParseInput();
        sw.Stop();
        ParsingTime = sw.ElapsedMilliseconds;
    }

    public Day05(string input) : base(input)
    {
        (_stacks, _moves) = ParseInput();
    }

    protected override long ParsingTime { get; }

    private new (Stack<char>[], Move[]) ParseInput()
    {
        var lines = RawInput.Split(Environment.NewLine).ToArray();

        var crates = lines
            .Where(line => line.Contains('['))
            .SelectMany(x =>
                x.Chunk(4)
                    .Select((chunk, stackIndex) => (chunk[1], stackIndex + 1))
                    .Where(t => t.Item1 != ' ')
            ).ToArray();
        var numberOfStacks = crates.Select(crate => crate.Item2).Max();
        var stacks = Enumerable.Range(1, numberOfStacks).Select(_ => new Stack<char>()).ToArray();

        foreach (var (crate, stackIndex) in crates.Reverse()) stacks[stackIndex - 1].Push(crate);

        var moves = lines.SkipWhile(line => !line.StartsWith("move")).Select(line =>
        {
            var captureGroups = _moveRegex.Match(line).Groups;
            return new Move(
                int.Parse(captureGroups[1].Captures[0].Value),
                int.Parse(captureGroups[2].Captures[0].Value),
                int.Parse(captureGroups[3].Captures[0].Value)
            );
        }).ToArray();


        return (stacks, moves);
    }


    public override string SolvePart1()
    {
        var stackCopy = _stacks.Select(stack => new Stack<char>(stack.Reverse())).ToArray();
        foreach (var move in _moves)
        {
            var from = _stacks[move.From - 1];
            var to = _stacks[move.To - 1];
            for (var i = 0; i < move.Quantity; i++) to.Push(from.Pop());
        }

        var res = string.Join("", _stacks.Select(stack => stack.Pop()).ToArray());
        _stacks = stackCopy;

        return res;
    }

    public override string SolvePart2()
    {
        foreach (var move in _moves)
        {
            var from = _stacks[move.From - 1];
            var to = _stacks[move.To - 1];
            Enumerable.Range(1, move.Quantity)
                .Select(_ => from.Pop())
                .Reverse()
                .ForEach(crate => to.Push(crate));
        }

        return string.Join("", _stacks.Select(stack => stack.Pop()).ToArray());
    }

    private record struct Move(int Quantity, int From, int To);
}