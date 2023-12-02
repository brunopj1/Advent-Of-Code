using System.Text.RegularExpressions;

internal static partial class Challenge2
{
    [GeneratedRegex(@"[,;]? ")]
    private static partial Regex MyRegex();

    public static int Solve(string[] input)
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

        return total;
    }
}
