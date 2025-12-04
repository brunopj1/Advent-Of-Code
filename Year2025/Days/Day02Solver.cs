using Common;

namespace Year2025.Days;

public class Day02Solver : Solver
{
    public override object SolveChallenge1(string[] input)
    {
        ulong result = 0;
        
        foreach (var sequence in input[0].Split(','))
        {
            var split = sequence.Split('-');

            var from = ulong.Parse(split[0]);
            var to = ulong.Parse(split[1]);

            for (var i = from; i <= to; i++)
            {
                var str = i.ToString();
                var len = str.Length;
                
                if (len % 2 == 1) continue;
                
                var half1 = str[..(len / 2)];
                var half2 = str[(len / 2)..];

                if (half1 == half2)
                {
                    result += i;
                }
            }
        }
        
        return result;
    }

    public override object SolveChallenge2(string[] input)
    {
        ulong result = 0;
        
        foreach (var sequence in input[0].Split(','))
        {
            var split = sequence.Split('-');

            var from = ulong.Parse(split[0]);
            var to = ulong.Parse(split[1]);

            for (var i = from; i <= to; i++)
            {
                var str = i.ToString();
                var len = str.Length;

                for (var j = 1; j <= len / 2; j++)
                {
                    if (len % j != 0)
                    {
                        continue;
                    }
                    
                    var value = str[..j];
                    if (str.Skip(j).Chunk(j).All(c => new string(c) == value))
                    {
                        result += i;
                        break;
                    }
                }
            }
        }
        
        return result;
    }
}
