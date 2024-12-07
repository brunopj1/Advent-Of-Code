using System.Text.RegularExpressions;
using Common;

// Honestly, I hate the part 2 of this challenge. It was so poorly explained...
namespace Year2023.Days;

internal partial class Day8Solver : Solver
{
    public override object SolveChallenge1(string[] input)
    {
        ParseInput(input, out var directions, out var paths);

        var current = "AAA";
        var idx = 0;
        var distance = 0;

        while (current != "ZZZ")
        {
            var goLeft = directions[idx];

            current = goLeft ? paths[current].Item1 : paths[current].Item2;

            distance++;
            idx = (idx + 1) % directions.Count;
        }

        return distance;
    }

    public override object SolveChallenge2(string[] input)
    {
        ParseInput(input, out var directions, out var paths);

        var startNodes = paths.Where(x => x.Key[2] == 'A').Select(x => x.Key).ToList();

        var results = new List<ulong>();

        foreach (var node in startNodes)
        {
            var current = node;
            var idx = 0;
            var distance = 0ul;

            while (current[2] != 'Z')
            {
                var goLeft = directions[idx];

                current = goLeft ? paths[current].Item1 : paths[current].Item2;

                distance++;
                idx = (idx + 1) % directions.Count;
            }

            results.Add(distance);
        }

        return LCM(results);
    }

    private void ParseInput(string[] input, out List<bool> directions, out Dictionary<string, (string, string)> paths)
    {
        var regex = MyRegex();

        directions = input[0].Select(x => x == 'L').ToList();

        paths = new();

        foreach (var line in input[2..])
        {
            var components = regex.Matches(line);

            paths[components[0].Value] = (components[1].Value, components[2].Value);
        }
    }
    private static ulong GCD(ulong a, ulong b)
    {
        if (a == 0) return b;
        return GCD(b % a, a);
    }

    private static ulong LCM(List<ulong> values)
    {
        if (values.Count == 0) throw new ArgumentException("Cannot find LCM of empty list");

        if (values.Count == 1) return values[0];

        var a = values[0];
        var b = LCM(values[1..]);
        return (a * b / GCD(a, b));
    }

    [GeneratedRegex(@"\w{3}")]
    private static partial Regex MyRegex();
}
