using System.Text.RegularExpressions;

namespace CaptainCoder.AdventOfCode;
// do\(\).*(mul\((\d+),(\d+)\).*)?(don't)?
// (?<enable>(don't\(\)|do\(\)))(.*)mul\((?<first>(\d{1,3})),(?<second>(\d{1,3}))\)
public partial class Puzzle
{
    public string Input { get; }
    public string[] Rows => Input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    [GeneratedRegex("""mul\((?<first>(\d{1,3})),(?<second>(\d{1,3}))\)""", RegexOptions.Compiled, "en-US")]
    public static partial Regex MulRegex();
    [GeneratedRegex("""(don't\(\)|do\(\))|(mul\((?<first>(\d{1,3})),(?<second>(\d{1,3}))\))""", RegexOptions.Compiled, "en-US")]
    public static partial Regex DoDontMulRegex();

    public Puzzle(string input)
    {
        Input = input.ReplaceLineEndings();
    }

    public static Puzzle FromFile(string path)
    {
        return new Puzzle(File.ReadAllText(path));
    }

    public virtual string Part1() => MulRegex().Matches(Input).Sum(DoMul).ToString();
    private static int DoMul(Match match) => int.Parse(match.Groups["first"].Value) * int.Parse(match.Groups["second"].Value);

    public virtual string Part2()
    {
        bool isEnabled = true;
        int sum = 0;
        foreach (Match match in DoDontMulRegex().Matches(Input))
        {
            string value = match.Value;
            if (match.Value == "do()")
            {
                isEnabled = true;
            }
            else if (match.Value == "don't()")
            {
                isEnabled = false;
            }
            else if (isEnabled)
            {
                sum += DoMul(match);
            }
        }
        return sum.ToString();
    }

}
