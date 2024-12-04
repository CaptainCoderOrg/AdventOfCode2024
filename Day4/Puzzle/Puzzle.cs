namespace CaptainCoder.AdventOfCode;

public class Puzzle
{
    public string Input { get; }
    public string[] Rows => Input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    
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
        int count = 0;
        for (int r = 0; r < Rows.Length; r++)
        {
            for (int c = 0; c < Rows[r].Length; c++)
            {
                if (Rows[r][c] != 'X') { continue; }
                count += Direction.Directions.Count(d => HasWord("XMAS", r, c, d));
            }
        }
        return count.ToString();
    }

    public bool HasWord(string word, int sr, int sc, Direction dir)
    {
        foreach (char ch in word)
        {
            if (sr < 0 || sr >= Rows.Length || sc < 0 || sc >= Rows[sr].Length) { return false; }
            if (ch != Rows[sr][sc]) { return false; }
            sr += dir.DR;
            sc += dir.DC;
        }
        return true;
    }

    public string CornerLetters(int sr, int sc)
    {
        if (sr <= 0 || sc <= 0 || sr >= Rows.Length - 1 || sc >= Rows[sr].Length - 1)
        {
            return string.Empty;
        }
        return $"{Rows[sr-1][sc-1]}{Rows[sr-1][sc+1]}{Rows[sr+1][sc+1]}{Rows[sr+1][sc-1]}";
    }

    public bool IsWord(string word, int sr, int sc, Direction[] offsets)
    {
        foreach (char ch in word)
        {
            if (sr < 0 || sr >= Rows.Length || sc < 0 || sc >= Rows[sr].Length) { return false; }
            if (ch != Rows[sr][sc]) { return false; }
            
        }
        return true;
    }

    public virtual string Part2()
    {
        HashSet<string> words = ["MMSS", "MSSM", "SMMS", "SSMM"];
        int count = 0;
        for (int r = 0; r < Rows.Length; r++)
        {
            for (int c = 0; c < Rows[r].Length; c++)
            {
                if (Rows[r][c] != 'A') { continue; }
                if (words.Contains(CornerLetters(r, c)))
                {
                    count++;
                }
            }
        }
        return count.ToString();
    }

}

public record class Direction(int DR, int DC)
{
    public static Direction[] Directions = [
        new Direction(0, -1),
        new Direction(1, -1),
        new Direction(1, 0),
        new Direction(1, 1),
        new Direction(0, 1),
        new Direction(-1, 1),
        new Direction(-1, 0),
        new Direction(-1, -1),
    ];
}
