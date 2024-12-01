using Shouldly;

namespace CaptainCoder.AdventOfCode;

public class Test
{
    [Fact]
    public void TestInput()
    {
        string input = """
        3   4
        4   3
        2   5
        1   3
        3   9
        3   3
        """;
        Puzzle puzzle = new Puzzle(input);
        puzzle.Part1().ShouldBe("11");
    }

    [Fact]
    public void Part2TestInput()
    {
        string input = """
        3   4
        4   3
        2   5
        1   3
        3   9
        3   3
        """;
        Puzzle puzzle = new Puzzle(input);
        puzzle.Part2().ShouldBe("31");
    }

    [Fact]
    public void SanitizeInput()
    {
        string input = """
        3   4
        4   3
        2   5
        1   3
        3   9
        3   3
        """;
        (List<int> first, List<int> second) = Puzzle.SanitizeInput(input);
        first.Count.ShouldBe(6);
        second.Count.ShouldBe(6);
        first[0].ShouldBe(3);
        second[0].ShouldBe(4);
        first[1].ShouldBe(4);
        second[1].ShouldBe(3);
        first[2].ShouldBe(2);
        second[2].ShouldBe(5);
        first[3].ShouldBe(1);
        second[3].ShouldBe(3);
        first[4].ShouldBe(3);
        second[4].ShouldBe(9);
        first[5].ShouldBe(3);
        second[5].ShouldBe(3);
    }

    [Fact]
    public void Final()
    {
        Puzzle puzzle = Puzzle.FromFile("inputs/puzzle-input.txt");
        puzzle.Part1().ShouldBe("1882714");
    }

    [Fact]
    public void FinalPart2()
    {
        Puzzle puzzle = Puzzle.FromFile("inputs/puzzle-input.txt");
        puzzle.Part2().ShouldBe("19437052");
    }
}
