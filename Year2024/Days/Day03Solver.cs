using System.Text.RegularExpressions;
using Common;

namespace Year2024.Days;

public class Day03Solver : Solver
{
    public override string SolveChallenge1(string[] input)
    {
        var regex = new Regex(@"mul\((\d+),(\d+)\)");

        var sum = 0;
        
        foreach (var line in input)
        {
            var results = regex.Matches(line);
            
            sum += results.Sum(x => int.Parse(x.Groups[1].Value) * int.Parse(x.Groups[2].Value));
        }
        
        return sum.ToString();
    }

    public override string SolveChallenge2(string[] input)
    {
        var regex = new Regex(@"mul\((\d+),(\d+)\)|do\(\)|don't\(\)");

        var doing = true;
        var sum = 0;
        
        foreach (var line in input)
        {
            var results = regex.Matches(line);

            foreach (Match match in results)
            {
                if (match.Value == "do()")
                {
                    doing = true;
                }
                else if (match.Value == "don't()")
                {
                    doing = false;
                }
                else if (doing)
                {
                    sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                }
            }
        }
        
        return sum.ToString();
    }
}
