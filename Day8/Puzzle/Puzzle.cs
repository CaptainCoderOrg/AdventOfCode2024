using System.Collections.Generic;
using System.Net;

namespace CaptainCoder.AdventOfCode;

public class Puzzle
{
    public const char EMPTY = '.';
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
        // ILookup<char, Position> lookup = FindAntennaIEnumerables(Rows).ToLookup(t => t.Item1, t => t.Item2);
        // var antinodes = lookup.Select(FindAntinodes);
        return FindAntennaDictionary(Rows).Values.Select(FindAntinodes).SelectMany(s => s).ToHashSet().Count(InGrid).ToString();
        bool InGrid(Position p) => InBounds(Rows, p);
    }


    public static bool InBounds(string[] grid, Position p)
    {
        return !(p.Row < 0 || p.Col < 0 || p.Row >= grid.Length || p.Col >= grid[p.Row].Length);
    }
    

    public static HashSet<Position> FindAntinodes(IEnumerable<Position> antenna)
    {
        HashSet<Position> antis = new();
        foreach (Position p0 in antenna)
        {
            foreach (Position p1 in antenna) 
            {
                if (p0 == p1) { continue; }
                antis.UnionWith(FindAntinodes(p0, p1));
            }
        }
        return antis;
    }

    public static IEnumerable<Position> FindAntinodes(Position p0, Position p1)
    {
        yield return new Position(p0.Row - (p1.Row - p0.Row), p0.Col - (p1.Col - p0.Col));
        yield return new Position(p1.Row + (p1.Row - p0.Row), p1.Col + (p1.Col - p0.Col));
    }

    public static Dictionary<char, List<Position>> FindAntennaDictionary(string[] grid)
    {
        Dictionary<char, List<Position>> antenna = new();
        foreach (int row  in Enumerable.Range(0, grid.Length))
        {
            foreach (int col in Enumerable.Range(0, grid[row].Length))
            {
                char symbol = grid[row][col];
                if (symbol == EMPTY) { continue; }
                if (!antenna.TryGetValue(symbol, out List<Position>? nodes))
                {
                    nodes = new List<Position>();
                    antenna[symbol] = nodes;
                }
                nodes.Add(new Position(row, col));
            }
        }
        return antenna;
    }

    public static IEnumerable<(char, Position)> FindAntennaIEnumerables(string[] grid)
    {
        // Dictionary<char, List<Position>> antenna = new();
        //new Lookup<char, Position>() { };
        foreach (int row  in Enumerable.Range(0, grid.Length))
        {
            foreach (int col in Enumerable.Range(0, grid[row].Length))
            {
                char symbol = grid[row][col];
                if (symbol == EMPTY) { continue; }
                yield return (symbol, new Position(row, col));
                // if (!antenna.TryGetValue(symbol, out List<Position>? nodes))
                // {
                //     nodes = new List<Position>();
                //     antenna[symbol] = nodes;
                // }
                // nodes.Add(new Position(row, col));
            }
        }
        // return antenna;
    }

    public virtual string Part2()
    {
        return "Unimplemented";
    }

}

public record Position(int Row, int Col);