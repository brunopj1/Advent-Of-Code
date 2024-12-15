using System.Numerics;
using Common;

namespace Year2024.Days;

public class Day10Solver : Solver
{
    private class Position1
    {
        public int Height { get; set; }
        public HashSet<(int, int)> Peaks { get; set; } = [];
    }
    
    private class Position2
    {
        public int Height { get; set; }
        public int UniquePaths { get; set; } = 0;
    }
    
    public override object SolveChallenge1(string[] input)
    {
        var map = ParseInput1(input, out var maxX, out var maxY);

        for (var i = 8; i >= 0; i--)
        {
            for (var x = 0; x < maxX; x++)
            {
                for (var y = 0; y < maxY; y++)
                {
                    var pos = map[x, y];

                    if (pos.Height != i) continue;
                    
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
        var map = ParseInput2(input, out var maxX, out var maxY);

        for (var i = 1; i <= 9; i++)
        {
            for (var x = 0; x < maxX; x++)
            {
                for (var y = 0; y < maxY; y++)
                {
                    var pos = map[x, y];

                    if (pos.Height != i) continue;

                    var upX = x - 1;
                    if (upX >= 0 && map[upX, y].Height == pos.Height - 1)
                    {
                        pos.UniquePaths += map[upX, y].UniquePaths;
                    }
                    
                    var downX = x + 1;
                    if (downX < maxX && map[downX, y].Height == pos.Height - 1)
                    {
                        pos.UniquePaths += map[downX, y].UniquePaths;
                    }
                    
                    var leftY = y - 1;
                    if (leftY >= 0 && map[x, leftY].Height == pos.Height - 1)
                    {
                        pos.UniquePaths += map[x, leftY].UniquePaths;
                    }
                    
                    var rightY = y + 1;
                    if (rightY < maxY && map[x, rightY].Height == pos.Height - 1)
                    {
                        pos.UniquePaths += map[x, rightY].UniquePaths;
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
                if (pos.Height == 9)
                {
                    result += pos.UniquePaths;
                }
            }
        }
        
        return result;
    }

    private static Position1[,] ParseInput1(string[] input, out int maxX, out int maxY)
    {
        maxX = input.Length;
        maxY = input[0].Length;
        var map = new Position1[maxX, maxY];

        for (var x = 0; x < maxX; x++)
        {
            for (var y = 0; y < maxY; y++)
            {
                var pos = new Position1 { Height = input[x][y] - '0' };
                if (pos.Height == 9) pos.Peaks.Add((x, y));
                map[x, y] = pos;
            }
        }
        
        return map;
    }
    
    private static Position2[,] ParseInput2(string[] input, out int maxX, out int maxY)
    {
        maxX = input.Length;
        maxY = input[0].Length;
        var map = new Position2[maxX, maxY];

        for (var x = 0; x < maxX; x++)
        {
            for (var y = 0; y < maxY; y++)
            {
                var pos = new Position2 { Height = input[x][y] - '0' };
                if (pos.Height == 0) pos.UniquePaths = 1;
                map[x, y] = pos;
            }
        }
        
        return map;
    }
}
