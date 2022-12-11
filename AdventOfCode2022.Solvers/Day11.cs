using System.Diagnostics;
using TrimStart = AdventOfCode2022.Solvers.StringExtensions;

namespace AdventOfCode2022.Solvers;

public class Day11 : BaseDay
{
    private readonly Monkey[] _monkeys;

    public Day11()
    {
        var sw = Stopwatch.StartNew();
        _monkeys = ParseInput();
        sw.Stop();
        ParsingTime = sw.ElapsedMilliseconds;
    }

    public Day11(string input) : base(input)
    {
        var sw = Stopwatch.StartNew();
        _monkeys = ParseInput();
        sw.Stop();
        ParsingTime = sw.ElapsedMilliseconds;
    }

    protected override long ParsingTime { get; }

    private new Monkey[] ParseInput()
    {
        return RawInput
            .Split(Environment.NewLine + Environment.NewLine)
            .Select(monke => monke.Split(Environment.NewLine))
            .Select(monke => new Monkey(
                monke[1].Trim().TrimStart("Starting items: ").Split(", ").Select(long.Parse).ToList(),
                monke[2].Trim().TrimStart("Operation: new = "),
                int.Parse(monke[3].Trim().TrimStart("Test: divisible by ")),
                int.Parse(monke[4].Trim().TrimStart("If true: throw to monkey ")),
                int.Parse(monke[5].Trim().TrimStart("If false: throw to monkey "))
            )).ToArray();
    }

    private static void SimulateRound(IReadOnlyList<Monkey> monkeys, int product, bool divideByThree)
    {
        foreach (var monkey in monkeys)
            while (monkey.Items.Count > 0)
            {
                var item = monkey.Items.First();
                monkey.Items.RemoveAt(0);
                var multiplier = monkey.Operation.EndsWith("old") ? item : int.Parse(monkey.Operation[6..]);
                var newWorryLevel = monkey.Operation[4] switch
                {
                    '*' => item * multiplier / (divideByThree ? 3 : 1) % product,
                    '+' => (item + multiplier) / (divideByThree ? 3 : 1) % product,
                    _ => throw new UnreachableException()
                };
                var target = newWorryLevel % monkey.Test == 0 ? monkey.TargetIfTrue : monkey.TargetIfFalse;
                monkeys[target].Items.Add(newWorryLevel);
                monkey.InspectedItems++;
            }
    }

    public override string SolvePart1()
    {
        var monkeys = _monkeys.Select(m => m.DeepClone()).ToArray();
        const int numberOfRounds = 20;
        var product = monkeys.Aggregate(1, (i, m) => i * m.Test);

        for (var i = 0; i < numberOfRounds; i++) SimulateRound(monkeys, product, true);

        return monkeys.OrderByDescending(m => m.InspectedItems).Take(2)
            .Aggregate(1, (cumulative, monkey) => cumulative * monkey.InspectedItems)
            .ToString();
    }

    public override string SolvePart2()
    {
        var monkeys = _monkeys.Select(m => m.DeepClone()).ToArray();
        const int numberOfRounds = 10_000;
        var product = monkeys.Aggregate(1, (i, m) => i * m.Test);

        for (var i = 0; i < numberOfRounds; i++) SimulateRound(monkeys, product, false);

        return monkeys.OrderByDescending(m => m.InspectedItems).Take(2)
            .Aggregate(1L, (cumulative, monkey) => cumulative * monkey.InspectedItems).ToString();
    }

    private record Monkey(List<long> Items, string Operation, int Test, int TargetIfTrue, int TargetIfFalse)
    {
        public int InspectedItems { get; set; }

        public Monkey DeepClone()
        {
            return this with {Items = new List<long>(Items)};
        }
    }
}