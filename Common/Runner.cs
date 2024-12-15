using System.Text.RegularExpressions;
using Common;

namespace Common;

public static partial class Runner
{
    public static void Run(string[] args)
    {
        var regex = SolverRegex();

        var solvers = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => regex.IsMatch(x.Name) && x.IsClass)
            .OrderBy(x => x.Name)
            .ToList();

        if (args[0] == "latest")
        {
            solvers = [ solvers.Last() ];
        }

        foreach (var type in solvers)
        {
            var day = int.Parse(regex.Match(type.Name).Groups[1].Value);
    
            Console.WriteLine($"Day {day:D2}:");

            var solver = (Solver)Activator.CreateInstance(type)!;
            var files = FileReader.ReadFiles(day);

            var solutionEx1 = Execute(solver.SolveChallenge1, files[0], out var execTimeEx1);
            Console.WriteLine($"Challenge 1 example solution: {solutionEx1} ({execTimeEx1} ms)");
            
            var solutionFull1 = Execute(solver.SolveChallenge1, files[2], out var execTimeFull1);
            Console.WriteLine($"Challenge 1 example solution: {solutionFull1} ({execTimeFull1} ms)");
            
            var solutionEx2 = Execute(solver.SolveChallenge2, files[0], out var execTimeEx2);
            Console.WriteLine($"Challenge 1 example solution: {solutionEx2} ({execTimeEx2} ms)");
            
            var solutionFull2 = Execute(solver.SolveChallenge2, files[2], out var execTimeFull2);
            Console.WriteLine($"Challenge 1 example solution: {solutionFull2} ({execTimeFull2} ms)");
            
            Console.WriteLine();
        }
    }

    private static object Execute(Func<string[], object> func, string[] input, out long execTime)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        
        var solution = func(input);
        
        watch.Stop();
        execTime = watch.ElapsedMilliseconds;
        
        return solution;
    }

    [GeneratedRegex(@"^Day(\d+)Solver$")]
    private static partial Regex SolverRegex();
}
