using System.Text.RegularExpressions;

internal static partial class Challenge1
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

        return total;
    }
}
