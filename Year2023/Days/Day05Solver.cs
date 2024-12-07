using Common;

namespace Year2023.Days;

internal partial class Day5Solver : Solver
{
    public override object SolveChallenge1(string[] input)
    {
        var seeds = ExtractSeeds(input);
        var maps = ExtractMaps(input);

        var min = uint.MaxValue;

        foreach (var seed in seeds)
        {
            min = Math.Min(min, ApplyMaps(seed, maps));
        }

        return min;
    }

    public override object SolveChallenge2(string[] input)
    {
        var seedRanges = ExtractSeedRanges(input);
        var maps = ExtractMaps(input);

        foreach (var map in maps)
        {
            map.Sort((x, y) => x.Item2 < y.Item2 ? -1 : 1);
        }

        var inputRanges = seedRanges;

        foreach (var map in maps)
        {
            var outputRanges = new List<(uint, uint)>();

            foreach (var range in inputRanges)
            {
                var convertedRanges = ConvertRange(range, map);
                outputRanges = MergeRanges(convertedRanges, outputRanges);
            }

            inputRanges = outputRanges;
        }

        return inputRanges[0].Item1;
    }

    private static uint[] ExtractSeeds(string[] input)
    {
        return input[0][7..].Split(' ').Select(x => uint.Parse(x)).ToArray();
    }

    private static List<(uint, uint)> ExtractSeedRanges(string[] input)
    {
        var numbers = input[0][7..].Split(' ').Select(x => uint.Parse(x)).ToArray();

        var ranges = new List<(uint, uint)>();

        for (var i = 0; i < numbers.Length; i += 2)
        {
            ranges.Add((numbers[i], numbers[i + 1]));
        }

        return ranges;
    }

    private static List<List<(uint, uint, uint)>> ExtractMaps(string[] inputs)
    {
        List<(uint, uint, uint)> currentMap = null!;
        List<List<(uint, uint, uint)>> mapList = new();

        foreach (var line in inputs[2..])
        {
            if (string.IsNullOrEmpty(line)) continue;

            if (line.EndsWith("map:"))
            {
                currentMap = new();
                mapList.Add(currentMap);
                continue;
            }

            var components = line.Split(' ').Select(x => uint.Parse(x)).ToArray();
            currentMap.Add((components[0], components[1], components[2]));
        }

        return mapList;
    }

    private static uint ApplyMaps(uint seed, List<List<(uint, uint, uint)>> maps)
    {
        var value = seed;

        foreach (var map in maps)
        {
            foreach (var entry in map)
            {
                var length = entry.Item3;
                var from = entry.Item2;
                var to = from + length - 1;

                if (value < from || value > to) continue;

                var destinationFrom = entry.Item1;
                value += destinationFrom - from;
                break;
            }
        }

        return value;
    }

    private static List<(uint, uint)> ConvertRange((uint, uint) range, List<(uint, uint, uint)> map)
    {
        var convertedRanges = new List<(uint, uint)>();

        var rangeLength = range.Item2;
        var rangeFrom = range.Item1;
        var rangeTo = rangeFrom + rangeLength - 1;

        while (rangeLength > 0)
        {
            foreach (var entry in map)
            {
                var entryLength = entry.Item3;
                var entryFrom = entry.Item2;
                var entryTo = entryFrom + entryLength - 1;

                if (rangeFrom < entryFrom)
                {
                    var convertedTo = Math.Min(rangeTo, entryFrom);
                    var convertedLength = convertedTo - rangeFrom + 1;
                    convertedRanges.Add((rangeFrom, convertedLength));
                    rangeFrom += convertedLength;
                    rangeLength -= convertedLength;

                    if (rangeLength == 0) break;
                }

                if (rangeFrom <= entryTo)
                {
                    var entryDestinationFrom = entry.Item1;

                    var convertedFrom = rangeFrom + entryDestinationFrom - entryFrom;
                    var convertedTo = Math.Min(rangeTo, entryTo) + entryDestinationFrom - entryFrom;
                    var convertedLength = convertedTo - convertedFrom + 1;
                    convertedRanges.Add((convertedFrom, convertedLength));
                    rangeFrom += convertedLength;
                    rangeLength -= convertedLength;

                    if (rangeLength == 0) break;
                }
            }

            if (rangeLength > 0)
            {
                convertedRanges.Add((rangeFrom, rangeLength));
                rangeLength = 0;
            }
        }

        return convertedRanges;
    }

    private static List<(uint, uint)> MergeRanges(List<(uint, uint)> from, List<(uint, uint)> into)
    {
        var merged = into.Concat(from).ToList();
        merged.Sort((x, y) => x.Item1 < y.Item1 ? -1 : 1);

        for (var i = 0; i < merged.Count - 1; i++)
        {
            var elem1 = merged[i];
            var elem2 = merged[i + 1];
            var elem1To = elem1.Item1 + elem1.Item2 - 1;
            var elem2To = elem2.Item1 + elem2.Item2 - 1;

            if (elem1To >= elem2.Item1 - 1)
            {
                var newLength = elem2To - elem1.Item1 + 1;

                merged[i] = (elem1.Item1, newLength);
                merged.RemoveAt(i + 1);
                i--;
            }
        }

        return merged;
    }
}
