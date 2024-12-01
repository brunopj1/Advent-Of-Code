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

            Console.WriteLine($"Challenge 1 example solution: {solver.SolveChallenge1(files[0])}");
            Console.WriteLine($"Challenge 1 full solution: {solver.SolveChallenge1(files[2])}");
    
            Console.WriteLine($"Challenge 2 example solution: {solver.SolveChallenge2(files[1])}");
            Console.WriteLine($"Challenge 2 full solution: {solver.SolveChallenge2(files[2])}");
    
            Console.WriteLine();
        }
    }

    [GeneratedRegex(@"^Day(\d+)Solver$")]
    private static partial Regex SolverRegex();
}
