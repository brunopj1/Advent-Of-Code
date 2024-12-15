using Common;

namespace Year2024.Days;

public class Day08Solver : Solver
{
    public override object SolveChallenge1(string[] input)
    {
        var antennas = ParseAntennas(input, out var maxX, out var maxY);
        var antinodes = ParseAntinodes1(antennas, maxX, maxY);
        return antinodes.Count;
    }

    public override object SolveChallenge2(string[] input)
    {
        var antennas = ParseAntennas(input, out var maxX, out var maxY);
        var antinodes = ParseAntinodes2(antennas, maxX, maxY);
        return antinodes.Count;
    }

    private static Dictionary<char, List<(int, int)>> ParseAntennas(string[] input, out int maxX, out int maxY)
    {
        var antennas = new Dictionary<char, List<(int, int)>>();
        
        maxX = input.Length;
        maxY = input[0].Length;
        
        for (var x = 0; x < maxX; x++)
        {
            for (var y = 0; y < maxY; y++)
            {
                var type = input[x][y];
                
                if (type == '.') continue;

                var list = antennas.ComputeIfAbsent(type);
                list.Add((x, y));
            }
        }
        
        return antennas;
    }

    private static HashSet<(int, int)> ParseAntinodes1(Dictionary<char, List<(int, int)>> antennas, int maxX, int maxY)
    {
        var antinodes = new HashSet<(int, int)>();

        foreach (var list in antennas.Values)
        {
            for (var i = 0; i < list.Count - 1; i++)
            {
                for (var j = i + 1; j < list.Count; j++)
                {
                    var antenna1 = list[i];
                    var antenna2 = list[j];
                    
                    var vecX = antenna1.Item1 - antenna2.Item1;
                    var vecY = antenna1.Item2 - antenna2.Item2;

                    var posX = antenna1.Item1 + vecX;
                    var posY = antenna1.Item2 + vecY;
                    if (posX >= 0 && posX < maxX && posY >= 0 && posY < maxY)
                    {
                        antinodes.Add((posX, posY));
                    }
                    
                    posX = antenna2.Item1 - vecX;
                    posY = antenna2.Item2 - vecY;
                    if (posX >= 0 && posX < maxX && posY >= 0 && posY < maxY)
                    {
                        antinodes.Add((posX, posY));
                    }
                }
            }
        }
        
        return antinodes;
    }
    
    private static HashSet<(int, int)> ParseAntinodes2(Dictionary<char, List<(int, int)>> antennas, int maxX, int maxY)
    {
        var antinodes = new HashSet<(int, int)>();

        foreach (var list in antennas.Values)
        {
            for (var i = 0; i < list.Count - 1; i++)
            {
                for (var j = i + 1; j < list.Count; j++)
                {
                    var antenna1 = list[i];
                    var antenna2 = list[j];
                    
                    var vecX = antenna1.Item1 - antenna2.Item1;
                    var vecY = antenna1.Item2 - antenna2.Item2;
                    
                    var posX = antenna1.Item1;
                    var posY = antenna1.Item2;
                    while (posX >= 0 && posX < maxX && posY >= 0 && posY < maxY)
                    {
                        antinodes.Add((posX, posY));
                        
                        posX += vecX;
                        posY += vecY;
                    }
                    
                    posX = antenna2.Item1;
                    posY = antenna2.Item2;
                    while (posX >= 0 && posX < maxX && posY >= 0 && posY < maxY)
                    {
                        antinodes.Add((posX, posY));
                        
                        posX -= vecX;
                        posY -= vecY;
                    }
                }
            }
        }
        
        return antinodes;
    }
}
