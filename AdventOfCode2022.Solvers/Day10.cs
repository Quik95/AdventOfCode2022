using System.Diagnostics;
using System.Text;

namespace AdventOfCode2022.Solvers;

public sealed class Day10 : BaseDay
{
    private readonly string[] _instructions;

    public Day10()
    {
        var sw = Stopwatch.StartNew();
        _instructions = ParseInput();
        sw.Stop();
        ParsingTime = sw.ElapsedMilliseconds;
    }

    public Day10(string input) : base(input)
    {
        var sw = Stopwatch.StartNew();
        _instructions = ParseInput();
        sw.Stop();
        ParsingTime = sw.ElapsedMilliseconds;
    }

    protected override long ParsingTime { get; }

    public override string SolvePart1()
    {
        var cycleNumber = 1;
        var strengths = new List<int>();
        var xRegister = 1;

        foreach (var instruction in _instructions)
        {
            UpdateSignalStrengths(ref strengths, cycleNumber, xRegister);

            if (instruction == "noop")
            {
                cycleNumber++;
                continue;
            }

            cycleNumber++;

            UpdateSignalStrengths(ref strengths, cycleNumber, xRegister);

            var value = int.Parse(instruction["addx ".Length..]);
            xRegister += value;
            cycleNumber++;
        }

        return strengths.Sum().ToString();
    }

    private static void UpdateSignalStrengths(ref List<int> strengths, int cycleNumber, int xRegister)
    {
        if (cycleNumber == 20 || (cycleNumber - 20) % 40 == 0) strengths.Add(cycleNumber * xRegister);
    }

    public override string SolvePart2()
    {
        var display = Enumerable.Range(1, 240).Select(_ => '░').ToArray();

        var cycleNumber = 1;
        var spritePosition = 1;

        foreach (var instruction in _instructions)
        {
            CRTDraw(ref display, cycleNumber, spritePosition);
            if (instruction == "noop")
            {
                cycleNumber++;
                continue;
            }

            cycleNumber++;

            CRTDraw(ref display, cycleNumber, spritePosition);

            var value = int.Parse(instruction["addx ".Length..]);
            spritePosition += value;
            cycleNumber++;
        }

        return Environment.NewLine +
               display
                   .Chunk(40)
                   .Aggregate(
                       new StringBuilder(240),
                       (s, row) => s.Append(Environment.NewLine).Append(row)
                   );
    }

    private void CRTDraw(ref char[] display, int cycleNumber, int spritePosition)
    {
        var drawingPosition = cycleNumber % 40 - 1;
        if (Math.Abs(spritePosition - drawingPosition) > 1) return;
        var index = cycleNumber - 1;
        display[index] = '▓';
    }
}