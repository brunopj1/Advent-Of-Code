using System.Text.RegularExpressions;

internal static partial class Challenge2
{
    [GeneratedRegex(@"(?=(one|two|three|four|five|six|seven|eight|nine|\d))")]
    private static partial Regex MyRegex();

    public static int Solve(string[] input)
    {
        var sum = 0;
        var regex = MyRegex();

        foreach (var line in input)
        {
            var results = regex.Matches(line);

            var firstNumber = ConvertNumber(results.First().Groups.Values.ElementAt(1).Value);
            var lastNumber = ConvertNumber(results.Last().Groups.Values.ElementAt(1).Value);

            sum += 10 * firstNumber + lastNumber;
        }

        return sum;
    }

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
