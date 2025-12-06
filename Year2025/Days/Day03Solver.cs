using Common;

namespace Year2025.Days;

public class Day03Solver : Solver
{
    public override object SolveChallenge1(string[] input)
    {
        var result = 0;

        foreach (var line in input)
        {
            var values = line.Select(c => c - '0').ToArray();
            
            var chosen1 = values[0];
            var chosen2 = values[1];

            for (var i = 1; i < values.Length; i++)
            {
                if (values[i] > chosen1 && i < values.Length - 1)
                {
                    chosen1 = values[i];
                    chosen2 = values[i + 1];
                }
                else if (values[i] > chosen2)
                {
                    chosen2 = values[i];
                }
            }
            
            result += 10 * chosen1 + chosen2;
        }

        return result;
    }

    public override object SolveChallenge2(string[] input)
    {
        ulong result = 0;

        foreach (var line in input)
        {
            var values = line.Select(c => c - '0').ToArray();

            var chosen = new int[12];
            var prevIdx = -1;

            for (var i = 0; i < chosen.Length; i++)
            {
                // Chose the next number temporarily (and increase the previous index
                chosen[i] = values[++prevIdx];
                
                for (var j = prevIdx + 1; j < values.Length - (chosen.Length - i - 1); j++)
                {
                    if (values[j] > chosen[i])
                    {
                        chosen[i] = values[j];
                        prevIdx = j;
                    }
                }
            }

            result += ulong.Parse(chosen
                .Select(v => v.ToString())
                .Aggregate((v1, v2) => v1 + v2));
        }

        return result;
    }
    
    // got: 302612122202140
    //  is: 3121910778619
}
