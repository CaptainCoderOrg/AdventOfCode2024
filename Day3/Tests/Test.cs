using System.Text.RegularExpressions;
using Shouldly;

namespace CaptainCoder.AdventOfCode;

public class Test
{
    [Fact]
    public void TestInputPart1()
    {
        string input = """
        xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
        """;
        input.Trim().ShouldNotBe("NOT SET");
        Puzzle puzzle = new Puzzle(input);
        puzzle.Part1().ShouldBe("161");
    }

    [Theory]
    [InlineData("mul(4,5)", true)]
    [InlineData("mul( 4 , 5 )", false)]
    [InlineData("mul(4*, mul(6,9!, ?(12,34)", false)]
    [InlineData("mul(123,321)", true)]
    [InlineData("mul(1234,321)", false)]
    [InlineData("mul (123,321)", false)]
    public void TestMulRegEx(string input, bool isMatch)
    {
        Match m = Puzzle.MulRegex().Match(input);
        m.Success.ShouldBe(isMatch);
    }

    [Fact]
    public void TestGroupNames()
    {
        string input = "mul(4,5)";
        Match match = Puzzle.MulRegex().Match(input);
        match.Groups["first"]?.Value.ShouldBe("4");
        match.Groups["second"]?.Value.ShouldBe("5");
    }

    [Fact]
    public void FinalPart1()
    {
        Puzzle puzzle = Puzzle.FromFile("inputs/puzzle-input.txt");
        puzzle.Part1().ShouldBe("175700056");
    }


    [Fact]
    public void TestInputPart2()
    {
        string input = """
        xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))
        """;
        input.Trim().ShouldNotBe("NOT SET");
        Puzzle puzzle = new Puzzle(input);
        puzzle.Part2().ShouldBe("48");
    }

    [Fact]
    public void FinalPart2()
    {
        Puzzle puzzle = Puzzle.FromFile("inputs/puzzle-input.txt");
        puzzle.Part2().ShouldBe("71668682");
    }
}
