using Shouldly;

namespace CaptainCoder.AdventOfCode;

public class Test
{
    [Fact]
    public void TestInputPart1()
    {
        string input = """
        MMMSXXMASM
        MSAMXMSMSA
        AMXSXMAAMM
        MSAMASMSMX
        XMASAMXAMM
        XXAMMXXAMA
        SMSMSASXSS
        SAXAMASAAA
        MAMMMXMMMM
        MXMXAXMASX
        """;
        input.Trim().ShouldNotBe("NOT SET");
        Puzzle puzzle = new Puzzle(input);
        puzzle.Part1().ShouldBe("18");
    }

    [Fact]
    public void FinalPart1()
    {
        Puzzle puzzle = Puzzle.FromFile("inputs/puzzle-input.txt");
        puzzle.Part1().ShouldBe("2603");
    }

        [Fact]
    public void TestInputPart2()
    {
        string input = """
        MMMSXXMASM
        MSAMXMSMSA
        AMXSXMAAMM
        MSAMASMSMX
        XMASAMXAMM
        XXAMMXXAMA
        SMSMSASXSS
        SAXAMASAAA
        MAMMMXMMMM
        MXMXAXMASX
        """;
        input.Trim().ShouldNotBe("NOT SET");
        Puzzle puzzle = new Puzzle(input);
        puzzle.Part2().ShouldBe("9");
    }

    [Fact]
    public void FinalPart2()
    {
        Puzzle puzzle = Puzzle.FromFile("inputs/puzzle-input.txt");
        puzzle.Part2().ShouldBe("1965");
    }
}
