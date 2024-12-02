namespace CaptainCoder.AdventOfCode;

public class Puzzle
{
    public string Input { get; }
    public string[] Rows { get; }
    public Puzzle(string input)
    {
        Input = input.ReplaceLineEndings();
        Rows = Input.Split(Environment.NewLine);
    }

    public static IEnumerable<IEnumerable<int>> RemoveOne(IEnumerable<int> inputs)
    {
        return Enumerable.Range(0, inputs.Count())
                  .Select(ixToRemove => inputs.Where((value, ix) => ix != ixToRemove));
    }

    public static bool IsSafeToleranceOne(string input)
    {
        var values = input.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                .Select(int.Parse);
        var toleranceList = RemoveOne(values);
        return toleranceList.Any(IsSafe);
    }

    public static bool IsSafe(string input)
    {
        // [ 7, 6, 4, 2, 1 ]
        // [ 6, 4, 2, 1]
        // [ (7, 6), (6, 4), (4, 2), (2, 1)]
        var values = input.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                .Select(int.Parse);

        return IsSafe(values);
    }

    public static bool IsSafe(IEnumerable<int> inputs)
    {
        var diffs = inputs.Zip(inputs.Skip(1))
                          .Select(pair => pair.First - pair.Second);

        return diffs.All(x => x >= 1 && x <= 3) || 
               diffs.All(x => x >= -3 && x <= -1);
    }

    public static Puzzle FromFile(string path)
    {
        return new Puzzle(File.ReadAllText(path));
    }

    public virtual string Part1()
    {
        return Rows.Count(IsSafe).ToString();
    }

    public virtual string Part2()
    {
        return Rows.Count(IsSafeToleranceOne).ToString();
    }
}