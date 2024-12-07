using Common;

namespace Year2024.Days;

public class Day01Solver : Solver
{
    public override object SolveChallenge1(string[] input)
    {
        var list1 = new List<int>(input.Length);
        var list2 = new List<int>(input.Length);

        foreach (var line in input.Select(x => x.Split("   ")))
        {
            list1.Add(int.Parse(line[0]));
            list2.Add(int.Parse(line[1]));
        }
        
        list1.Sort();
        list2.Sort();
        
        return input
            .Select((_, i) => int.Abs(list1[i] - list2[i]))
            .Sum();
    }

    public override object SolveChallenge2(string[] input)
    {
        var list1 = new List<int>(input.Length);
        var values = new Dictionary<int, int>(input.Length);

        foreach (var line in input.Select(x => x.Split("   ")))
        {
            list1.Add(int.Parse(line[0]));
            
            var num2 = int.Parse(line[1]);
            values[num2] = values.GetValueOrDefault(num2, 0) + 1;
        }
        
        return list1
            .Select(x => x * values.GetValueOrDefault(x, 0))
            .Sum();
        
    }
}
