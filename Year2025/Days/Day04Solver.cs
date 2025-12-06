using Common;

namespace Year2025.Days;

public class Day04Solver : Solver
{
    public override object SolveChallenge1(string[] input)
    {
        var map = input.Select(i => i.ToCharArray()).ToArray();
        var result = 0;
        
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[0].Length; j++)
            {
                if (input[i][j] != '@') continue;
                
                if (CountNeighbours(i, j, map) < 4)
                {
                    result += 1;
                }
            }
        }

        return result;
    }

    public override object SolveChallenge2(string[] input)
    {
        var map = input.Select(i => i.ToCharArray()).ToArray();
        var result = 0;
        var diff = 0;

        do
        {
            diff = 0;
            
            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map[0].Length; j++)
                {
                    if (map[i][j] != '@') continue;
                
                    if (CountNeighbours(i, j, map) < 4)
                    {
                        map[i][j] = '.';
                        result += 1;
                        diff += 1;
                    }
                }
            }

        } 
        while (diff > 0);
        
        return result;
    }

    private static int CountNeighbours(int i, int j, char[][] input)
    {
        var count = 0;
        
        for (var di = -1; di <= 1; di++)
        {
            for (var dj = -1; dj <= 1; dj++)
            {
                var ti = i + di;
                var tj = j + dj;

                if (ti == i && tj == j)
                {
                    continue;
                }
                        
                if (ti >= 0 && ti < input.Length && tj >= 0 && tj < input[0].Length && input[ti][tj] == '@')
                {
                    count += 1;
                }
            }
        }

        return count;
    }
}
