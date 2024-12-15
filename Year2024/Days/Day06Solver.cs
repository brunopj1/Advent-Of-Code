using Common;

namespace Year2024.Days;

public class Day06Solver : Solver
{
    private static readonly byte Empty    = 0b0_0000;
    private static readonly byte Obstacle = 0b1_0000;
    private static readonly byte DirUp    = 0b0_0001;
    private static readonly byte DirDown  = 0b0_0010;
    private static readonly byte DirLeft  = 0b0_0100;
    private static readonly byte DirRight = 0b0_1000;
    
    public override object SolveChallenge1(string[] input)
    {
        var map = ParseMap(input, out var x, out var y);
        var (maxX, maxY) = (map.Length, map[0].Length);
        
        var (dirX, dirY) = (-1, 0);
        var dirByte = DirUp;
        
        var result = 0;

        while (true)
        {
            if (map[x][y] == '.')
            {
                map[x][y] = 'X';
                result++;
            }
            
            var (nextX, nextY) = (x + dirX, y + dirY);

            if (nextX < 0 || nextX >= maxX || nextY < 0 || nextY >= maxY)
            {
                break;
            }
            
            if (map[nextX][nextY] == '#')
            {
                Rotate(ref dirX, ref dirY, ref dirByte);
            }
            
            x += dirX;
            y += dirY;
        }
        
        return result;
    }

    public override object SolveChallenge2(string[] input)
    {
        var map = MapToBytes(ParseMap(input, out var x, out var y));
        var (maxX, maxY) = (map.Length, map[0].Length);
        
        var (dirX, dirY) = (-1, 0);
        var dirByte = DirUp;

        var result = 0;
        var lockObject = new object();
        
        Parallel.For(0, maxX, i =>
        {
            var mapCopy = map.Select(v => v.ToArray()).ToArray();

            for (var j = 0; j < maxY; j++)
            {
                if (i == x && j == y) continue;

                if (mapCopy[i][j] == Obstacle) continue;

                mapCopy[i][j] = Obstacle;

                if (CheckLoop(mapCopy, maxX, maxY, x, y, dirX, dirY, dirByte))
                {
                    lock (lockObject) result++;
                }

                ClearMap(mapCopy, maxX, maxY);

                mapCopy[i][j] = Empty;
            }
        });
        
        return result;
    }

    private static char[][] ParseMap(string[] input, out int startX, out int startY)
    {
        var map = input.Select(x => x.ToCharArray()).ToArray();
        
        startX = 0;
        startY = 0;

        for (var x = 0; x < map.Length; x++)
        {
            for (var y = 0; y < map[0].Length; y++)
            {
                if (map[x][y] == '^')
                {
                    map[x][y] = '.';
                    
                    startX = x;
                    startY = y;
                    
                    break;
                }
            }
        }

        return map;
    }

    private static byte[][] MapToBytes(char[][] charMap)
    {
        return charMap.Select(x => x.Select(y => y == '.' ? Empty : Obstacle).ToArray()).ToArray();
    }

    private static void Rotate(ref int dirX, ref int dirY, ref byte dirByte)
    {
        if (dirX == 1)
        {
            dirX = 0;
            dirY = -1;
            dirByte = DirLeft;
        }
        else if (dirX == -1)
        {
            dirX = 0;
            dirY = 1;
            dirByte = DirRight;
        }
        else if (dirY == 1)
        {
            dirX = 1;
            dirY = 0;
            dirByte = DirDown;
        }
        else if (dirY == -1)
        {
            dirX = -1;
            dirY = 0;
            dirByte = DirUp;
        }
    }

    private static bool CheckLoop(byte[][] map, int maxX, int maxY, int x, int y, int dirX, int dirY, byte dirByte)
    {
        while (true)
        {
            if ((map[x][y] & dirByte) > 0) return true;
            
            map[x][y] = (byte)(map[x][y] | dirByte);
            
            var (nextX, nextY) = (x + dirX, y + dirY);

            if (nextX < 0 || nextX >= maxX || nextY < 0 || nextY >= maxY)
            {
                return false;
            }
            
            while (map[nextX][nextY] == Obstacle)
            {
                Rotate(ref dirX, ref dirY, ref dirByte);
                nextX = x + dirX;
                nextY = y + dirY;
            }
            
            x += dirX;
            y += dirY;
        }
    }

    private static void ClearMap(byte[][] map, int maxX, int maxY)
    {
        for (var x = 0; x < maxX; x++)
        {
            for (var y = 0; y < maxY; y++)
            {
                if ((map[x][y] & Obstacle) == 0) map[x][y] = Empty;
            }
        }
    }
}
