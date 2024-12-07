using System.Runtime.InteropServices;

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

    public virtual string Part1() =>
        Rows.Select(CalibrationEquation.Parse)
            .Where(EquationExtensions.IsValid)
            .Sum(eq => eq.TestValue).ToString();

    public virtual string Part2() => 
        Rows.Select(CalibrationEquation.Parse)
            .Where(EquationExtensions.IsValidWithConcatenation)
            .Sum(eq => eq.TestValue).ToString();

}

public record CalibrationEquation(long TestValue, List<long> Values)
{
    public const char Separator = ':';
    // TestValue: V0 V1 V2 V3
    public static CalibrationEquation Parse(string row)
    {
        string[] parts = row.Trim().Split(Separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        (long testValue, List<long> values) = (long.Parse(parts[0]), parts[1].Split(' ').Select(long.Parse).ToList());
        return new CalibrationEquation(testValue, values);
    }
}

public static class EquationExtensions
{
    public static bool IsValid(this CalibrationEquation eq, bool useConcat = false) => 
        IsValid(eq.TestValue, CollectionsMarshal.AsSpan(eq.Values), eq.Values.Count - 1, useConcat);

    public static bool IsValid(this CalibrationEquation eq) => IsValid(eq, false);
    public static bool IsValidWithConcatenation(this CalibrationEquation eq) => IsValid(eq, true); 

    private static bool IsValid(long target, Span<long> values, int ix, bool useConcat)
    {
        // If there are no targets left, we cannot be valid
        if (ix < 0) { return false; }
        long lastElement = values[ix];
        // If there is only one element left and it is our target, it IS valid
        if (ix == 0) { return target == lastElement; }

        // If we can factor out the lastElement AND the rest is valid, it is valid
        if (target % lastElement == 0 && IsValid(target / lastElement, values, ix -1, useConcat)) { return true; }

        // If we can subtract out the lastElement AND the rest is valid, it is valid
        if (target > lastElement && IsValid(target - lastElement, values, ix - 1, useConcat)) { return true; }
        
        // Perform concatenation and recurse
        // "29": 5 290 
        string tString = target.ToString();
        string lString = lastElement.ToString();
        // "2987": 29 8 7
        // "298": 29 8
        // Span<long> n = values[7..12];
        if (useConcat && 
            tString.Length > lString.Length && 
            tString.EndsWith(lString) && 
            IsValid(long.Parse(tString[..^lString.Length]), values, ix - 1, useConcat)) { return true; }

        // Otherwise, this is not valid
        return false;
    }
}