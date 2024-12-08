using Shouldly;

namespace CaptainCoder.AdventOfCode;

public class Test
{

    public const string SampleInput = """
    ............
    ........0...
    .....0......
    .......0....
    ....0.......
    ......A.....
    ............
    ............
    ........A...
    .........A..
    ............
    ............
    """;
    [Fact]
    public void TestInputPart1()
    {
        string input = SampleInput;
        input.Trim().ShouldNotBe("NOT SET");
        Puzzle puzzle = new Puzzle(input);
        puzzle.Part1().ShouldBe("14");
    }

    /*
               11
     012345678901
    0............
    1........0...
    2.....0......
    3.......0....
    4....0.......
    5......A.....
    6............
    7............
    8........A...
    9.........A..
    0............
    1............
    */
    [Fact]
    public void TestFindAntenna()
    {
        Puzzle puzzle = new (SampleInput);
        Dictionary<char, List<Position>> antenna = Puzzle.FindAntennaDictionary(puzzle.Rows);
        antenna.Keys.Count.ShouldBe(2);
        antenna['0'].Count.ShouldBe(4);
        antenna['A'].Count.ShouldBe(3);
        antenna['0'].ShouldContain(new Position(1, 8));
        antenna['0'].ShouldContain(new Position(2, 5));
        antenna['0'].ShouldContain(new Position(3, 7));
        antenna['0'].ShouldContain(new Position(4, 4));
        antenna['A'].ShouldContain(new Position(5, 6));
        antenna['A'].ShouldContain(new Position(8, 8));
        antenna['A'].ShouldContain(new Position(9, 9));
    }

    [Fact]
    public void TestFindAntinodes()
    {
        Position[] anti = [.. Puzzle.FindAntinodes(new Position(1, 8), new Position(2, 5))];
        // (3, 2), (1, 11)
        anti.ShouldContain(new Position(3, 2));
        anti.ShouldContain(new Position(0, 11));
    }

    [Fact]
    public void FinalPart1()
    {
        Puzzle puzzle = Puzzle.FromFile("inputs/puzzle-input.txt");
        puzzle.Part1().ShouldBe("Answer Unknown");
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
