using System.Text.RegularExpressions;
using Common;

namespace Year2024.Days;

public class Day05Solver : Solver
{
    public override string SolveChallenge1(string[] input)
    {
        var restrictions = ParseRestrictions(input, out var count);

        var result = input
            .Skip(count + 1)
            .Select(line => line.Split(",").Select(int.Parse).ToList())
            .Where(pages => CheckOrder(pages, restrictions))
            .Sum(pages => pages[pages.Count / 2]);

        return result.ToString();
    }

    public override string SolveChallenge2(string[] input)
    {
        var restrictions = ParseRestrictions(input, out var count);

        var result = input
            .Skip(count + 1)
            .Select(line => line.Split(",").Select(int.Parse).ToList())
            .Where(pages => !CheckOrder(pages, restrictions))
            .Select(pages => Sort(pages, restrictions))
            .Where(pages => CheckOrder(pages, restrictions))
            .Sum(pages => pages[pages.Count / 2]);

        return result.ToString();
    }

    private static Dictionary<int, List<int>> ParseRestrictions(string[] input, out int count)
    {
        var restrictions = new Dictionary<int, List<int>>();
        count = 0;
        
        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];

            if (string.IsNullOrEmpty(line))
            {
                count = i;
                break;
            }
            
            var components = line.Split("|");

            var left = int.Parse(components[0]);
            var right = int.Parse(components[1]);

            var values = restrictions.ComputeIfAbsent(right);
            values.Add(left);
        }
        
        return restrictions;
    }
    
    private static bool CheckOrder(List<int> pages, Dictionary<int, List<int>> restrictions)
    {
        var forbidden = new HashSet<int>();

        foreach (var page in pages)
        {
            if (forbidden.Contains(page))
            {
                return false;
            }
                    
            forbidden.UnionWith(restrictions.ComputeIfAbsent(page));
        }

        return true;
    }
    
    private static List<int> Sort(List<int> pages, Dictionary<int, List<int>> restrictions)
    {
        var curr = 0;
        var swapped = new HashSet<int>();

        while (curr < pages.Count)
        {
            var forbidden = restrictions.ComputeIfAbsent(pages[curr]);

            var swapWith = Enumerable
                .Range(curr + 1, pages.Count - (curr + 1))
                .LastOrDefault(i => forbidden.Contains(pages[i]), curr);

            if (swapWith != curr)
            {
                if (swapped.Contains(pages[swapWith])) break;
                
                swapped.Add(pages[curr]);
                (pages[swapWith], pages[curr]) = (pages[curr], pages[swapWith]);
            }
            else
            {
                swapped.Clear();
                curr++;
            }
        }

        return pages;
    }
}
