using System.Text.RegularExpressions;
using Common;

internal partial class Day4Solver : Solver
{
    protected override object SolveChallenge1(string[] input)
    {
        var total = 0;

        var cardRegex = CardRegex();
        var numbersRegex = NumbersRegex();

        foreach (var line in input)
        {
            var components = cardRegex.Split(line);
            var gameId = int.Parse(components[0][5..]);

            var winningNumbers = numbersRegex.Matches(components[1]).Select(x => int.Parse(x.Value)).ToHashSet();
            var myNumbers = numbersRegex.Matches(components[2]).Select(x => int.Parse(x.Value)).ToHashSet();

            var matches = myNumbers.Where(x => winningNumbers.Contains(x)).Count();
            total += 1 << (matches - 1);
        }

        return total;
    }

    protected override object SolveChallenge2(string[] input)
    {
        var cardCounts = Enumerable.Repeat(1, input.Length).ToArray();

        var cardRegex = CardRegex();
        var numbersRegex = NumbersRegex();

        for (var i = 0; i < cardCounts.Length; i++)
        {
            var line = input[i];
            var components = cardRegex.Split(line);

            var winningNumbers = numbersRegex.Matches(components[1]).Select(x => int.Parse(x.Value)).ToHashSet();
            var myNumbers = numbersRegex.Matches(components[2]).Select(x => int.Parse(x.Value)).ToHashSet();

            var matches = myNumbers.Where(x => winningNumbers.Contains(x)).Count();

            if (matches == 0) continue;

            var currentCardCount = cardCounts[i];

            for (var j = 1; j <= matches; j++)
            {
                var cardNum = i + j;
                if (cardNum >= input.Length) continue;

                cardCounts[cardNum] += currentCardCount;
            }
        }

        return cardCounts.Sum();
    }

    [GeneratedRegex(@": | \| ")]
    private static partial Regex CardRegex();

    [GeneratedRegex(@"\d+")]
    private static partial Regex NumbersRegex();
}
