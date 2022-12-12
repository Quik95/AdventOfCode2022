using System.Diagnostics;

namespace AdventOfCode2022.Solvers;

public sealed class Day12 : BaseDay
{
    private readonly (int X, int Y)[] _cardinalDirections =
    {
        (X: 0, Y: 1),
        (X: 1, Y: 0),
        (X: 0, Y: -1),
        (X: -1, Y: 0)
    };

    private readonly (int X, int Y) _destination;
    private readonly Dictionary<(int X, int Y), int> _relativeHeightMap;
    private readonly (int X, int Y) _startingLocation;

    public Day12(string input) : base(input)
    {
        var sw = Stopwatch.StartNew();
        (_startingLocation, _destination, _relativeHeightMap) = ParseInput();
        sw.Stop();
        ParsingTime = sw.ElapsedMilliseconds;
    }

    public Day12()
    {
        var sw = Stopwatch.StartNew();
        (_startingLocation, _destination, _relativeHeightMap) = ParseInput();
        sw.Stop();
        ParsingTime = sw.ElapsedMilliseconds;
    }

    protected override long ParsingTime { get; }

    private new ((int, int), (int, int), Dictionary<(int X, int Y), int>) ParseInput()
    {
        var lines = RawInput.Split(Environment.NewLine).ToArray();
        var r = lines
            .SelectMany((line, rowIndex) =>
                line.Select((c, columnIndex) =>
                    c is 'S' or 'E'
                        ? new KeyValuePair<(int X, int Y), int>((columnIndex, rowIndex), c)
                        : new KeyValuePair<(int, int), int>((columnIndex, rowIndex), c - 'a'))
            );
        var dict = new Dictionary<(int X, int Y), int>(r);
        var start = dict.First(kv => (char) kv.Value is 'S').Key;
        var end = dict.First(kv => (char) kv.Value is 'E').Key;

        dict[start] = 'a' - 'a';
        dict[end] = 'z' - 'a';

        return (start, end, dict);
    }

    private int DijkstraPart1((int X, int Y) startingPosition)
    {
        var dist = new Dictionary<(int, int), int>(
            _relativeHeightMap.Keys.Select(key => new KeyValuePair<(int, int), int>(key, int.MaxValue))
        ) {[startingPosition] = 0};

        var queue = new PriorityQueue<(int, int), int>();
        queue.Enqueue(startingPosition, 0);

        while (queue.TryDequeue(out (int X, int Y) position, out var cost))
        {
            if (position == _destination) return cost;

            if (cost > dist[position]) continue;

            foreach (var (dx, dy) in _cardinalDirections)
            {
                var next = (position.X + dx, position.Y + dy);
                var nextCost = cost + 1;

                if (!_relativeHeightMap.ContainsKey(next)) continue;
                if (_relativeHeightMap[next] - _relativeHeightMap[position] > 1) continue;
                if (nextCost >= dist[next]) continue;

                queue.Enqueue(next, nextCost);
                dist[next] = nextCost;
            }
        }

        return int.MaxValue;
    }

    private int DijkstraPart2((int X, int Y) startingPosition)
    {
        var dist = new Dictionary<(int, int), int>(
            _relativeHeightMap.Keys.Select(key => new KeyValuePair<(int, int), int>(key, int.MaxValue))
        ) {[startingPosition] = 0};

        var queue = new PriorityQueue<(int, int), int>();
        queue.Enqueue(startingPosition, 0);

        while (queue.TryDequeue(out (int X, int Y) position, out var cost))
        {
            if (_relativeHeightMap[position] == 0) return cost;

            if (cost > dist[position]) continue;

            foreach (var (dx, dy) in _cardinalDirections)
            {
                var next = (position.X + dx, position.Y + dy);
                var nextCost = cost + 1;

                if (!_relativeHeightMap.ContainsKey(next)) continue;
                if (_relativeHeightMap[position] - _relativeHeightMap[next] > 1) continue;
                if (nextCost >= dist[next]) continue;

                queue.Enqueue(next, nextCost);
                dist[next] = nextCost;
            }
        }

        return int.MaxValue;
    }

    public override string SolvePart1()
    {
        return DijkstraPart1(_startingLocation).ToString();
    }

    public override string SolvePart2()
    {
        return DijkstraPart2(_destination).ToString();
    }
}