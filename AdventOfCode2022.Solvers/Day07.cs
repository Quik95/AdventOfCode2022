namespace AdventOfCode2022.Solvers;

public sealed class Day07 : BaseDay
{
    private readonly Entry _rootElement;

    public Day07()
    {
        _rootElement = ParseInput();
    }

    public Day07(string input) : base(input)
    {
        _rootElement = ParseInput();
    }

    private new Entry ParseInput()
    {
        var lines = RawInput.Split(Environment.NewLine);
        var current = new Entry(lines[0].TrimStart("$ cd "), new List<Entry>(), null);
        var rootElement = current;

        var linesIndex = 1;
        while (linesIndex < lines.Length)
        {
            var line = lines[linesIndex];
            if (line.StartsWith("$ cd"))
            {
                var destination = line.TrimStart("$ cd ");
                current = destination switch
                {
                    ".." => current!.Parent,
                    "/" => rootElement,
                    _ => current!.Children!.First(child => child.Name == destination)
                };
                linesIndex++;
            }
            else if (line.StartsWith("$ ls"))
            {
                linesIndex++;
                line = lines[linesIndex];
                while (!line.StartsWith("$"))
                {
                    if (line.StartsWith("dir"))
                    {
                        current!.Children!.Add(new Entry(line.TrimStart("dir "), new List<Entry>(), current));
                    }
                    else
                    {
                        var parts = line.Split(" ");
                        current!.Children!.Add(new Entry(parts[1], null, current, int.Parse(parts[0])));
                    }

                    linesIndex++;
                    if (linesIndex > lines.Length - 1) break;
                    line = lines[linesIndex];
                }
            }
        }

        rootElement.GetSize();
        return rootElement;
    }

    public override int SolvePart1()
    {
        var directories = new List<Entry>();
        _rootElement.GetAllDirectories(ref directories);

        var res = directories.Where(dir => dir.Size! <= 100_000).Sum(dir => dir.Size!);
        return res!.Value;
    }

    public override int SolvePart2()
    {
        const int totalSpace = 70_000_000;
        const int minimumFreeSpace = 30_000_000;
        var currentFreeSpace = totalSpace - _rootElement.Size!.Value;

        var directories = new List<Entry>();
        _rootElement.GetAllDirectories(ref directories);

        return directories.Where(dir => currentFreeSpace + dir.Size!.Value >= minimumFreeSpace)
            .Min(dir => dir.Size!.Value);
    }
}

public static class StringExtension
{
    public static string TrimStart(this string str, string toBeTrimmed)
    {
        return str.Remove(0, toBeTrimmed.Length);
    }
}

public class Entry
{
    public Entry(string name, List<Entry>? children, Entry? parent, int? size = null)
    {
        Name = name;
        Children = children;
        Size = size;
        Parent = parent;
    }

    public string Name { get; }
    public List<Entry>? Children { get; }
    public int? Size { get; set; }
    public Entry? Parent { get; set; }

    public int GetSize()
    {
        if (Size.HasValue) return Size.Value;

        var size = Children!.Sum(child => child.GetSize());

        Size = size;
        return size;
    }

    public void GetAllDirectories(ref List<Entry> accumulator)
    {
        if (Children is null) return;

        accumulator.Add(this);
        foreach (var child in Children) child.GetAllDirectories(ref accumulator);
    }
}