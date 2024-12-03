using System.Text.RegularExpressions;

namespace CaptainCoder.AdventOfCode;

public partial class Puzzle
{
    public string Input { get; }
    public string[] Rows => Input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    [GeneratedRegex("""mul\((?<first>(\d{1,3})),(?<second>(\d{1,3}))\)""", RegexOptions.Compiled, "en-US")]
    public static partial Regex MulRegex();

    public Puzzle(string input)
    {
        Input = input.ReplaceLineEndings();
    }

    public static Puzzle FromFile(string path)
    {
        return new Puzzle(File.ReadAllText(path));
    }

    public virtual string Part1()
    {
        int sum = 0;
        return MulRegex().Matches(Input).Sum(DoMul).ToString();
    }

    private static int DoMul(Match match) => int.Parse(match.Groups["first"].Value) * int.Parse(match.Groups["second"].Value);

    public virtual string Part2()
    {
        return "Unimplemented";
    }

}
