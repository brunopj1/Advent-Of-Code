using System.Text.RegularExpressions;
using Common;

internal partial class Day3Solver : Solver
{
    [GeneratedRegex(@"[^\d\.]")]
    private static partial Regex SymbolRegex();

    protected override object SolveChallenge1(string[] input)
    {
        var total = 0;
        var validCells = CalculateValidCells(input);

        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];
            var num = 0;
            var isValidNum = false;

            for (var j = 0; j < line.Length; j++)
            {
                var c = line[j];

                if (!char.IsDigit(c))
                {
                    if (isValidNum)
                    {
                        total += num;
                    }

                    num = 0;
                    isValidNum = false;
                    continue;
                }

                num = num * 10 + c - '0';

                isValidNum = isValidNum || validCells.Contains((i, j));
            }

            if (isValidNum)
            {
                total += num;
            }
        }

        return total;
    }

    protected override object SolveChallenge2(string[] input)
    {
        var total = 0;
        var cellNumbers = CalculateCellNumbers(input);

        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];

            for (var j = 0; j < line.Length; j++)
            {
                var c = line[j];

                if (c != '*') continue;

                var adjacentNumbers = GetNumbersAround(i, j, cellNumbers);

                if (adjacentNumbers.Count() == 2)
                {
                    total += adjacentNumbers.Aggregate(1, (acc, x) => acc * x.Value);
                }
            }
        }

        return total;
    }

    private HashSet<(int, int)> CalculateValidCells(string[] input)
    {
        var validCells = new HashSet<(int, int)>();

        var symbolRegex = SymbolRegex();

        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];

            for (var j = 0; j < line.Length; j++)
            {
                var c = line[j];

                if (!symbolRegex.IsMatch(c.ToString())) continue;

                for (var di = -1; di <= 1; di++)
                {
                    for (var dj = -1; dj <= 1; dj++)
                    {
                        validCells.Add((i + di, j + dj));
                    }
                }
            }
        }

        return validCells;
    }

    private class BoxedInt
    {
        public int Value { get; set; } = 0;
    }

    private Dictionary<(int, int), BoxedInt> CalculateCellNumbers(string[] input)
    {
        var cellNumbers = new Dictionary<(int, int), BoxedInt>();

        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];

            BoxedInt? number = new();

            for (var j = 0; j < line.Length; j++)
            {
                var c = line[j];

                if (!char.IsDigit(c))
                {
                    number = null;
                    continue;
                }

                number ??= new();
                number.Value = number.Value * 10 + c - '0';
                cellNumbers[(i, j)] = number;
            }
        }

        return cellNumbers;
    }

    private IEnumerable<BoxedInt> GetNumbersAround(int line, int column, Dictionary<(int, int), BoxedInt> cellNumbers)
    {
        var result = new HashSet<BoxedInt>();

        for (var i = -1; i <= 1; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                if (cellNumbers.TryGetValue((line + i, column + j), out var num))
                {
                    result.Add(num);
                }
            }
        }

        return result;
    }
}
