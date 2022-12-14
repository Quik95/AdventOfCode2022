using System.Diagnostics;

namespace AdventOfCode2022.Solvers;

public sealed class Day14 : BaseDay
{
    private readonly Dictionary<(int X, int Y), BlockType> _initialMap = new();
    private readonly int _rockBottom;
    private readonly (int X, int Y) _sandPouringPoint = (500, 0);

    public Day14()
    {
        var sw = Stopwatch.GetTimestamp();
        (_initialMap, _rockBottom) = ParseInput();
        ParsingTime = Stopwatch.GetElapsedTime(sw).Milliseconds;
    }

    public Day14(string input) : base(input)
    {
        var sw = Stopwatch.GetTimestamp();
        (_initialMap, _rockBottom) = ParseInput();
        ParsingTime = Stopwatch.GetElapsedTime(sw).Milliseconds;
    }

    protected override long ParsingTime { get; }

    private new (Dictionary<(int X, int Y), BlockType>, int) ParseInput()
    {
        var lines = RawInput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
                line.Split(" -> ")
                    .Select(path => (
                            X: int.Parse(path.Split(',')[0]),
                            Y: int.Parse(path.Split(",")[1])
                        )
                    ).ToArray()
            ).ToArray();

        var dict = new Dictionary<(int X, int Y), BlockType>();
        foreach (var paths in lines)
        {
            var position = paths[0];
            dict[position] = BlockType.Rock;

            foreach (var path in paths.Skip(1))
            {
                var (x, y) = position;
                var (dx, dy) = (path.X - x, path.Y - y);
                while (x != path.X || y != path.Y)
                {
                    x += 1 * Math.Sign(dx);
                    y += 1 * Math.Sign(dy);
                    dict[(x, y)] = BlockType.Rock;
                }

                position = (x, y);
            }
        }

        return (dict, lines.SelectMany(line => line.Select(path => path.Y)).Max() + 2);
    }

    private BlockType GetBlockAtPosition(IReadOnlyDictionary<(int X, int Y), BlockType> grid, (int X, int Y) target)
    {
        return target.Y == _rockBottom ? BlockType.Rock : grid.GetValueOrDefault(target, BlockType.Air);
    }

    private static void PrintMap(Dictionary<(int X, int Y), BlockType> map)
    {
        var minX = map.Keys.Min(key => key.X);
        var maxX = map.Keys.Max(key => key.X);
        var minY = map.Keys.Min(key => key.Y);
        var maxY = map.Keys.Max(key => key.Y);

        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                var block = map.TryGetValue((x, y), out var blockType) ? blockType : BlockType.Air;
                Console.Write(block switch
                {
                    BlockType.Rock => '#',
                    BlockType.Air => '.',
                    BlockType.Sand => 'o',
                    _ => throw new ArgumentOutOfRangeException()
                });
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    public override string SolvePart1()
    {
        var grid = new Dictionary<(int X, int Y), BlockType>(_initialMap);
        var currentLocation = _sandPouringPoint;
        var i = 0;

        while (currentLocation.Y < _rockBottom - 2)
        {
            // PrintMap(grid);
            var (x, y) = currentLocation;
            grid.Remove(currentLocation);
            if (GetBlockAtPosition(grid, (x, y + 1)) == BlockType.Air)
            {
                currentLocation = (x, y + 1);
                grid[currentLocation] = BlockType.Sand;
            }
            else
            {
                if (GetBlockAtPosition(grid, (x - 1, y + 1)) == BlockType.Air)
                {
                    currentLocation = (x - 1, y + 1);
                    grid[currentLocation] = BlockType.Sand;
                }
                else if (GetBlockAtPosition(grid, (x + 1, y + 1)) == BlockType.Air)
                {
                    currentLocation = (x + 1, y + 1);
                    grid[currentLocation] = BlockType.Sand;
                }
                else
                {
                    grid[currentLocation] = BlockType.Sand;
                    currentLocation = _sandPouringPoint;
                    i++;
                }
            }
        }

        return i.ToString();
    }

    public override string SolvePart2()
    {
        var grid = new Dictionary<(int X, int Y), BlockType>(_initialMap);
        var currentLocation = _sandPouringPoint;
        var i = 0;

        while (true)
        {
            var (x, y) = currentLocation;
            grid.Remove(currentLocation);
            if (GetBlockAtPosition(grid, (x, y + 1)) == BlockType.Air)
            {
                currentLocation = (x, y + 1);
                grid[currentLocation] = BlockType.Sand;
            }
            else
            {
                if (GetBlockAtPosition(grid, (x - 1, y + 1)) == BlockType.Air)
                {
                    currentLocation = (x - 1, y + 1);
                    grid[currentLocation] = BlockType.Sand;
                }
                else if (GetBlockAtPosition(grid, (x + 1, y + 1)) == BlockType.Air)
                {
                    currentLocation = (x + 1, y + 1);
                    grid[currentLocation] = BlockType.Sand;
                }
                else
                {
                    if (currentLocation == _sandPouringPoint) break;
                    grid[currentLocation] = BlockType.Sand;
                    currentLocation = _sandPouringPoint;
                    i++;
                }
            }
        }

        return (i + 1).ToString();
    }

    private enum BlockType
    {
        Rock = '#',
        Air = '.',
        Sand = 'o'
    }
}