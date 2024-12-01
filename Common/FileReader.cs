namespace Common;

public class FileReader
{
    private FileReader() { }
    
    public static List<string[]> ReadFiles(int day)
    {
        var files = new List<string[]>(3);
        
        files.Add(ReadFile($"Input/day{day:D2}_example1.txt")!);
        files.Add(ReadFile($"Input/day{day:D2}_example2.txt") ?? files[0]);
        files.Add(ReadFile($"Input/day{day:D2}_full.txt")!);
        
        return files;
    }

    private static string[]? ReadFile(string path)
    {
        return !File.Exists(path) ? null : File.ReadAllLines(path);
    }
}
