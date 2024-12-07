using Common;

namespace Year2024.Days;

public class Day02Solver : Solver
{
    public override object SolveChallenge1(string[] input)
    {
        var parsed = ParseInput(input);
        return parsed.Count(CheckLine);
    }

    public override object SolveChallenge2(string[] input)
    {
        var parsed = ParseInput(input);
        return parsed.Count(x => CheckLine(x) || CheckLineButSkip(x));
    }

    private static List<List<int>> ParseInput(string[] input)
    {
        return input.Select(x => x.Split(" ").Select(int.Parse).ToList()).ToList();
    }

    private static bool CheckLine(List<int> data)
    {
        var difs = new int[data.Count - 1];
        for (var i = 0; i < data.Count - 1; i++)
        {
            difs[i] = data[i + 1] - data[i];
        }

        return
            difs.All(x => x >= 1 && x <= 3) || // All incrementing
            difs.All(x => x >= -3 && x <= -1); // All decrementing
    }

    private static bool CheckLineButSkip(List<int> data)
    {
        for (var i = 0; i < data.Count; i++)
        {
            var newData = new List<int>(data);
            newData.RemoveAt(i);
            
            if (CheckLine(newData))
            {
                return true;
            }
        }

        return false;
    }
}
