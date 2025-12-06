using Common;

namespace Year2025.Days;

public class Day05Solver : Solver
{
    public override object SolveChallenge1(string[] input)
    {
        var parsingRanges = true;
        List<Range> ranges = [];
        var result = 0;
        
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                ranges.Sort((x, y) => x.Start.CompareTo(y.Start));
                parsingRanges = false;
            }
            else if (parsingRanges)
            {
                var split = line.Split('-');
                ranges.Add(new Range()
                {
                    Start = ulong.Parse(split[0]), 
                    Stop = ulong.Parse(split[1])
                });
            }
            else // Checking solution
            {
                var value = ulong.Parse(line);
                
                foreach (var range in ranges)
                {
                    if (value < range.Start)
                    {
                        break;
                    }
                    if (value <= range.Stop)
                    {
                        result += 1;
                        break;
                    }
                }
            }
        }
        
        return result;
    }

    public override object SolveChallenge2(string[] input)
    {
        List<Range> ranges = [];
        ulong result = 0;
        
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line)) break;
            
            var split = line.Split('-');
            ranges.Add(new Range()
            {
                Start = ulong.Parse(split[0]), 
                Stop = ulong.Parse(split[1])
            });
        }
        
        ranges.Sort((x, y) => x.Start.CompareTo(y.Start));

        var i = 1;
        while (i < ranges.Count)
        {
            var curr = ranges[i];
            var prev = ranges[i - 1];

            if (curr.Start >= prev.Start && curr.Start <= prev.Stop)
            {
                prev.Stop = ulong.Max(prev.Stop, curr.Stop);
                ranges.RemoveAt(i);
            }
            else
            {
                i++;
            }
            
        }

        foreach (var range in ranges)
        {
            var count = range.Stop - range.Start + 1;
            result += count;
        }
        
        return result;
    }

    private class Range
    {
        public ulong Start { get; set; }
        public ulong Stop { get; set; }
    }
}
