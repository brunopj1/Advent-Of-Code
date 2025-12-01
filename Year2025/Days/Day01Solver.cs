using Common;

namespace Year2025.Days;

public class Day01Solver : Solver
{
    public override object SolveChallenge1(string[] input)
    {
        var dial = 50;
        var result = 0;
        
        foreach (var line in input)
        {
            var sign = line[0] == 'L' ? -1 : 1;
            var num = int.Parse(line[1..]);
            
            dial = (dial + sign * num) % 100;
            if (dial < 0) dial += 100;
            
            if (dial == 0)
            {
                result++;
            }
        }

        return result;
    }

    public override object SolveChallenge2(string[] input)
    {
        var dial = 50;
        var result = 0;
        
        foreach (var line in input)
        {
            var sign = line[0] == 'L' ? -1 : 1;
            var num = int.Parse(line[1..]);

            var nextDial = (dial + sign * num % 100);
            
            result += num / 100; // full rotations

            if (nextDial == 0)
            {
                result += 1;
            }
            else if (nextDial >= 100)
            {
                result += 1;
                nextDial -= 100;
            }
            else if (nextDial < 0)
            {
                if (dial != 0) result += 1; // already accounted
                nextDial += 100;
            }

            dial = nextDial;
        }

        return result;
    }
}
