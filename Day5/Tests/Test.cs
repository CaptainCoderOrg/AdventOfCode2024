using Shouldly;

namespace CaptainCoder.AdventOfCode;

public class Test
{
    public const string SampleInput = 
        """
        47|53
        97|13
        97|61
        97|47
        75|29
        61|13
        75|53
        29|13
        97|29
        53|29
        61|53
        97|53
        61|29
        47|13
        75|47
        97|75
        47|61
        75|61
        47|29
        75|13
        53|13

        75,47,61,53,29
        97,61,53,29,13
        75,29,13
        75,97,47,61,53
        61,13,29
        97,13,75,29,47
        """;
    [Fact]
    public void TestInputPart1()
    {
        SampleInput.Trim().ShouldNotBe("NOT SET");
        Puzzle puzzle = new Puzzle(SampleInput);
        puzzle.Part1().ShouldBe("143");
    }

    [Fact]
    public void TestIsOrdered()
    {

        // 75,47,61,53,29
        // 97,61,53,29,13
        // 75,29,13
        // 75,97,47,61,53
        // 61,13,29
        // 97,13,75,29,47   
        Puzzle puzzle = new (SampleInput);
        puzzle.IsOrdered(puzzle.ListOfManuals[0]).ShouldBe(true);
    }

    [Fact]
    public void TestParseRules()
    {
        string input = """
        47|53
        97|13
        97|61

        75,47,61,53,29
        97,61,53,29,13
        """;
        Puzzle puzzle = new Puzzle(input);
        puzzle.Rules.Count.ShouldBe(3);
        puzzle.Rules.ShouldContain(new Rule(47, 53));
        puzzle.Rules.ShouldContain(new Rule(97, 13));
        puzzle.Rules.ShouldContain(new Rule(97, 61));

        puzzle.ListOfManuals.Count.ShouldBe(2);
        puzzle.ListOfManuals[0].Count.ShouldBe(5);
        puzzle.ListOfManuals[0].ShouldBe([75, 47, 61, 53, 29]);

        puzzle.ListOfManuals[1].Count.ShouldBe(5);
        puzzle.ListOfManuals[1].ShouldBe([97,61,53,29,13]);
    }

    

    [Fact]
    public void FinalPart1()
    {
        Puzzle puzzle = Puzzle.FromFile("inputs/puzzle-input.txt");
        puzzle.Part1().ShouldBe("5391");
    }

        [Fact]
    public void TestInputPart2()
    {
        string input = """
        NOT SET
        """;
        input.Trim().ShouldNotBe("NOT SET");
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
