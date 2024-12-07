using System.Text.RegularExpressions;
using Common;

namespace Year2023.Days;

internal partial class Day1Solver : Solver
{
    public override object SolveChallenge1(string[] input)
    {
        var firstNumber = -1;
        var lastNumber = 0;
        var sum = 0;

        foreach (var line in input)
        {
            foreach (var character in line)
            {
                if (char.IsDigit(character))
                {
                    var number = character - '0';

                    if (firstNumber < 0)
                    {
                        firstNumber = number;
                    }

                    lastNumber = number;
                }
            }

            sum += firstNumber * 10 + lastNumber;
            firstNumber = -1;
        }

        return sum;
    }

    public override object SolveChallenge2(string[] input)
    {
        var sum = 0;
        var regex = Challenge2Regex();

        foreach (var line in input)
        {
            var results = regex.Matches(line);

            var firstNumber = ConvertNumber(results.First().Groups.Values.ElementAt(1).Value);
            var lastNumber = ConvertNumber(results.Last().Groups.Values.ElementAt(1).Value);

            sum += 10 * firstNumber + lastNumber;
        }

        return sum;
    }

    [GeneratedRegex(@"(?=(one|two|three|four|five|six|seven|eight|nine|\d))")]
    private static partial Regex Challenge2Regex();

    private static int ConvertNumber(string number)
    {
        return number switch
        {
            "one" => 1,
            "two" => 2,
            "three" => 3,
            "four" => 4,
            "five" => 5,
            "six" => 6,
            "seven" => 7,
            "eight" => 8,
            "nine" => 9,
            _ => int.Parse(number)
        };
    }
}
