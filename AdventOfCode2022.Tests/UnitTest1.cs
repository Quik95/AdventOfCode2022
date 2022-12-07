using AdventOfCode2022.Solvers;

namespace AdventOfCode2022.Tests;

public class AdventOfCodeTests
{
    public class Day02Tests
    {
        [Fact]
        public void Part1Example()
        {
            var d = new Day02("A Y\nB X\nC Z");
            Assert.Equal(15, d.SolvePart1());
        }

        [Fact]
        public void Part2Example()
        {
            var d = new Day02("A Y\nB X\nC Z");
            Assert.Equal(12, d.SolvePart2());
        }
    }

    public class Day03Tests
    {
        [Fact]
        public void Part1Example()
        {
            var d = new Day03(
                "vJrwpWtwJgWrhcsFMMfFFhFp\n" +
                "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL\n" +
                "PmmdzqPrVvPwwTWBwg\n" +
                "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn\n" +
                "ttgJtRGJQctTZtZT\n" +
                "CrZsJsPPZsGzwwsLwLmpwMDw"
            );
            Assert.Equal(157, d.SolvePart1());
            Assert.Equal(70, d.SolvePart2());
        }
    }

    public class Day04Tests
    {
        [Fact]
        public void Part1Example()
        {
            var d = new Day04(
                "2-4,6-8\n" +
                "2-3,4-5\n" +
                "5-7,7-9\n" +
                "2-8,3-7\n" +
                "6-6,4-6\n" +
                "2-6,4-8"
            );
            Assert.Equal(2, d.SolvePart1());
            Assert.Equal(4, d.SolvePart2());
        }
    }

    public class Day05Tests
    {
        [Fact]
        public void CargoParseTest()
        {
            var d = new Day05(
                "    [D]    \n" +
                "[N] [C]    \n" +
                "[Z] [M] [P]\n" +
                "move 1 from 2 to 1\n" +
                "move 3 from 1 to 3\n" +
                "move 2 from 2 to 1\n" +
                "move 1 from 1 to 2"
            );
            Assert.Equal("CMZ", d.SolvePart1String());
            Assert.Equal("MCD", d.SolvePart2String());
        }
    }

    public class Day06Tests
    {
        [Fact]
        public void Part1Example()
        {
            var d = new Day06("mjqjpqmgbljsphdztnvjfqwrcgsmlb");
            Assert.Equal(7, d.SolvePart1());
            Assert.Equal(19, d.SolvePart2());

            d = new Day06("bvwbjplbgvbhsrlpgdmjqwftvncz");
            Assert.Equal(5, d.SolvePart1());
            Assert.Equal(23, d.SolvePart2());
        }
    }

    public class Day07Tests
    {
        [Fact]
        public void Part1Example()
        {
            var d = new Day07(
                "$ cd /\n" +
                "$ ls\n" +
                "dir a\n" +
                "14848514 b.txt\n" +
                "8504156 c.dat\n" +
                "dir d\n" +
                "$ cd a\n" +
                "$ ls\n" +
                "dir e\n" +
                "29116 f\n" +
                "2557 g\n" +
                "62596 h.lst\n" +
                "$ cd e\n" +
                "$ ls\n" +
                "584 i\n" +
                "$ cd ..\n" +
                "$ cd ..\n" +
                "$ cd d\n" +
                "$ ls\n" +
                "4060174 j\n" +
                "8033020 d.log\n" +
                "5626152 d.ext\n" +
                "7214296 k"
            );
            Assert.Equal(95437, d.SolvePart1());
            Assert.Equal(24933642, d.SolvePart2());
        }
    }
}