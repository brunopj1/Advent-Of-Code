using Common;

namespace Year2024.Days;

public class Day06Solver : Solver
{
    private static readonly byte Empty        = 0b0_0000;
    private static readonly byte Obstacle     = 0b1_0000;
    private static readonly byte Visited      = 0b1_1111;
    private static readonly byte VisitedUp    = 0b0_0001;
    private static readonly byte VisitedDown  = 0b0_0010;
    private static readonly byte VisitedLeft  = 0b0_0100;
    private static readonly byte VisitedRight = 0b0_1000;
    
    public override object SolveChallenge1(string[] input)
    {
        var map = ParseMap(input, out var x, out var y);
        var (maxX, maxY) = (map.GetLength(0), map.GetLength(1));
        
        var (dirX, dirY) = (-1, 0);
        var dirByte = VisitedUp;
        
        var result = 0;

        while (true)
        {
            if (map[x, y] == Empty)
            {
                map[x, y] = Visited;
                result++;
            }
            
            var (nextX, nextY) = (x + dirX, y + dirY);

            if (nextX < 0 || nextX >= maxX || nextY < 0 || nextY >= maxY)
            {
                break;
            }
            
            while (map[nextX, nextY] == Obstacle)
            {
                Rotate(ref dirX, ref dirY, ref dirByte);
                nextX = x + dirX;
                nextY = y + dirY;
            }
            
            x = nextX;
            y = nextY;
        }
        
        return result;
    }

    public override object SolveChallenge2(string[] input)
    {
        var map = ParseMap(input, out var x, out var y);
        var (maxX, maxY) = (map.GetLength(0), map.GetLength(1));
        var mapCopy = (byte[,])map.Clone();
        
        var (dirX, dirY) = (-1, 0);
        var dirByte = VisitedUp;
        
        var result = 0;
        
        while (true)
        {
            if ((map[x, y] & dirByte) > 0) break;

            map[x, y] = (byte)(map[x, y] | dirByte);
            
            var (nextX, nextY) = (x + dirX, y + dirY);

            if (nextX < 0 || nextX >= maxX || nextY < 0 || nextY >= maxY)
            {
                break;
            }
            
            if (map[nextX, nextY] == Empty)
            {
                mapCopy[nextX, nextY] = Obstacle;

                if (CheckLoop(mapCopy, maxX, maxY, x, y, dirX, dirY, dirByte))
                {
                    result++;
                }
                
                map[nextX, nextY] = Empty;
                RevertMap(map, mapCopy, maxX, maxY);
            }

            var rotated = false;
            
            while (map[nextX, nextY] == Obstacle)
            {
                Rotate(ref dirX, ref dirY, ref dirByte);
                map[x, y] = (byte)(map[x, y] | dirByte);
                nextX = x + dirX;
                nextY = y + dirY;
                rotated = true;
            }
            
            if (rotated && map[nextX, nextY] == Empty)
            {
                mapCopy[nextX, nextY] = Obstacle;

                if (CheckLoop(mapCopy, maxX, maxY, x, y, dirX, dirY, dirByte))
                {
                    result++;
                }
                
                map[nextX, nextY] = Empty;
                RevertMap(map, mapCopy, maxX, maxY);
            }
            
            x += dirX;
            y += dirY;
        }
        
        return result;
    }

    private static byte[,] ParseMap(string[] input, out int startX, out int startY)
    {
        var (maxX, maxY) = (input.Length, input[0].Length);
        var map = new byte[input.Length, input[0].Length];
        
        startX = 0;
        startY = 0;

        for (var x = 0; x < maxX; x++)
        {
            for (var y = 0; y < maxY; y++)
            {
                switch (input[x][y])
                {
                    case '.':
                        map[x, y] = Empty;
                        break;
                    case '#':
                        map[x, y] = Obstacle;
                        break;
                    case '^':
                        map[x, y] = Empty;
                        startX = x;
                        startY = y;
                        break;
                }
            }
        }

        return map;
    }

    private static void Rotate(ref int dirX, ref int dirY, ref byte dirByte)
    {
        if (dirX == 1)
        {
            dirX = 0;
            dirY = -1;
            dirByte = VisitedLeft;
        }
        else if (dirX == -1)
        {
            dirX = 0;
            dirY = 1;
            dirByte = VisitedRight;
        }
        else if (dirY == 1)
        {
            dirX = 1;
            dirY = 0;
            dirByte = VisitedDown;
        }
        else if (dirY == -1)
        {
            dirX = -1;
            dirY = 0;
            dirByte = VisitedUp;
        }
    }

    private static bool CheckLoop(byte[,] map, int maxX, int maxY, int x, int y, int dirX, int dirY, byte dirByte)
    {
        map[x, y] = (byte)(map[x, y] & ~dirByte);
        
        while (true)
        {
            if ((map[x, y] & dirByte) > 0) return true;
            
            map[x, y] = (byte)(map[x, y] | dirByte);
            
            var (nextX, nextY) = (x + dirX, y + dirY);

            if (nextX < 0 || nextX >= maxX || nextY < 0 || nextY >= maxY)
            {
                return false;
            }
            
            while (map[nextX, nextY] == Obstacle)
            {
                Rotate(ref dirX, ref dirY, ref dirByte);
                map[x, y] = (byte)(map[x, y] | dirByte);
                nextX = x + dirX;
                nextY = y + dirY;
            }

            x = nextX;
            y = nextY;
        }
    }

    private static void RevertMap(byte[,] originalMap, byte[,] copiedMap, int maxX, int maxY)
    {
        for (var x = 0; x < maxX; x++)
        {
            for (var y = 0; y < maxY; y++)
            {
                copiedMap[x, y] = originalMap[x, y];
            }
        }
    }
}
