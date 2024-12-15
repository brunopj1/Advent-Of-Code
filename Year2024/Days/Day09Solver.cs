using System.Numerics;
using Common;

namespace Year2024.Days;

public class Day09Solver : Solver
{
    private class FileInfo
    {
        public FileInfo(int number, int count)
        {
            Number = number;
            Count = count;
        }
        
        public int Number { get; init; }
        public int Count { get; set; }
    }

    public override object SolveChallenge1(string[] input)
    {
        ParseInput(input, out var files, out var spaces);

        var filesIdx = 1;

        while (spaces.Count > 0 && filesIdx < files.Count)
        {
            var spacesCount = spaces.First();
            var lastFile = files.Last();
            
            if (spacesCount == lastFile.Count)
            {
                files.Insert(filesIdx, lastFile);
                spaces.RemoveAt(0);
                files.RemoveAt(files.Count - 1);
                filesIdx += 2;
            }
            else if (spacesCount < lastFile.Count)
            {
                files.Insert(filesIdx, new FileInfo(lastFile.Number, spacesCount));
                spaces.RemoveAt(0);
                files[^1].Count -= spacesCount;
                filesIdx += 2;
            }
            else /* spacesCount > lastFileCount */
            {
                files.Insert(filesIdx, new FileInfo(lastFile.Number, lastFile.Count));
                spaces[0] -= lastFile.Count;
                files.RemoveAt(files.Count - 1);
                filesIdx += 1;
            }
        }

        BigInteger result = 0;
        var resultIdx = 0;

        foreach (var file in files)
        {
            BigInteger from = resultIdx;
            BigInteger to = resultIdx + file.Count - 1;
            result += (to - from + 1) * (from + to) / 2 * file.Number;
            resultIdx += file.Count;
        }

        return result;
    }

    public override object SolveChallenge2(string[] input)
    {
        return "";
    }

    private static void ParseInput(string[] input, out List<FileInfo> files, out List<int> spaces)
    {
        files = [];
        spaces = [];

        for (var i = 0; i < input[0].Length; i++)
        {
            var count = input[0][i] - '0';
            var isFile = i % 2 == 0;
            if (isFile) files.Add( new FileInfo(i/2, count));
            else spaces.Add(count);
        }
    }
}
