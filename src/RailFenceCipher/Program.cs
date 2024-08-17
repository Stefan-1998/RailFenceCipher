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
                if (indexes.First == indexes.Second)
                {
                    output += s[indexes.First].ToString();
                }
                else
                {
                    if (IsValidIndex(indexes.First, s) && IsNotMaxDepth(i, n)) { output += s[indexes.First].ToString(); }
                    if (IsValidIndex(indexes.Second, s)) { output += s[indexes.Second].ToString(); }
                }

                indexes = CalculateIndexes(cycle, n, i);
            }
        }
        return output;
    }
    private static (int First, int Second) CalculateIndexes(int cycle, int depth, int currentline)
    {
        return (First: CalculateFirstIndex(cycle, depth, currentline), Second: CalculateSecondIndex(cycle, depth, currentline));
    }
    //currentDepth starts at 0
    private static bool IsNotMaxDepth(int currentDepth, int maxDepth) => maxDepth != currentDepth + 1;

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
        return String.Empty;
    }
}
