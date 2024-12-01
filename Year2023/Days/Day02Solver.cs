using System.Text.RegularExpressions;
using Common;

namespace Year2023.Days;

internal partial class Day2Solver : Solver
{
    public override string SolveChallenge1(string[] input)
    {
        var total = 0;

        foreach (var line in input)
        {
            var components = line.Split(": ");
            var gameId = int.Parse(components[0][5..]);
            var isGameValid = true;

            var cubes = MyRegex().Split(components[1]);

            for (var i = 0; i < cubes.Length; i += 2)
            {
                var count = int.Parse(cubes[i]);
                var color = cubes[i + 1];

                isGameValid = color switch
                {
                    "red" => count <= 12,
                    "green" => count <= 13,
                    "blue" => count <= 14,
                    _ => false
                };

                if (!isGameValid) break;
            }

            if (isGameValid) total += gameId;
        }

        return total.ToString();
    }

    public override string SolveChallenge2(string[] input)
    {
        var total = 0;

        foreach (var line in input)
        {
            var components = line.Split(": ");
            var gameId = int.Parse(components[0][5..]);

            var cubes = MyRegex().Split(components[1]);

            var red = 0; var green = 0; var blue = 0;

            for (var i = 0; i < cubes.Length; i += 2)
            {
                var count = int.Parse(cubes[i]);
                var color = cubes[i + 1];

                switch (color)
                {
                    case "red":
                        red = int.Max(red, count);
                        break;
                    case "green":
                        green = int.Max(green, count);
                        break;
                    case "blue":
                        blue = int.Max(blue, count);
                        break;
                    default:
                        break;
                };

            }

            total += red * green * blue;
        }

        return total.ToString();
    }

    [GeneratedRegex(@"[,;]? ")]
    private static partial Regex MyRegex();
}
