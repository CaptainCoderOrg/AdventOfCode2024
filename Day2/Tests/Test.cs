using System.Runtime.InteropServices;
using Shouldly;

namespace CaptainCoder.AdventOfCode;

public class Test
{
    [Fact]
    public void TestInputPart1()
    {
        string input = """
        7 6 4 2 1
        1 2 7 8 9
        9 7 6 2 1
        1 3 2 4 5
        8 6 4 4 1
        1 3 6 7 9
        """;
        Puzzle puzzle = new Puzzle(input);
        puzzle.Part1().ShouldBe("2");
    }

    [Theory]
    [InlineData("7 6 4 2 1", true)]
    [InlineData("1 2 7 8 9", false)]
    [InlineData("9 7 6 2 1", false)]
    [InlineData("1 3 2 4 5", false)]
    [InlineData("8 6 4 4 1", false)]
    [InlineData("1 3 6 7 9", true)]
    public void TestIsSafe(string input, bool isSafe)
    {
        Puzzle.IsSafe(input).ShouldBe(isSafe);
    }

    [Fact]
    public void FinalPart1()
    {
        Puzzle puzzle = Puzzle.FromFile("inputs/puzzle-input.txt");
        puzzle.Part1().ShouldBe("332");
    }

    [Fact]
    public void TestInputPart2()
    {
        string input = """

        """;
        Puzzle puzzle = new Puzzle(input);
        puzzle.Part2().ShouldBe("Some answer");
    }

    [Fact]
    public void FinalPart2()
    {
        Puzzle puzzle = Puzzle.FromFile("inputs/puzzle-input.txt");
        puzzle.Part2().ShouldBe("Answer Unknown");
    }
}
