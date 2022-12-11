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
            Assert.Equal("15", d.SolvePart1());
        }

        [Fact]
        public void Part2Example()
        {
            var d = new Day02("A Y\nB X\nC Z");
            Assert.Equal("12", d.SolvePart2());
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
            Assert.Equal("157", d.SolvePart1());
            Assert.Equal("70", d.SolvePart2());
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
            Assert.Equal("2", d.SolvePart1());
            Assert.Equal("4", d.SolvePart2());
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
            Assert.Equal("CMZ", d.SolvePart1());
            Assert.Equal("MCD", d.SolvePart2());
        }
    }

    public class Day06Tests
    {
        [Fact]
        public void Part1Example()
        {
            var d = new Day06("mjqjpqmgbljsphdztnvjfqwrcgsmlb");
            Assert.Equal("7", d.SolvePart1());
            Assert.Equal("19", d.SolvePart2());

            d = new Day06("bvwbjplbgvbhsrlpgdmjqwftvncz");
            Assert.Equal("5", d.SolvePart1());
            Assert.Equal("23", d.SolvePart2());
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
            Assert.Equal("95437", d.SolvePart1());
            Assert.Equal("24933642", d.SolvePart2());
        }
    }

    public class Day08Tests
    {
        [Fact]
        public void Part1Example()
        {
            var d = new Day08(
                "30373\n" +
                "25512\n" +
                "65332\n" +
                "33549\n" +
                "35390"
            );
            Assert.Equal("21", d.SolvePart1());
            Assert.Equal("8", d.SolvePart2());
        }
    }

    public class Day09Tests
    {
        [Fact]
        public void Part1Example()
        {
            var d = new Day09(
                "R 4\n" +
                "U 4\n" +
                "L 3\n" +
                "D 1\n" +
                "R 4\n" +
                "D 1\n" +
                "L 5\n" +
                "R 2"
            );
            Assert.Equal("13", d.SolvePart1());
        }

        [Fact]
        public void Part2Example()
        {
            var d = new Day09(
                "R 5\n" +
                "U 8\n" +
                "L 8\n" +
                "D 3\n" +
                "R 17\n" +
                "D 10\n" +
                "L 25\n" +
                "U 20"
            );
            Assert.Equal("36", d.SolvePart2());
        }
    }

    public class Day11Tests
    {
        [Fact]
        public void TestPart1Example()
        {
            var d = new Day11(
                "Monkey 0:\n" +
                "Starting items: 79, 98\n" +
                "Operation: new = old * 19\n" +
                "Test: divisible by 23\n" +
                "If true: throw to monkey 2\n" +
                "If false: throw to monkey 3\n" +
                "\n" +
                "Monkey 1:\n" +
                "Starting items: 54, 65, 75, 74\n" +
                "Operation: new = old + 6\n" +
                "Test: divisible by 19\n" +
                "If true: throw to monkey 2\n" +
                "If false: throw to monkey 0\n" +
                "\n" +
                "Monkey 2:\n" +
                "Starting items: 79, 60, 97\n" +
                "Operation: new = old * old\n" +
                "Test: divisible by 13\n" +
                "If true: throw to monkey 1\n" +
                "If false: throw to monkey 3\n" +
                "\n" +
                "Monkey 3:\n" +
                "Starting items: 74\n" +
                "Operation: new = old + 3\n" +
                "Test: divisible by 17\n" +
                "If true: throw to monkey 0\n" +
                "If false: throw to monkey 1"
            );
            Assert.Equal("10605", d.SolvePart1());
            Assert.Equal("2713310158", d.SolvePart2());
        }
    }
}