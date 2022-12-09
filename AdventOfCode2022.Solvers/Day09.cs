using System.Diagnostics;

namespace AdventOfCode2022.Solvers;

public sealed class Day09 : BaseDay
{
    private readonly string[] _commands;

    public Day09()
    {
        var sw = Stopwatch.StartNew();
        _commands = ParseInput();
        sw.Stop();
        ParsingTime = sw.ElapsedMilliseconds;
    }

    public Day09(string input) : base(input)
    {
        var sw = Stopwatch.StartNew();
        _commands = ParseInput();
        sw.Stop();
        ParsingTime = sw.ElapsedMilliseconds;
    }

    protected override long ParsingTime { get; }

    public override int SolvePart1()
    {
        var headPosition = new Coord(0, 0);
        var tailPosition = new Coord(0, 0);
        var visitedByTail = new HashSet<Coord> {new(0, 0)};

        foreach (var command in _commands)
        {
            var (direction, amount) = (command[0], int.Parse(command[2..]));
            var (dx, dy) = direction switch
            {
                'U' => (0, 1),
                'R' => (1, 0),
                'L' => (-1, 0),
                'D' => (0, -1),
                _ => throw new UnreachableException($"Invalid direction: {direction}")
            };

            for (var i = 0; i < amount; i++)
            {
                headPosition = new Coord(headPosition.X + dx, headPosition.Y + dy);
                var (headTaildx, headTaildy) = (Math.Abs(headPosition.X - tailPosition.X),
                    Math.Abs(headPosition.Y - tailPosition.Y));
                if (headTaildy <= 1 && headTaildx <= 1) continue;

                tailPosition = new Coord(headPosition.X - dx, headPosition.Y - dy);
                visitedByTail.Add(tailPosition);
            }
        }

        return visitedByTail.Count;
    }

    public override int SolvePart2()
    {
        var segmentsPositions = new[] {new Coord(0, 0)}
            .Concat(Enumerable.Range(1, 9).Select(_ => new Coord(0, 0))).ToArray();
        var visitedByTail = new HashSet<Coord> {new(0, 0)};

        foreach (var command in _commands)
        {
            var (direction, amount) = (command[0], int.Parse(command[2..]));
            var (dx, dy) = direction switch
            {
                'U' => (0, 1),
                'R' => (1, 0),
                'L' => (-1, 0),
                'D' => (0, -1),
                _ => throw new UnreachableException($"Invalid direction: {direction}")
            };

            for (var i = 0; i < amount; i++)
            {
                var head = segmentsPositions[0];
                segmentsPositions[0] = new Coord(head.X + dx, head.Y + dy);

                foreach (var (s, j) in segmentsPositions.Select((t, j) => (t, j)).Skip(1))
                {
                    var prevSegment = segmentsPositions[j - 1];
                    var (segments_dx, segments_dy) = (
                        s.X - prevSegment.X,
                        s.Y - prevSegment.Y
                    );
                    if (Math.Abs(segments_dx) <= 1 && Math.Abs(segments_dy) <= 1) break;

                    segmentsPositions[j] = new Coord(s.X - Math.Clamp(segments_dx, -1, 1),
                        s.Y - Math.Clamp(segments_dy, -1, 1));
                    if (j == 9)
                        visitedByTail.Add(segmentsPositions[j]);
                }
            }
        }

        return visitedByTail.Count;
    }

    private void PrintBoard(Coord[] segments)
    {
        var minX = -10;
        var maxX = 10;
        var minY = -10;
        var maxY = 10;

        Console.WriteLine();
        for (var y = maxY; y >= minY; y--)
        {
            for (var x = minX; x <= maxX; x++)
            {
                var coord = new Coord(x, y);
                Console.Write(segments.Contains(coord) ? Array.FindIndex(segments, a => a == coord) : ".");
            }

            Console.WriteLine();
        }
    }

    private sealed record Coord(int X, int Y);
}