using System.Numerics;
using Common;

namespace Year2024.Days;

public class Day11Solver : Solver
{
    public override object SolveChallenge1(string[] input)
    {
        return Solve(input, 25);
    }

    public override object SolveChallenge2(string[] input)
    {
        return Solve(input, 75);
    }

    private static long Solve(string[] input, int loops)
    {
        var counts = ParseInput(input);
        var nextCounts = new Dictionary<long, long>();

        for (var t = 0; t < loops; t++)
        {
            var i = 0;
            
            foreach (var (num, count) in counts)
            {
                var numStr = num.ToString();

                if (num == 0)
                {
                    nextCounts[1] = nextCounts.ComputeIfAbsent(1) + count;
                }
                else if (numStr.Length % 2 == 0)
                {
                    var halfLength = numStr.Length / 2;
                    var numLeft = long.Parse(numStr[..halfLength]);
                    var numRight = long.Parse(numStr[halfLength..]);
                    nextCounts[numLeft] = nextCounts.ComputeIfAbsent(numLeft) + count;
                    nextCounts[numRight] = nextCounts.ComputeIfAbsent(numRight) + count;
                }
                else
                {
                    var newNum = num * 2024;
                    nextCounts[newNum] = nextCounts.ComputeIfAbsent(newNum) + count;
                }
            }
            
            (counts, nextCounts) = (nextCounts, counts);
            nextCounts.Clear();
        }

        return counts.Sum(x => x.Value);
    }

    private static Dictionary<long, long> ParseInput(string[] input)
    {
        var numbers = new Dictionary<long, long>();
        
        foreach (var numStr in input[0].Split(" "))
        {
            var num = long.Parse(numStr);
            numbers[num] = numbers.ComputeIfAbsent(num) + 1;
        }
        
        return numbers;
    }
}
