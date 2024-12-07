using Shouldly;

namespace CaptainCoder.AdventOfCode;

public class Test
{
    const string SampleInput = """
        ....#.....
        .........#
        ..........
        ..#.......
        .......#..
        ..........
        .#..^.....
        ........#.
        #.........
        ......#...
        """;
    [Fact]
    public void TestInputPart1()
    {
        string input = SampleInput;
        input.Trim().ShouldNotBe("NOT SET");
        Puzzle puzzle = new Puzzle(input);
        puzzle.Part1().ShouldBe("41");
    }

    [Fact]
    public void FinalPart1()
    {
        Puzzle puzzle = Puzzle.FromFile("inputs/puzzle-input.txt");
        puzzle.Part1().ShouldBe("5329");
    }

        [Fact]
    public void TestInputPart2()
    {
        string input = SampleInput;
        input.Trim().ShouldNotBe("NOT SET");
        Puzzle puzzle = new Puzzle(input);
        puzzle.Part2().ShouldBe("6");
    }

    [Fact]
    public void FinalPart2()
    {
        Puzzle puzzle = Puzzle.FromFile("inputs/puzzle-input.txt");
        puzzle.Part2().ShouldBe("2162");
    }
}
