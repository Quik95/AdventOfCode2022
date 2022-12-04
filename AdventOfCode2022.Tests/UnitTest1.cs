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
}