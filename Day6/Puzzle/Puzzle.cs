namespace CaptainCoder.AdventOfCode;

public class Puzzle
{
    public string Input { get; }
    public string[] Rows => Input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    public Position StartingPosition { get; }
    
    
    public Puzzle(string input)
    {
        Input = input.ReplaceLineEndings();
        StartingPosition = FindStartingPosition();
    }

    public Position FindStartingPosition()
    {
        int r = 0;
        int c = -1;
        foreach (string row in Rows)
        {
            c = row.IndexOf('^');
            if (c != -1) 
            { 
                break;
            }
            r++;
        }
        return new Position(r, c);
    }

    public static Puzzle FromFile(string path)
    {
        return new Puzzle(File.ReadAllText(path));
    }

    public virtual string Part1()
    {
        Simulation sim = new (StartingPosition, Rows);
        while (sim.Step() is not SimulationState.SteppedOffMap);
        return sim.Visited.Count.ToString();
    }

    public virtual string Part2()
    {
        Simulation firstSim = new (StartingPosition, Rows);
        while (firstSim.Step() is not SimulationState.SteppedOffMap);
        return firstSim.Visited
            .Select(o => new Simulation(StartingPosition, Rows, o).Result)
            .Count(r => r is SimulationState.StuckInLoop)
            .ToString();
    }

}

public class Simulation(Position StartingPosition, string[] Map, Position? obstruction = null)
{
    public const char Wall = '#';
    public HashSet<Position> Visited { get; }= new([StartingPosition]);
    public HashSet<Guard> GuardStates { get; } = new ([new Guard(StartingPosition, Direction.North)]);
    
    // If no obstruction is specified, place it outside of the map
    public Position Obstruction { get; } = obstruction ?? new Position(-1, -1);
    public Guard Guard { get; private set; } = new Guard(StartingPosition, Direction.North);
    public bool IsInBounds(Position p) => p.Row >= 0 && p.Col >= 0 && p.Row < Map.Length && p.Col < Map[p.Row].Length;
    public bool IsInBounds(Guard g) => IsInBounds(g.Position);
    public bool IsObstructed(Position p) => p == Obstruction || Map[p.Row][p.Col] is Wall;
    public bool IsObstructed(Guard g) => IsObstructed(g.Step().Position);
    public bool IsLoop(Guard guard) => GuardStates.Contains(guard);

    public SimulationState Result
    {
        get
        {
            while (Step() == SimulationState.InProgress);
            return Step();
        }
    }

    public SimulationState Step()
    {
        // If the guard has been in this state previously, they are stuck in a loop
        if (IsLoop(Guard.Step())) { return SimulationState.StuckInLoop; }

        // If the guard would step out of bounds, the simulation is over
        if (!IsInBounds(Guard.Step())) { return SimulationState.SteppedOffMap; }

        // If the guard cannot step forward, the guard rotates
        if (IsObstructed(Guard))
        {
            Guard turned = Guard.Clockwise();
            if (IsLoop(turned)) { return SimulationState.StuckInLoop; }
            Guard = turned;
            GuardStates.Add(Guard);
            return SimulationState.InProgress;
        }

        // Otherwise, the guard steps forward
        Guard = Guard.Step();
        Visited.Add(Guard.Position);
        GuardStates.Add(Guard);
        return SimulationState.InProgress;
    }
}

public record Guard(Position Position, Direction Facing)
{
    public Guard Step() => this with { Position = Position.Step(Facing) };
    public Guard Clockwise() => this with { Facing = Facing.Clockwise() };
}

public enum SimulationState { InProgress, SteppedOffMap, StuckInLoop, }

public enum Direction { North, East, South, West }
public static class DirectionExtensions
{
    public static Direction Clockwise(this Direction dir) =>
        dir switch 
        {
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.South => Direction.West,
            Direction.West => Direction.North,
            var unexpected => throw new Exception($"Unexpected direction {unexpected}"),
        };
}

public record Position(int Row, int Col);
public static class PositionExtensions
{
    public static Position Step(this Position pos, Direction dir) =>
        dir switch
        {
            Direction.North when pos is (int r, _) => pos with { Row = r - 1 },
            Direction.South when pos is (int r, _) => pos with { Row = r  + 1},
            Direction.East when pos is (_, int c) => pos with { Col = c + 1 },
            Direction.West when pos is (_, int c) => pos with { Col = c - 1 },
            var unexpected => throw new Exception($"Unexpected direction {unexpected}"),
        };
}

