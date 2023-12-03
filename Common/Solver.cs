namespace Common;

public abstract class Solver
{
    public void Solve(string path)
    {
        var lines = File.ReadAllLines(path);

        var result1 = SolveChallenge1(lines);
        var result2 = SolveChallenge2(lines);

        Console.WriteLine($"Challenge 1: {result1}");
        Console.WriteLine($"Challenge 2: {result2}");
    }

    protected abstract object SolveChallenge1(string[] input);
    protected abstract object SolveChallenge2(string[] input);
}
