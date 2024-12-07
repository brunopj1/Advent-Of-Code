using System.Text.RegularExpressions;
using Common;

namespace Year2023.Days;

internal partial class Day06Solver : Solver
{
    public override object SolveChallenge1(string[] input)
    {
        var data = ParseInput(input);
        var total = 1;

        foreach (var item in data)
        {
            var solutions = 0;

            for (var i = 1ul; i < item.Time; i++)
            {
                var speed = i;
                var time = item.Time - i;

                if (speed * time > item.Distance) solutions++;
            }

            if (solutions > 0) total *= solutions;
        }

        return total;
    }

    public override object SolveChallenge2(string[] input)
    {
        var data = ParseInput2(input);

        var solutions = 0ul;

        for (var i = 1ul; i < data.Time; i++)
        {
            var speed = i;
            var time = data.Time - i;

            if (speed * time > data.Distance) solutions++;
        }

        return solutions;
    }

    private static List<Data> ParseInput(string[] input)
    {
        var regex = MyRegex();

        var times = regex.Matches(input[0]);
        var distances = regex.Matches(input[1]);

        var results = new List<Data>();

        for (var i = 0; i < times.Count; i++)
        {
            results.Add(new Data()
            {
                Time = ulong.Parse(times[i].Value),
                Distance = ulong.Parse(distances[i].Value),
            });
        }

        return results;
    }

    private Data ParseInput2(string[] input)
    {
        var time = input[0][5..].Replace(" ", null);
        var distance = input[1][9..].Replace(" ", null);

        return new Data()
        {
            Time = ulong.Parse(time),
            Distance = ulong.Parse(distance),
        };
    }

    private struct Data
    {
        public ulong Time { get; set; }
        public ulong Distance { get; set; }
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex MyRegex();
}
