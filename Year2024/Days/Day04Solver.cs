using System.Text.RegularExpressions;
using Common;

namespace Year2024.Days;

public class Day04Solver : Solver
{
    public override string SolveChallenge1(string[] input)
    {
        var count = 0;
        
        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[i].Length; j++)
            {
                if (FindXmas1(input, i, j, input.Length, input[i].Length, 0, 1)) count++;
                if (FindXmas1(input, i, j, input.Length, input[i].Length, 0, -1)) count++;
                if (FindXmas1(input, i, j, input.Length, input[i].Length, 1, 0)) count++;
                if (FindXmas1(input, i, j, input.Length, input[i].Length, -1, 0)) count++;
                if (FindXmas1(input, i, j, input.Length, input[i].Length, 1, 1)) count++;
                if (FindXmas1(input, i, j, input.Length, input[i].Length, 1, -1)) count++;
                if (FindXmas1(input, i, j, input.Length, input[i].Length, -1, 1)) count++;
                if (FindXmas1(input, i, j, input.Length, input[i].Length, -1, -1)) count++;
            }
        }
        
        return count.ToString();
    }

    public override string SolveChallenge2(string[] input)
    {
        var count = 0;

        for (var i = 1; i < input.Length - 1; i++)
        {
            for (var j = 1; j < input[i].Length - 1; j++)
            {
                if (FindXmas2(input, i, j, input.Length, input[i].Length)) count++;
            }
        }
        
        return count.ToString();
    }
    
    private static bool FindXmas1(string[] input, int x, int y, int sizeX, int sizeY, int stepX, int stepY)
    {
        var maxX = x + stepX * 3;
        if (maxX < 0 || maxX >= sizeX) return false;
        
        var maxY = y + stepY * 3;
        if (maxY < 0 || maxY >= sizeY) return false;
        
        return
            input[x + stepX * 0][y + stepY * 0] == 'X' &&
            input[x + stepX * 1][y + stepY * 1] == 'M' &&
            input[x + stepX * 2][y + stepY * 2] == 'A' &&
            input[x + stepX * 3][y + stepY * 3] == 'S';
    }
    
    private static bool FindXmas2(string[] input, int x, int y, int sizeX, int sizeY)
    {
        if (x < 1 || x >= sizeX - 1) return false;
        if (y < 1 || y >= sizeY - 1) return false;

        return
            input[x][y] == 'A' 
            && ((input[x - 1][y - 1] == 'M' && input[x + 1][y + 1] == 'S') || (input[x - 1][y - 1] == 'S' && input[x + 1][y + 1] == 'M')) 
            && ((input[x - 1][y + 1] == 'M' && input[x + 1][y - 1] == 'S') || (input[x - 1][y + 1] == 'S' && input[x + 1][y - 1] == 'M'));
    }
}
