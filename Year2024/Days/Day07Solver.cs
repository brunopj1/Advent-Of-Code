using Common;

namespace Year2024.Days;

public class Day07Solver : Solver
{
    public override object SolveChallenge1(string[] input)
    {
        return ParseInput(input)
            .Where(x => CheckLine1(null, x.Item1, x.Item2, 0))
            .Select(x => x.Item1)
            .Sum();
    }

    public override object SolveChallenge2(string[] input)
    {
        return ParseInput(input)
            .Where(x => CheckLine2(null, x.Item1, x.Item2, 0))
            .Select(x => x.Item1)
            .Sum();
    }

    private static List<(long, List<long>)> ParseInput(string[] input)
    {
        var result = new List<(long, List<long>)>();

        foreach (var line in input)
        {
            var components = line.Split(" ");
            
            var left = long.Parse(components[0][..^1]);
            
            var right = new List<long>();

            for (var i = 1; i < components.Length; i++)
            {
                right.Add(long.Parse(components[i]));
            }
            
            result.Add((left, right));
        }
        
        return result;
    }

    private static bool CheckLine1(long? current, long target, List<long> numbers, int index)
    {
        // Exit conditions
        if (index == numbers.Count) return current == target;
        if (current > target) return false;
        
        // Multiplication
        var next = (current ?? 1) * numbers[index];
        if (CheckLine1(next, target, numbers, index + 1)) return true;
        
        // Addition
        next = (current ?? 0) + numbers[index];
        if (CheckLine1(next, target, numbers, index + 1)) return true;
        
        // Not found
        return false;
    }
    
    private static bool CheckLine2(long? current, long target, List<long> numbers, int index)
    {
        // Exit conditions
        if (index == numbers.Count) return current == target;
        if (current > target) return false;
        
        // Concatenation
        var next = long.Parse($"{current ?? 0}{numbers[index]}");
        if (CheckLine2(next, target, numbers, index + 1)) return true;
        
        // Multiplication
        next = (current ?? 1) * numbers[index];
        if (CheckLine2(next, target, numbers, index + 1)) return true;
        
        // Addition
        next = (current ?? 0) + numbers[index];
        if (CheckLine2(next, target, numbers, index + 1)) return true;
        
        // Not found
        return false;
    }
}
