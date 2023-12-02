namespace Day1.Source;
internal static class Challenge1
{
    public static int Solve(string[] input)
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
}
