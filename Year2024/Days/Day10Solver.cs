using System.Numerics;
using Common;

namespace Year2024.Days;

public class Day10Solver : Solver
{
    private class Position
    {
        public int Height { get; set; }
        public HashSet<(int, int)> Peaks { get; set; } = [];
    }
    
    public override object SolveChallenge1(string[] input)
    {
        var map = ParseInput(input, out var maxX, out var maxY);

        for (var i = 8; i >= 0; i--)
        {
            for (var x = 0; x < maxX; x++)
            {
                for (var y = 0; y < maxY; y++)
                {
                    var pos = map[x, y];

                    var upX = x - 1;
                    if (upX >= 0 && map[upX, y].Height == pos.Height + 1)
                    {
                        pos.Peaks.UnionWith(map[upX, y].Peaks);
                    }
                    
                    var downX = x + 1;
                    if (downX < maxX && map[downX, y].Height == pos.Height + 1)
                    {
                        pos.Peaks.UnionWith(map[downX, y].Peaks);
                    }
                    
                    var leftY = y - 1;
                    if (leftY >= 0 && map[x, leftY].Height == pos.Height + 1)
                    {
                        pos.Peaks.UnionWith(map[x, leftY].Peaks);
                    }
                    
                    var rightY = y + 1;
                    if (rightY < maxY && map[x, rightY].Height == pos.Height + 1)
                    {
                        pos.Peaks.UnionWith(map[x, rightY].Peaks);
                    }
                }
            }
        }

        var result = 0;

        for (var x = 0; x < maxX; x++)
        {
            for (var y = 0; y < maxY; y++)
            {
                var pos = map[x, y];
                if (pos.Height == 0)
                {
                    result += pos.Peaks.Count;
                }
            }
        }
        
        return result;
    }

    public override object SolveChallenge2(string[] input)
    {
        
        return "";
    }

    private static Position[,] ParseInput(string[] input, out int maxX, out int maxY)
    {
        maxX = input.Length;
        maxY = input[0].Length;
        var map = new Position[maxX, maxY];

        for (var x = 0; x < maxX; x++)
        {
            for (var y = 0; y < maxY; y++)
            {
                var pos = new Position { Height = input[x][y] - '0' };
                if (pos.Height == 9) pos.Peaks.Add((x, y));
                map[x, y] = pos;
            }
        }
        
        return map;
    }
}
