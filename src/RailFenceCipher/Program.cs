namespace Cipher.RailFence;

static public class RailFenceCipher
{
    static void Main() { }
    public static string Encode(string s, int n)
    {
        if (String.IsNullOrEmpty(s)) { return String.Empty; }
        var output = String.Empty;
        for (int i = 0; i < n; i++)
        {
            int cycle = 0;
            var indexes = CalculateIndexes(cycle, n, i);

            while (indexes.First < s.Length)
            {
                cycle++;
                if (IsMinOrMaxDepth(indexes, i, n))
                {
                    AddLetterToOutput(ref output, indexes.First, s);
                }
                else
                {
                    AddLetterToOutput(ref output, indexes.First, s);
                    AddLetterToOutput(ref output, indexes.Second, s);
                }
                indexes = CalculateIndexes(cycle, n, i);
            }
        }
        return output;
    }
    private static bool IsMinOrMaxDepth((int First, int Second) indexes, int currentDepth, int maxDepth) => indexes.First == indexes.Second || IsMaxDepth(currentDepth, maxDepth);
    private static (int First, int Second) CalculateIndexes(int cycle, int depth, int currentline)
    {
        return (First: CalculateFirstIndex(cycle, depth, currentline), Second: CalculateSecondIndex(cycle, depth, currentline));
    }
    private static void AddLetterToOutput(ref string output, int index, string originalString)
    {
        if (IsValidIndex(index, originalString)) { output += originalString[index].ToString(); }
    }
    //currentDepth starts at 0
    private static bool IsMaxDepth(int currentDepth, int maxDepth) => maxDepth == currentDepth + 1;

    private static int CalculateSecondIndex(int cycle, int depth, int currentline)
    {
        return cycle * (2 * depth - 2) + currentline;
    }
    private static int CalculateFirstIndex(int cycle, int depth, int currentline)
    {
        return cycle * (2 * depth - 2) - currentline;
    }
    private static bool IsValidIndex(int index, string text)
    {
        if (index < 0) { return false; }
        if (index > text.Length - 1) { return false; }
        return true;
    }

    public static string Decode(string s, int n)
    {
        if (String.IsNullOrEmpty(s)) { return String.Empty; }
        int linesizes = s.Length % n == 0 ? s.Length / n : s.Length / n + 1;
        List<string> lines = new();
        for (int i = 0; i < n; i++)
        {
            try { lines.Add(s.Substring(i * linesizes, linesizes)); }
            catch { lines.Add(s.Substring(i * linesizes, linesizes - 1)); }
        }
        string output = String.Empty;
        for (int i = 0; i < lines[0].Length; i++)
        {
            try { lines.ForEach(x => output += x[i]); }
            catch { }
        }

        return output;
    }
}
