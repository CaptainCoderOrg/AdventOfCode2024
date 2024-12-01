namespace CaptainCoder.AdventOfCode;

public class Puzzle
{
    public string Input { get; }

    public Puzzle(string input)
    {
        Input = input.ReplaceLineEndings();
    }

    public static Puzzle FromFile(string path)
    {
        return new Puzzle(File.ReadAllText(path));
    }

    public static (List<int> Left, List<int> Right) SanitizeInput(string input)
    {
        string[] rows = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        List<int> left = new();
        List<int> right = new();
        foreach (string row in rows)
        {
            string[] items = row.Split("   ");
            left.Add(int.Parse(items[0]));
            right.Add(int.Parse(items[1])); 
        }
        return (left, right);
    }

    public virtual string Part1()
    {
        (List<int> left, List<int> right) = SanitizeInput(Input);
        left.Sort();
        right.Sort();
        // [1, 2, 3, 4, 5]
        // [5, 7, 2, 9, 8]
        // Zip => [(1, 5), (2, 7), (3, 2), (4, 9), (5, 8)]
        return left.Zip(right).Sum(p => Math.Abs(p.First - p.Second)).ToString();
    }

    public virtual string Part2()
    {
        (List<int> left, List<int> right) = SanitizeInput(Input);
        int sum = 0;
        foreach (int value in left)
        {
            int multiplier = right.Count(x => x == value);
            sum += value * multiplier;
        }
        return sum.ToString();
    }

}
