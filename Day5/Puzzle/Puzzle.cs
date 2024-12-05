namespace CaptainCoder.AdventOfCode;

public class Puzzle
{
    public string Input { get; }
    public HashSet<Rule> Rules { get; }
    public List<List<int>> ListOfManuals { get; }        
    public string[] Rows => Input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    
    public Puzzle(string input)
    {
        Input = input.ReplaceLineEndings();

        ListOfManuals = 
            [..
                Rows.Where(r => r.Contains(','))
                    .Select(r => new List<int>(r.Split(',').Select(int.Parse)))          
            ];
        Rules = 
            [.. 
                Rows.Where(r => r.Contains('|'))
                    .Select(r => r.Split('|'))
                    .Select(r => new Rule(int.Parse(r[0]), int.Parse(r[1])))
            ];
    }

    public static Puzzle FromFile(string path)
    {
        return new Puzzle(File.ReadAllText(path));
    }

    public bool IsOrdered(List<int> pageNumbers)
    {
        // [1, 2, 3, 4, 5]
        // (1, 2), (1, 3), (1, 4), (1, 5) -- All of these must be true
        // (2, 3), (2, 4), (2, 5),
        // (3, 4), (3, 5)
        // (4, 5)
        for(int ix = 0; ix < pageNumbers.Count - 1; ix++)
        {
            int first = pageNumbers[ix];
            for (int jx = ix + 1; jx < pageNumbers.Count; jx++)
            {
                int second = pageNumbers[jx];
                if (!Rules.Contains(new Rule(first, second)))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public virtual string Part1()
    {
        return ListOfManuals.Where(IsOrdered).Select(ls => ls[ls.Count/2]).Sum().ToString();
    }

    public int Compare(int a, int b)
    {
        if (Rules.Contains(new Rule(a, b))) { return -1; }
        return 1;
    }


    public virtual string Part2()
    {
        
        IEnumerable<List<int>> unordered = ListOfManuals.Where(ls => !IsOrdered(ls));
        var comparer = Comparer<int>.Create(Compare);
        return unordered.Select(ls => ls.Order(comparer).ToList()).Sum(ls => ls[ls.Count/2]).ToString();
    }

}


public record class Rule(int First, int Second);