using Shouldly;

namespace CaptainCoder.AdventOfCode;

public class Test
{
    public const string SampleInput = """
    190: 10 19
    3267: 81 40 27
    83: 17 5
    156: 15 6
    7290: 6 8 6 15
    161011: 16 10 13
    192: 17 8 14
    21037: 9 7 18 13
    292: 11 6 16 20
    """;

    [Fact]
    public void TestInputPart1()
    {
        string input = SampleInput;
        input.Trim().ShouldNotBe("NOT SET");
        Puzzle puzzle = new Puzzle(input);
        puzzle.Part1().ShouldBe("3749");
    }

    
    // 3267: 81 40 27
    // 83: 17 5
    // 156: 15 6
    // 7290: 6 8 6 15
    // 161011: 16 10 13
    // 192: 17 8 14
    // 21037: 9 7 18 13
    // 292: 11 6 16 20
    [Fact]
    public void TestEquationParser()
    {
        string input = "21037: 9 7 18 13";
        var eq = CalibrationEquation.Parse(input);
        eq.TestValue.ShouldBe(21037);
        eq.Values.ShouldBe([9, 7, 18, 13]);
    }

    [Fact]
    public void FinalPart1()
    {
        Puzzle puzzle = Puzzle.FromFile("inputs/puzzle-input.txt");
        puzzle.Part1().ShouldBe("945512582195");
    }

    [Fact]
    public void TestInputPart2()
    {
        string input = SampleInput;
        input.Trim().ShouldNotBe("NOT SET");
        Puzzle puzzle = new Puzzle(input);
        puzzle.Part2().ShouldBe("11387");
    }

    [Fact]
    public void FinalPart2()
    {
        Puzzle puzzle = Puzzle.FromFile("inputs/puzzle-input.txt");
        puzzle.Part2().ShouldBe("271691107779347");
    }
}
