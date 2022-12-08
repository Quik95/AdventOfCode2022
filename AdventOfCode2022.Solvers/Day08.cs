using System.Diagnostics;

namespace AdventOfCode2022.Solvers;

public sealed class Day08 : BaseDay
{
    private readonly Coord[] _cardinalDirections = {new(-1, 0), new(1, 0), new(0, -1), new(0, 1)};

    private readonly Dictionary<Coord, int> _grid;

    public Day08()
    {
        var sw = Stopwatch.StartNew();
        _grid = ParseInput();
        sw.Stop();
        ParsingTime = sw.ElapsedMilliseconds;
    }

    public Day08(string input) : base(input)
    {
        _grid = ParseInput();
    }

    protected override long ParsingTime { get; }

    private new Dictionary<Coord, int> ParseInput()
    {
        var lines = RawInput.Split(Environment.NewLine);
        var grid = new Dictionary<Coord, int>();
        for (var rowIndex = 0; rowIndex < lines.Length; rowIndex++)
        for (var columnIndex = 0; columnIndex < lines[0].Length; columnIndex++)
            grid.Add(new Coord(columnIndex, rowIndex), lines[rowIndex][columnIndex] - '0');

        return grid;
    }


    public override int SolvePart1()
    {
        return _grid.Count(tree =>
            _cardinalDirections.Any(direction =>
            {
                if (tree.Key.X == 0 || tree.Key.Y == 0)
                    return true;
                var treeHeight = tree.Value;
                var adjacent = new Coord(tree.Key.X + direction.X, tree.Key.Y + direction.Y);
                while (_grid.ContainsKey(adjacent))
                {
                    if (treeHeight <= _grid[adjacent]) return false;
                    adjacent = new Coord(adjacent.X + direction.X, adjacent.Y + direction.Y);
                }

                return true;
            })
        );
    }

    public override int SolvePart2()
    {
        return _grid.Max(entry =>
            _cardinalDirections.Aggregate(1, (cumulative, direction) =>
            {
                if (entry.Key.X == 0 || entry.Key.Y == 0)
                    return 0;

                var treeHeight = entry.Value;
                var adjacent = new Coord(entry.Key.X + direction.X, entry.Key.Y + direction.Y);
                var viewingDistance = 0;
                while (_grid.ContainsKey(adjacent))
                {
                    viewingDistance++;
                    if (_grid[adjacent] >= treeHeight) break;
                    adjacent = new Coord(adjacent.X + direction.X, adjacent.Y + direction.Y);
                }

                return viewingDistance * cumulative;
            })
        );
    }

    private record struct Coord(int X, int Y);
}